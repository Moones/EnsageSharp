namespace Orbwalker
{
    using System;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects;

    /// <summary>
    ///     The orb walker.
    /// </summary>
    internal class Orbwalker
    {
        #region Static Fields

        /// <summary>
        ///     The menu.
        /// </summary>
        private static readonly Menu Menu = new Menu("Orbwalker", "orbwalker", true);

        /// <summary>
        ///     The range display.
        /// </summary>
        private static readonly RangeDrawing RangeDisplay = new RangeDrawing();

        /// <summary>
        ///     The creep target.
        /// </summary>
        private static Unit creepTarget;

        /// <summary>
        ///     The loaded.
        /// </summary>
        private static bool loaded;

        /// <summary>
        ///     The me.
        /// </summary>
        private static Hero me;

        /// <summary>
        ///     The target.
        /// </summary>
        private static Hero target;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The initialization
        /// </summary>
        public static void Init()
        {
            Menu.AddItem(new MenuItem("chaseKey", "Chase Key").SetValue(new KeyBind(32, KeyBindType.Press)));
            Menu.AddItem(new MenuItem("kiteKey", "Kite Key").SetValue(new KeyBind('V', KeyBindType.Press)));
            Menu.AddItem(new MenuItem("farmKey", "Farm Key").SetValue(new KeyBind('B', KeyBindType.Press)));
            Menu.AddItem(
                new MenuItem("bonusWindup", "Bonus WindUp time on kitting").SetValue(new Slider(500, 100, 2000))
                    .SetTooltip("Time between attacks in kitting mode"));
            Menu.AddToMainMenu();
            Game.OnUpdate += Game_OnUpdate;
            Orbwalking.Load();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The game_ on update.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void Game_OnUpdate(EventArgs args)
        {
            if (!loaded)
            {
                me = ObjectManager.LocalHero;
                if (!Game.IsInGame || me == null)
                {
                    return;
                }

                loaded = true;
                target = null;
                RangeDisplay.Dispose();
                Game.PrintMessage(
                    "<font face='Tahoma'><font color='#000000'>[--</font> <font color='#33ff66'>Orbwalker</font> by <font color='#999999'>MOON</font><font color='#ff9900'>ES</font> loaded! <font color='#000000'>--]</font></font>", 
                    MessageType.LogMessage);
            }

            if (me == null || !me.IsValid)
            {
                loaded = false;
                me = ObjectManager.LocalHero;

                target = null;
                RangeDisplay.Dispose();
                return;
            }

            RangeDisplay.Me();

            if (Game.IsPaused)
            {
                return;
            }

            if (RangeDisplay.IsDisposed())
            {
                if (me.IsAlive)
                {
                    RangeDisplay.Create();
                }
            }
            else
            {
                if (!me.IsAlive)
                {
                    RangeDisplay.Dispose();
                }
                else if (!RangeDisplay.IsUpdated())
                {
                    RangeDisplay.Update();
                }
            }

            if (target != null && (!target.IsValid || !target.IsVisible || !target.IsAlive || target.Health <= 0))
            {
                target = null;
            }

            var canCancel = Orbwalking.CanCancelAnimation();
            var cd = false;
            if (creepTarget != null && creepTarget.IsValid)
            {
                cd = Orbwalking.AttackOnCooldown(creepTarget);
            }

            if (target != null && target.IsValid && !Game.IsKeyDown(Menu.Item("farmKey").GetValue<KeyBind>().Key))
            {
                cd = Orbwalking.AttackOnCooldown(target);
            }

            if (canCancel || !cd)
            {
                if (Utils.SleepCheck("Orbwalker.Update.Target"))
                {
                    if (target != null && !target.IsVisible)
                    {
                        target = me.ClosestToMouseTarget(128);
                    }
                    else if (target == null)
                    {
                        var bestAa = me.BestAATarget();
                        if (bestAa != null)
                        {
                            target = me.BestAATarget();
                        }
                    }

                    Utils.Sleep(500, "Orbwalker.Update.Target");
                }

                if (!Game.IsChatOpen && Game.IsKeyDown(Menu.Item("farmKey").GetValue<KeyBind>().Key)
                    && (creepTarget == null || !creepTarget.IsValid || !creepTarget.IsAlive || !creepTarget.IsVisible
                        || Utils.SleepCheck("Orbwalker.Update.Creep")))
                {
                    creepTarget =
                        Creeps.All.Where(
                            x => x.IsValid && x.Team != me.Team && x.Distance2D(me) < me.GetAttackRange() + 150)
                            .MinOrDefault(x => x.Health + x.Distance2D(me));
                    Utils.Sleep(200, "Orbwalker.Update.Creep");
                }
            }

            if (Game.IsChatOpen)
            {
                return;
            }

            if (Game.IsKeyDown(Menu.Item("farmKey").GetValue<KeyBind>().Key))
            {
                Orbwalking.Orbwalk(creepTarget);
                return;
            }

            if (Game.IsKeyDown(Menu.Item("chaseKey").GetValue<KeyBind>().Key))
            {
                Orbwalking.Orbwalk(target, attackmodifiers: true);
                return;
            }

            if (Game.IsKeyDown(Menu.Item("kiteKey").GetValue<KeyBind>().Key))
            {
                Orbwalking.Orbwalk(
                    target, 
                    attackmodifiers: true, 
                    bonusWindupMs: Menu.Item("bonusWindup").GetValue<Slider>().Value);
            }
        }

        #endregion
    }
}