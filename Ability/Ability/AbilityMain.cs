namespace Ability
{
    using System;
    using System.Linq;

    using Ability.AbilityEvents;
    using Ability.AbilityMenu;
    using Ability.AutoAttack;
    using Ability.Casting.ComboExecution;
    using Ability.DamageCalculation;
    using Ability.Drawings;
    using Ability.Extensions;
    using Ability.ObjectManager;
    using Ability.ObjectManager.Heroes;
    using Ability.OnUpdate;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects;

    using SharpDX;

    internal class AbilityMain
    {
        #region Static Fields

        public static float DealtDamage;

        public static Hero Me;

        private static NetworkActivity lastActivity;

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
            var enemyHeroes = EnemyHeroes.UsableHeroes;
            var allyHeroes = AllyHeroes.UsableHeroes;

            GankDamage.UpdateDamage(enemyHeroes, allyHeroes);
            if (!Me.IsAlive || Me.IsChanneling() || Me.HasModifier("modifier_spirit_breaker_charge_of_darkness")
                || (MyAbilities.ChargeOfDarkness != null && MyAbilities.ChargeOfDarkness.IsValid
                    && MyAbilities.ChargeOfDarkness.IsInAbilityPhase) || !Utils.SleepCheck("Ability#.Sleep"))
            {
                return;
            }

            var keyDown = Game.IsKeyDown(MainMenu.ComboKeysMenu.Item("abilityKey1").GetValue<KeyBind>().Key);

            if (Utils.SleepCheck("cancelorder") || !keyDown)
            {
                if (lastOrderPosition != Vector3.Zero && lastActivity == NetworkActivity.Move)
                {
                    var ctarget = TargetSelector.ClosestToMouse(Me, 150);
                    if (ctarget != null && ctarget.IsValid)
                    {
                        Me.Attack(ctarget);
                    }
                    else
                    {
                        Me.Move(lastOrderPosition);
                    }

                    lastOrderPosition = Vector3.Zero;
                }
            }
            else
            {
                lastActivity = Me.NetworkActivity;
                lastOrderPosition = Game.MousePosition;
            }

            var ping = Game.Ping;

            var invisible = Me.IsInvisible() && Me.ClassID != ClassID.CDOTA_Unit_Hero_Riki
                            && (!Me.HasModifier("modifier_templar_assassin_meld") || !Orbwalking.CanCancelAnimation());

            if (!invisible && MainMenu.Menu.Item("Ability#.EnableAutoUsage").GetValue<bool>()
                && MyAbilities.DefensiveAbilities.Any() && Utils.SleepCheck("casting"))
            {
                if (Utils.SleepCheck("Orbwalk.Attack")
                    && allyHeroes.Any(allyHero => FullCombo.DeffensiveAutoUsage(allyHero, Me, enemyHeroes, ping)))
                {
                    return;
                }
            }

            var targetLock =
                MainMenu.ComboKeysMenu.Item("Ability.KeyCombo.TargetLock").GetValue<StringList>().SelectedIndex;

            if (!keyDown)
            {
                target = null;
            }

            if (Game.IsChatOpen)
            {
                return;
            }

            var meMissingHp = Me.MaximumHealth - Me.Health;
            var meMana = Me.Mana;
            ManageAutoAttack.UpdateAutoAttack();

            if (keyDown)
            {
                if (Utils.SleepCheck("UpdateTarget")
                    && (target == null || !target.IsValid || !target.IsAlive || !target.IsVisible
                        || (target.IsVisible && targetLock <= 1)))
                {
                    var mode =
                        MainMenu.ComboKeysMenu.Item("Ability.KeyCombo.Target").GetValue<StringList>().SelectedIndex;
                    target = mode == 0
                                 ? TargetSelector.ClosestToMouse(Me, 2000)
                                 : EnemyHeroes.UsableHeroes.Where(x => x.Distance2D(Me) < 2000)
                                       .MaxOrDefault(x => x.GetDoableDamage());
                    Utils.Sleep(250, "UpdateTarget");
                }

                var selectedCombo = MainMenu.ComboKeysMenu.Item("abilityComboType").GetValue<StringList>().SelectedIndex;
                var combo = false;
                if (!invisible && target != null && Utils.SleepCheck("Orbwalk.Attack"))
                {
                    combo = FullCombo.Execute(
                        target, 
                        enemyHeroes, 
                        ping, 
                        selectedCombo == 2, 
                        selectedCombo == 1, 
                        Me, 
                        meMana, 
                        selectedCombo == 3);
                }

                if (!combo
                    && (target == null
                        || (target.Distance2D(MyHeroInfo.Position) > 500 && Me.HasModifier("modifier_pudge_rot")
                            && !target.HasModifier("modifier_pudge_meat_hook")))
                    && AllyHeroes.AbilityDictionary.ContainsKey(Me.StoredName()))
                {
                    foreach (var ability in AllyHeroes.AbilityDictionary[Me.StoredName()].Where(x => x.IsToggled))
                    {
                        var handle = ability.Handle.ToString();
                        if (!Utils.SleepCheck(handle))
                        {
                            continue;
                        }

                        ability.ToggleAbility();
                        Utils.Sleep(500 + Game.Ping, handle);
                        return;
                    }
                }

                if (Me.ClassID == ClassID.CDOTA_Unit_Hero_TemplarAssassin && target != null && target.IsVisible)
                {
                    var meld = Me.Spellbook.SpellW;
                    if (meld.ManaCost <= meMana && meld.Cooldown >= 0 && meld.Cooldown < UnitDatabase.GetAttackRate(Me)
                        && target.Health > Dictionaries.HitDamageDictionary[target.Handle] * 2)
                    {
                        if (!Utils.SleepCheck("Ability.Move"))
                        {
                            return;
                        }

                        Me.Move(Game.MousePosition);
                        Utils.Sleep(100, "Ability.Move");
                        return;
                    }
                }

                if (combo || !Utils.SleepCheck("GlobalCasting")
                    || (!(Game.MousePosition.Distance2D(Me)
                          > MainMenu.ComboKeysMenu.Item("Ability.KeyCombo.NoMoveRange").GetValue<Slider>().Value)
                        && (target == null || !(Me.Distance2D(target) <= MyHeroInfo.AttackRange() + 100))))
                {
                    return;
                }

                MoveMode(target);
            }

            if (!invisible && MainMenu.Menu.Item("Ability#.EnableAutoKillSteal").GetValue<bool>()
                && Utils.SleepCheck("casting"))
            {
                Variables.Killsteal.FindTarget(enemyHeroes, Me);
                if (Variables.Killsteal.TryKillsteal(Me, ping, enemyHeroes))
                {
                    return;
                }
            }

            if (!invisible && MainMenu.Menu.Item("Ability#.EnableAutoUsage").GetValue<bool>()
                && Utils.SleepCheck("Orbwalk.Attack")
                && enemyHeroes.Any(
                    enemyHero => Variables.AutoUsage.Try(enemyHero, enemyHeroes, meMissingHp, ping, Me, meMana)))
            {
                return;
            }
        }

        public static void Init()
        {
            Events.OnLoad += OnLoad.Event;
            Events.OnClose += OnClose.Event;

            // if (Game.IsInGame && ObjectMgr.LocalHero != null)
            // {
            // OnLoad.Event(null, null);
            // }
        }

        public static bool LaunchSnowball()
        {
            if (Me.ClassID != ClassID.CDOTA_Unit_Hero_Tusk)
            {
                return false;
            }

            Me.FindSpell("tusk_launch_snowball").UseAbility();
            return true;
        }

        /// <summary>
        ///     The move mode.
        /// </summary>
        /// <param name="unitTarget">
        ///     The unit target.
        /// </param>
        public static void MoveMode(Unit unitTarget)
        {
            var mode = MainMenu.ComboKeysMenu.Item("Ability.KeyCombo.Mode").GetValue<StringList>().SelectedIndex;
            switch (mode)
            {
                case 0:

                    Orbwalking.Orbwalk(unitTarget, attackmodifiers: true);
                    break;
                case 1:
                    if (!Utils.SleepCheck("Ability.Move"))
                    {
                        return;
                    }

                    Me.Move(Game.MousePosition);
                    Utils.Sleep(100, "Ability.Move");
                    break;
                case 2:
                    if (!Utils.SleepCheck("Ability.Move") || unitTarget == null)
                    {
                        return;
                    }

                    Orbwalking.Attack(unitTarget, true);
                    Utils.Sleep(100, "Ability.Move");
                    break;
                case 3:
                    return;
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
                    blinkPos = (blinkPos - Me.Position) * 1200 / blinkPos.Distance2D(Me) + Me.Position;
                }

                MyHeroInfo.Position = blinkPos;
                Utils.Sleep(Game.Ping + Me.GetTurnTime(MyHeroInfo.Position) + 200, "mePosition");
                return;
            }

            if (ability != null && NameManager.Name(ability) != null)
            {
                var hero = args.Target as Hero;
                if (hero != null)
                {
                    Utils.Sleep(ability.GetCastDelay(Me, hero, true, useChannel: true) * 1000, "GlobalCasting");
                    Utils.Sleep(ability.GetCastDelay(Me, hero, true, useChannel: true) * 1000, "casting");
                    return;
                }

                if (args.TargetPosition != Vector3.Zero)
                {
                    Utils.Sleep(
                        ability.FindCastPoint() * 1000 + Me.GetTurnTime(args.TargetPosition) * 1000, 
                        "GlobalCasting");
                    Utils.Sleep(ability.FindCastPoint() * 1000 + Me.GetTurnTime(args.TargetPosition) * 1000, "casting");
                    return;
                }
            }

            if (Utils.SleepCheck("cancelorder"))
            {
                // && !MyHeroInfo.IsChanneling())
                return;
            }

            if (args.TargetPosition != Vector3.Zero && args.Order == Order.MoveLocation)
            {
                lastOrderPosition = args.TargetPosition;
            }

            args.Process = false;
        }

        #endregion
    }
}