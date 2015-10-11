namespace Techies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using SharpDX;

    internal class Program
    {
        #region Static Fields

        private static float bombDmg;

        private static float bombRadius;

        private static uint lastLevel;

        #endregion

        #region Methods

        private static Dictionary<int, Ability> FindDetonatableBombs(Unit hero, Vector3 pos, IEnumerable<Unit> bombs)
        {
            var me = ObjectMgr.LocalHero;
            var possibleBombs = bombs.Where(x => x.Distance2D(pos) <= bombRadius);
            var detonatableBombs = new Dictionary<int, Ability>();
            var dmg = 0f;
            foreach (var bomb in possibleBombs)
            {
                if (dmg > 0)
                {
                    var takenDmg = hero.DamageTaken(dmg, DamageType.Magical, me, false);
                    if (takenDmg >= hero.Health)
                    {
                        break;
                    }
                }
                detonatableBombs[detonatableBombs.Count + 1] = bomb.Spellbook.Spell1;
                dmg += bombDmg;
            }
            dmg = hero.DamageTaken(dmg, DamageType.Magical, me, false);
            return dmg < hero.Health ? null : detonatableBombs;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            var me = ObjectMgr.LocalHero;

            if (!Game.IsInGame || Game.IsPaused || me == null || me.ClassID != ClassID.CDOTA_Unit_Hero_Techies)
            {
                return;
            }

            var bombAbility = me.Spellbook.SpellR;
            var bombLevel = bombAbility.Level;
            var forceStaff = me.FindItem("item_force_staff");

            if (lastLevel != bombLevel)
            {
                var firstOrDefault = bombAbility.AbilityData.FirstOrDefault(x => x.Name == "damage");
                if (firstOrDefault != null)
                {
                    bombDmg = firstOrDefault.GetValue(bombLevel - 1);
                }
                var abilityData = bombAbility.AbilityData.FirstOrDefault(x => x.Name == "radius");
                if (abilityData != null)
                {
                    bombRadius = abilityData.Value;
                }
                lastLevel = bombLevel;
            }
            var enemyHeroes =
                ObjectMgr.GetEntities<Hero>()
                    .Where(
                        x =>
                        x.Team == me.GetEnemyTeam() && x.IsAlive && x.IsVisible && !x.IsMagicImmune()
                        && x.Modifiers.All(y => y.Name != "modifier_abaddon_borrowed_time")
                        && Utils.SleepCheck(x.ClassID.ToString()));
            var bombs =
                ObjectMgr.GetEntities<Unit>()
                    .Where(
                        x =>
                        x.ClassID == ClassID.CDOTA_NPC_TechiesMines && x.Spellbook.Spell1 != null
                        && x.Spellbook.Spell1.CanBeCasted() && x.IsAlive);

            var bombsArray = bombs as Unit[] ?? bombs.ToArray();

            foreach (var hero in enemyHeroes)
            {
                var nearbyBombs = bombsArray.Any(x => x.Distance2D(hero) <= bombRadius + 500);
                if (nearbyBombs)
                {
                    CheckBombDamageAndDetonate(hero, bombsArray);
                }
                if (forceStaff == null || !(hero.Distance2D(me) <= forceStaff.CastRange)
                    || !Utils.SleepCheck("forcestaff") || bombsArray.Any(x => x.Distance2D(hero) <= bombRadius)
                    || Prediction.IsTurning(hero) || !forceStaff.CanBeCasted())
                {
                    continue;
                }

                var data =
                    Prediction.TrackTable.ToArray()
                        .FirstOrDefault(
                            unitData => unitData.UnitName == hero.Name || unitData.UnitClassID == hero.ClassID);

                if (data != null)
                {
                    var turnTime =
                        (Math.Max(
                            Math.Abs(me.FindAngleR() - Utils.DegreeToRadian(me.FindAngleBetween(hero.Position))) - 0.69,
                            0) / (0.5 * (1 / 0.03)));
                    var predict = Prediction.PredictedXYZ(hero, (float)(turnTime * 1000 + Game.Ping));
                    var forcePosition = predict
                                        + VectorExtensions.FromPolarCoordinates(
                                            1f,
                                            hero.NetworkRotationRad + data.RotSpeed).ToVector3() * 600;

                    var possibleBombs = bombsArray.Any(x => x.Distance2D(forcePosition) <= (bombRadius - 150));
                    if (!possibleBombs)
                    {
                        continue;
                    }
                    var dmg = CheckBombDamage(hero, forcePosition, bombsArray);
                    if (!(dmg >= hero.Health))
                    {
                        continue;
                    }
                }
                forceStaff.UseAbility(hero);
                Utils.Sleep(250, "forcestaff");
            }
            var creeps =
                ObjectMgr.GetEntities<Creep>()
                    .Where(
                        x =>
                        (x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Lane || x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Siege)
                        && x.IsAlive && x.IsVisible && x.IsSpawned && x.Team == me.GetEnemyTeam());

            var enumerable = creeps as Creep[] ?? creeps.ToArray();

            foreach (var detonatableBombs in from creep in enumerable
                                             let nearbyBombs =
                                                 bombsArray.Any(x => x.Distance2D(creep) <= bombRadius + 500)
                                             where nearbyBombs
                                             let detonatableBombs =
                                                 FindDetonatableBombs(creep, creep.Position, bombsArray)
                                             where detonatableBombs != null
                                             let nearbyCreeps =
                                                 enumerable.Count(
                                                     x =>
                                                     x.Distance2D(creep) <= bombRadius
                                                     && CheckBombDamage(x, x.Position, bombsArray) >= x.Health)
                                             where nearbyCreeps > 3
                                             select detonatableBombs)
            {
                foreach (var data in detonatableBombs.Where(data => Utils.SleepCheck(data.Value.Handle.ToString())))
                {
                    data.Value.UseAbility();
                    Utils.Sleep(250, data.Value.Handle.ToString());
                }
            }
        }

        private static float CheckBombDamage(Unit hero, Vector3 pos, IEnumerable<Unit> bombs)
        {
            var me = ObjectMgr.LocalHero;
            var possibleBombs = bombs.Where(x => x.Distance2D(pos) <= bombRadius);
            var dmg = bombDmg * possibleBombs.Count();
            return hero.DamageTaken(dmg, DamageType.Magical, me, false);
        }

        private static void CheckBombDamageAndDetonate(Unit hero, IEnumerable<Unit> bombs)
        {
            var pos = hero.Position;
            if (hero.NetworkActivity == NetworkActivity.Move)
            {
                pos = Prediction.InFront(hero, (Game.Ping / 1000) * hero.MovementSpeed);
            }
            CheckBombDamageAndDetonate(hero, pos, bombs);
        }

        private static void CheckBombDamageAndDetonate(Unit hero, Vector3 pos, IEnumerable<Unit> bombs)
        {
            if (!Utils.SleepCheck(hero.ClassID.ToString()))
            {
                return;
            }
            if (hero.Modifiers.Any(y => y.Name == "modifier_abaddon_borrowed_time"))
            {
                return;
            }
            var me = ObjectMgr.LocalHero;
            var possibleBombs = bombs.Where(x => x.Distance2D(pos) <= bombRadius);
            var detonatableBombs = new Dictionary<int, Ability>();
            var dmg = 0f;
            foreach (var bomb in possibleBombs)
            {
                if (dmg > 0)
                {
                    var takenDmg = hero.DamageTaken(dmg, DamageType.Magical, me, false);
                    if (takenDmg >= hero.Health)
                    {
                        break;
                    }
                }
                detonatableBombs[detonatableBombs.Count + 1] = bomb.Spellbook.Spell1;
                dmg += bombDmg;
            }
            dmg = hero.DamageTaken(dmg, DamageType.Magical, me, false);
            if (dmg < hero.Health)
            {
                return;
            }
            foreach (var data in detonatableBombs.Where(data => Utils.SleepCheck(data.Value.Handle.ToString())))
            {
                data.Value.UseAbility();
                Utils.Sleep(250, data.Value.Handle.ToString());
            }
            Utils.Sleep(250, hero.ClassID.ToString());
        }

        private static void Main()
        {
            Game.OnUpdate += Game_OnUpdate;
        }

        #endregion
    }
}