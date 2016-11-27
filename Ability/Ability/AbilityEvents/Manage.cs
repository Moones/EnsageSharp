namespace Ability.AbilityEvents
{
    using Ability.AbilityMenu;
    using Ability.Casting;
    using Ability.DamageCalculation;
    using Ability.Drawings;
    using Ability.ObjectManager.Heroes;
    using Ability.ObjectManager.Players;

    using Ensage;
    using Ensage.Common;

    internal class Manage
    {
        #region Static Fields

        public static bool Loaded;

        private static bool rubick;

        #endregion

        #region Public Methods and Operators

        public static void SubscribeAllEvents()
        {
            Events.OnUpdate += AbilityMain.Game_OnUpdate;
            Drawing.OnDraw += DamageIndicator.Drawing_OnDraw;
            Player.OnExecuteOrder += AbilityMain.Player_OnExecuteOrder;
            ObjectManager.OnRemoveEntity += Update.ObjectMgr_OnRemoveEntity;
            Events.OnUpdate += Update.UpdateItems;
            Events.OnUpdate += Process.OnUpdate;
            Events.OnUpdate += MyDamage.Update;
            Events.OnUpdate += EnemyHeroes.Update;
            Events.OnUpdate += EnemyPlayers.Update;
            Events.OnUpdate += AllyHeroes.Update;
            Events.OnUpdate += AllyPlayers.Update;
            Drawing.OnDraw += GankDamage.Drawing_OnDraw;
            Drawing.OnDraw += HpBar.Update;
            Drawing.OnDraw += AbilityOverlay.Drawing_OnDraw;
            if (AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_Rubick)
            {
                Events.OnUpdate += Rubick.Game_OnUpdate;
                rubick = true;
            }
        }

        public static void UnsubscribeAllEvents()
        {
            Events.OnUpdate -= AbilityMain.Game_OnUpdate;
            Drawing.OnDraw -= DamageIndicator.Drawing_OnDraw;
            Player.OnExecuteOrder -= AbilityMain.Player_OnExecuteOrder;
            ObjectManager.OnRemoveEntity -= Update.ObjectMgr_OnRemoveEntity;
            Events.OnUpdate -= Update.UpdateItems;
            Events.OnUpdate -= Process.OnUpdate;
            Events.OnUpdate -= MyDamage.Update;
            Events.OnUpdate -= EnemyHeroes.Update;
            Events.OnUpdate -= EnemyPlayers.Update;
            Events.OnUpdate -= AllyHeroes.Update;
            Events.OnUpdate -= AllyPlayers.Update;
            Drawing.OnDraw -= GankDamage.Drawing_OnDraw;
            Drawing.OnDraw -= HpBar.Update;
            Drawing.OnDraw -= AbilityOverlay.Drawing_OnDraw;
            if (rubick)
            {
                Events.OnUpdate -= Rubick.Game_OnUpdate;
            }

            rubick = false;
        }

        #endregion
    }
}