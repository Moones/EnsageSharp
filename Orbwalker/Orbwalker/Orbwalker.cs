namespace Orbwalker
{
    using System;
    using System.Windows.Input;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using SharpDX;

    internal class Orbwalker
    {
        #region Static Fields

        private static bool loaded;

        private static Hero me;

        private static Unit target;

        private const Key ChaseKey = Key.Space;

        private const Key KiteKey = Key.V;

        private static ParticleEffect rangeDisplay;

        private static float lastTargettime;

        #endregion

        #region Public Methods and Operators

        public static void Init()
        {
            Game.OnUpdate += Game_OnUpdate;
            Orbwalking.Load();
            if (rangeDisplay == null)
            {
                return;
            }
            rangeDisplay.Dispose();
            rangeDisplay = null;
        }

        #endregion

        #region Methods

        private static void Game_OnUpdate(EventArgs args)
        {
            if (!loaded)
            {
                me = ObjectMgr.LocalHero;
                if (!Game.IsInGame || me == null)
                {
                    return;
                }
                loaded = true;
            }

            if (!Game.IsInGame || me == null)
            {
                loaded = false;
                me = null;
                if (rangeDisplay == null)
                {
                    return;
                }
                rangeDisplay.Dispose();
                rangeDisplay = null;
                return;
            }

            if (Game.IsPaused)
            {
                return;
            }

            if (rangeDisplay == null)
            {
                rangeDisplay = me.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
            }
            rangeDisplay.SetControlPoint(1, new Vector3(me.GetAttackRange() + me.HullRadius + 25, 0, 0));

            var canCancel = Orbwalking.CanCancelAnimation();
            if (canCancel)
            {
                if (target != null && !target.IsVisible && target is Hero)
                {
                    var closestToMouse = me.ClosestToMouseTarget(128);
                    if (closestToMouse != null)
                    {
                        target = closestToMouse;
                    }
                }
                else
                {
                    target = me.BestAATarget();
                }
            }
            if (target != null)
            {
                lastTargettime = Environment.TickCount;
            }
            else if (Environment.TickCount - lastTargettime > 5000)
            {
                target = TargetSelector.GetLowestHPCreep(me);
            }
            if (Game.IsChatOpen)
            {
                return;
            }
            if (Game.IsKeyDown(ChaseKey))
            {
                Orbwalking.Orbwalk(target,attackmodifiers: true);
            }
            if (Game.IsKeyDown(KiteKey))
            {
                Orbwalking.Orbwalk(target,attackmodifiers: true,bonusRange: (float)(UnitDatabase.GetAttackRate(me)*1000));
            }
        }

        #endregion
    }
}
