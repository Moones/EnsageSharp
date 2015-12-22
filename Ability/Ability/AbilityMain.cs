namespace Ability
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.AbilityEvents;
    using Ability.AbilityMenu;
    using Ability.AutoAttack;
    using Ability.Casting.ComboExecution;
    using Ability.Drawings;
    using Ability.ObjectManager;
    using Ability.ObjectManager.Heroes;
    using Ability.OnUpdate;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    using SharpDX;

    internal class AbilityMain
    {
        #region Static Fields

        public static float DealtDamage;

        public static Hero Me;

        private static Vector3 lastOrderPosition;

        private static Hero target;

        #endregion

        #region Public Methods and Operators

        public static void Game_OnUpdate(EventArgs args)
        {
            if (!OnUpdateChecks.CanUpdate())
            {
                return;
            }
            MyHeroInfo.UpdatePosition();
            ManageAutoAttack.UpdateAutoAttack();
            var enemyHeroes = EnemyHeroes.UsableHeroes;
            var allyHeroes = AllyHeroes.UsableHeroes;
            GankDamage.UpdateDamage(enemyHeroes, allyHeroes);
            if (!Me.IsAlive
                || (Me.IsInvisible() && !Me.IsVisibleToEnemies && Me.ClassID != ClassID.CDOTA_Unit_Hero_Riki
                    && (Me.Modifiers.All(x => x.Name != "modifier_templar_assassin_meld")
                        || !Orbwalking.CanCancelAnimation())) || Me.IsChanneling())
            {
                return;
            }
            if (Utils.SleepCheck("cancelorder"))
            {
                if (lastOrderPosition != Vector3.Zero)
                {
                    Me.Move(lastOrderPosition);
                    lastOrderPosition = Vector3.Zero;
                }
            }
            var meModifiers = Me.Modifiers.ToList();
            var ping = Game.Ping;
            if (LaunchSnowball(meModifiers))
            {
                return;
            }
            if (MyAbilities.DeffensiveAbilities.Any() && Utils.SleepCheck("casting"))
            {
                if (allyHeroes.Any(allyHero => FullCombo.DeffensiveAutoUsage(allyHero, Me, enemyHeroes, ping)))
                {
                    return;
                }
            }

            if (!MyAbilities.OffensiveAbilities.Any())
            {
                return;
            }

            if (Utils.SleepCheck("casting"))
            {
                if (FullCombo.KillSteal(enemyHeroes, ping, Me))
                {
                    return;
                }
            }
            var meMissingHp = Me.MaximumHealth - Me.Health;
            if (
                enemyHeroes.Any(
                    enemyHero => FullCombo.AutoUsage(enemyHero, enemyHeroes, meMissingHp, meModifiers, ping, Me)))
            {
                return;
            }
            if (Game.IsChatOpen)
            {
                return;
            }
            if (Game.IsKeyDown(MainMenu.ComboKeysMenu.Item("abilityKey1").GetValue<KeyBind>().Key))
            {
                if (Utils.SleepCheck("UpdateTarget"))
                {
                    target = TargetSelector.ClosestToMouse(Me, 2000);
                    Utils.Sleep(250, "UpdateTarget");
                }
                var selectedCombo = MainMenu.ComboKeysMenu.Item("abilityComboType").GetValue<StringList>().SelectedIndex;
                if (Utils.SleepCheck("casting")
                    && !FullCombo.Execute(
                        target,
                        enemyHeroes,
                        ping,
                        selectedCombo == 2,
                        selectedCombo == 1,
                        Me,
                        meModifiers))
                {
                    Orbwalking.Orbwalk(target);
                }
            }
        }

        public static void Init()
        {
            Events.OnLoad += OnLoad.Event;
            Events.OnClose += OnClose.Event;
            if (Game.IsInGame)
            {
                OnLoad.Event(null, null);
            }
        }

        public static void Player_OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            var ability = args.Ability;
            if (ability != null && NameManager.Name(ability) == "item_blink")
            {
                var blinkPos = args.TargetPosition;
                if (Me.Distance2D(blinkPos) > 1200)
                {
                    blinkPos = (blinkPos - Me.Position) * (1200) / blinkPos.Distance2D(Me) + Me.Position;
                }
                MyHeroInfo.Position = blinkPos;
                Utils.Sleep(Game.Ping + Me.GetTurnTime(MyHeroInfo.Position) + 100, "mePosition");
                return;
            }
            if (ability != null && NameManager.Name(ability) != null)
            {
                var hero = args.Target as Hero;
                if (hero != null && ability.CanHit(hero, NameManager.Name(ability)))
                {
                    Utils.Sleep(ability.GetCastDelay(Me, hero) * 1000, "GlobalCasting");
                    Utils.Sleep(ability.GetCastDelay(Me, hero) * 1000, "casting");
                    return;
                }
                if (args.TargetPosition != Vector3.Zero
                    && (ability.GetCastRange() <= Me.Distance2D(args.TargetPosition)))
                {
                    Utils.Sleep(
                        ability.FindCastPoint() * 1000 + Me.GetTurnTime(args.TargetPosition) * 1000,
                        "GlobalCasting");
                    Utils.Sleep(ability.FindCastPoint() * 1000 + Me.GetTurnTime(args.TargetPosition) * 1000, "casting");
                    return;
                }
            }
            if (Utils.SleepCheck("cancelorder")) // && !MyHeroInfo.IsChanneling())
            {
                return;
            }
            if (args.TargetPosition != Vector3.Zero && args.Order == Order.MoveLocation)
            {
                lastOrderPosition = args.TargetPosition;
            }
            args.Process = false;
        }

        #endregion

        #region Methods

        private static bool LaunchSnowball(IEnumerable<Modifier> modifiers)
        {
            if (modifiers.All(x => x.Name != "modifier_tusk_snowball_movement"))
            {
                return false;
            }
            if (Me.ClassID != ClassID.CDOTA_Unit_Hero_Tusk || !Utils.SleepCheck("snowball"))
            {
                return false;
            }
            Me.FindSpell("tusk_launch_snowball").UseAbility();
            Utils.Sleep(1000, "snowball");
            return true;
        }

        #endregion
    }
}