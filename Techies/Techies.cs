namespace Techies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using SharpDX;
    using SharpDX.Direct3D9;

    internal class Techies
    {
        #region Static Fields

        private static bool enableCreepWave = false;

        private static Ability forceStaff;

        private static bool loaded;

        private static Hero me;

        private static Ability remoteMines;

        private static float remoteMinesDmg;

        private static uint remoteMinesLevel;

        private static float remoteMinesRadius;

        private static Ability suicideAttack;

        private static float suicideAttackDmg;

        private static uint suicideAttackLevel;

        private static float suicideAttackRadius;

        private static Font text;

        #endregion

        #region Public Methods and Operators

        public static void Init()
        {
            Game.OnUpdate += Game_OnUpdate;
            loaded = false;
            text = new Font(
                Drawing.Direct3DDevice9,
                new FontDescription
                    {
                        FaceName = "Tahoma", Height = 13, OutputPrecision = FontPrecision.Default,
                        Quality = FontQuality.Default
                    });

            Drawing.OnPreReset += Drawing_OnPreReset;
            Drawing.OnPostReset += Drawing_OnPostReset;
            Drawing.OnEndScene += Drawing_OnEndScene;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomainDomainUnload;
            Game.OnWndProc += Game_OnWndProc;
        }

        #endregion

        #region Methods

        private static void CurrentDomainDomainUnload(object sender, EventArgs e)
        {
            text.Dispose();
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            if (Drawing.Direct3DDevice9 == null || Drawing.Direct3DDevice9.IsDisposed || !Game.IsInGame)
            {
                return;
            }

            var player = ObjectMgr.LocalPlayer;
            if (player == null || player.Team == Team.Observer)
            {
                return;
            }

            text.DrawText(
                null,
                enableCreepWave
                    ? "#Techies: Detonate on Creeps Enabled! | [L] for toggle"
                    : "#Techies: Detonate on Creeps Disabled! | [L] for toggle",
                5,
                128,
                Color.DarkOrange);
        }

        private static void Drawing_OnPostReset(EventArgs args)
        {
            text.OnResetDevice();
        }

        private static void Drawing_OnPreReset(EventArgs args)
        {
            text.OnLostDevice();
        }

        private static Dictionary<int, Ability> FindDetonatableBombs(Unit hero, Vector3 pos, IEnumerable<Unit> bombs)
        {
            var possibleBombs = bombs.Where(x => x.Distance2D(pos) <= remoteMinesRadius);
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
                dmg += remoteMinesDmg;
            }
            dmg = hero.DamageTaken(dmg, DamageType.Magical, me, false);
            return dmg < hero.Health ? null : detonatableBombs;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (!loaded)
            {
                me = ObjectMgr.LocalHero;
                if (!Game.IsInGame || me == null || me.ClassID != ClassID.CDOTA_Unit_Hero_Techies)
                {
                    return;
                }
                loaded = true;
                remoteMines = me.Spellbook.SpellR;
                suicideAttack = me.Spellbook.SpellE;
                forceStaff = null;
                Console.WriteLine("#Techies: Loaded!");
            }

            if (!Game.IsInGame || me == null || me.ClassID != ClassID.CDOTA_Unit_Hero_Techies)
            {
                loaded = false;
                Console.WriteLine("#Techies: Unloaded!");
                return;
            }

            if (Game.IsPaused)
            {
                return;
            }

            if (forceStaff == null)
            {
                forceStaff = me.Inventory.Items.FirstOrDefault(x => x.ClassID == ClassID.CDOTA_Item_ForceStaff);
            }

            var suicideLevel = suicideAttack.Level;

            if (suicideAttackLevel != suicideLevel)
            {
                var firstOrDefault = suicideAttack.AbilityData.FirstOrDefault(x => x.Name == "damage");
                if (firstOrDefault != null)
                {
                    suicideAttackDmg = firstOrDefault.GetValue(suicideLevel - 1);
                }
                var abilityData = suicideAttack.AbilityData.FirstOrDefault(x => x.Name == "small_radius");
                if (abilityData != null)
                {
                    suicideAttackRadius = abilityData.Value;
                }
                suicideAttackLevel = suicideLevel;
            }

            var bombLevel = remoteMines.Level;

            if (remoteMinesLevel != bombLevel)
            {
                var firstOrDefault = remoteMines.AbilityData.FirstOrDefault(x => x.Name == "damage");
                if (firstOrDefault != null)
                {
                    remoteMinesDmg = firstOrDefault.GetValue(bombLevel - 1);
                }
                var abilityData = remoteMines.AbilityData.FirstOrDefault(x => x.Name == "radius");
                if (abilityData != null)
                {
                    remoteMinesRadius = abilityData.Value;
                }
                remoteMinesLevel = bombLevel;
            }
            var enemyHeroes =
                ObjectMgr.GetEntities<Hero>()
                    .Where(
                        x =>
                        x.Team == me.GetEnemyTeam() && x.IsAlive && x.IsVisible && !x.IsMagicImmune()
                        && x.Modifiers.All(y => y.Name != "modifier_abaddon_borrowed_time")
                        && Utils.SleepCheck(x.ClassID.ToString()) && !x.IsIllusion);
            var bombs =
                ObjectMgr.GetEntities<Unit>()
                    .Where(
                        x =>
                        x.ClassID == ClassID.CDOTA_NPC_TechiesMines && x.Spellbook.Spell1 != null
                        && x.Spellbook.Spell1.CanBeCasted() && x.IsAlive);

            var bombsArray = bombs as Unit[] ?? bombs.ToArray();

            foreach (var hero in enemyHeroes)
            {
                var heroDistance = me.Distance2D(hero);
                var nearbyBombs = bombsArray.Any(x => x.Distance2D(hero) <= remoteMinesRadius + 500);
                if (nearbyBombs)
                {
                    CheckBombDamageAndDetonate(hero, bombsArray);
                }
                if (heroDistance < 400 && suicideAttackLevel > 0 && me.IsAlive)
                {
                    SuicideKillSteal(hero);
                }
                if (forceStaff == null || !(heroDistance <= forceStaff.CastRange) || !Utils.SleepCheck("forcestaff")
                    || bombsArray.Any(x => x.Distance2D(hero) <= remoteMinesRadius) || Prediction.IsTurning(hero)
                    || !forceStaff.CanBeCasted())
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

                    var possibleBombs = bombsArray.Any(x => x.Distance2D(forcePosition) <= (remoteMinesRadius - 150));
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
            if (!enableCreepWave)
            {
                return;
            }
            var creeps =
                ObjectMgr.GetEntities<Creep>()
                    .Where(
                        x =>
                        (x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Lane || x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Siege)
                        && x.IsAlive && x.IsVisible && x.IsSpawned && x.Team == me.GetEnemyTeam());

            var enumerable = creeps as Creep[] ?? creeps.ToArray();

            foreach (var data in (from creep in enumerable
                                  let nearbyBombs = bombsArray.Any(x => x.Distance2D(creep) <= remoteMinesRadius + 500)
                                  where nearbyBombs
                                  let detonatableBombs = FindDetonatableBombs(creep, creep.Position, bombsArray)
                                  where detonatableBombs != null
                                  let nearbyCreeps =
                                      enumerable.Count(
                                          x =>
                                          x.Distance2D(creep) <= remoteMinesRadius
                                          && CheckBombDamage(x, x.Position, bombsArray) >= x.Health)
                                  where nearbyCreeps > 3
                                  select detonatableBombs).SelectMany(
                                      detonatableBombs =>
                                      detonatableBombs.Where(data => Utils.SleepCheck(data.Value.Handle.ToString()))))
            {
                data.Value.UseAbility();
                Utils.Sleep(250, data.Value.Handle.ToString());
            }
        }

        private static void Game_OnWndProc(WndEventArgs args)
        {
            if (args.Msg != (ulong)Utils.WindowsMessages.WM_KEYUP || args.WParam != 'L' || Game.IsChatOpen)
            {
                return;
            }
            enableCreepWave = !enableCreepWave;
        }

        private static float CheckBombDamage(Unit hero, Vector3 pos, IEnumerable<Unit> bombs)
        {
            var possibleBombs = bombs.Where(x => x.Distance2D(pos) <= remoteMinesRadius);
            var dmg = remoteMinesDmg * possibleBombs.Count();
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
            var possibleBombs = bombs.Where(x => x.Distance2D(pos) <= remoteMinesRadius);
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
                dmg += remoteMinesDmg;
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
            Utils.Sleep(1000, hero.ClassID.ToString());
        }

        private static void SuicideKillSteal(Unit hero)
        {
            if (!Utils.SleepCheck("suicide"))
            {
                return;
            }
            var pos = hero.Position;
            if (hero.NetworkActivity == NetworkActivity.Move)
            {
                pos = Prediction.InFront(hero, (float)((Game.Ping / 1000 + me.GetTurnTime(hero)) * hero.MovementSpeed));
            }
            if (pos.Distance2D(me) < hero.Distance2D(me))
            {
                pos = hero.Position;
            }
            if (me.Distance2D(pos) > 100)
            {
                pos = (pos - me.Position) * 99 / pos.Distance2D(me) + me.Position;
            }
            if (!(pos.Distance2D(me) < suicideAttackRadius))
            {
                return;
            }
            var dmg = hero.DamageTaken(suicideAttackDmg, DamageType.Physical, me, true);
            //Console.WriteLine(dmg);
            if (!(dmg >= hero.Health))
            {
                return;
            }
            suicideAttack.UseAbility(pos);
            Utils.Sleep(500, "suicide");
        }

        #endregion
    }
}