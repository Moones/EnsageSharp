namespace Orbwalker
{
    using System;
    using System.Windows.Input;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    internal class Orbwalker
    {
        #region Static Fields

        private static bool loaded;

        private static Hero me;

        private static Hero target;

        private const Key ChaseKey = Key.Space;

        private const Key KiteKey = Key.V;

        #endregion

        #region Public Methods and Operators

        public static void Init()
        {
            Game.OnUpdate += Game_OnUpdate;
            Orbwalking.Load();
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
                return;
            }

            if (Game.IsPaused)
            {
                return;
            }

            var canCancel = Orbwalking.CanCancelAnimation();
            if (canCancel)
            {
                if (target != null && !target.IsVisible)
                {
                    var trgt = me.BestAATarget();
                    if (trgt != null)
                    {
                        target = trgt;
                    }
                }
                else
                {
                    target = me.BestAATarget();
                }
            }
            if (Game.IsChatOpen)
            {
                return;
            }
            if (Game.IsKeyDown(ChaseKey))
            {
                Orbwalking.Orbwalk(target);
            }
            if (Game.IsKeyDown(KiteKey))
            {
                Orbwalking.Orbwalk(target,(float)(UnitDatabase.GetAttackRate(me)*1000));
            }
        }

        #endregion
    }
}