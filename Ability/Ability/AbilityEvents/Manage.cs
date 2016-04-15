namespace Ability.AbilityEvents
{
    using Ability.AbilityMenu;
    using Ability.Casting;
    using Ability.DamageCalculation;
    using Ability.Drawings;
    using Ability.ObjectManager.Heroes;
    using Ability.ObjectManager.Players;

    using Ensage;

    internal class Manage
    {
        #region Static Fields

        public static bool Loaded;

        private static bool rubick;

        #endregion

        #region Public Methods and Operators

        public static void SubscribeAllEvents()
        {
            Game.OnUpdate += AbilityMain.Game_OnUpdate;
            Drawing.OnDraw += DamageIndicator.Drawing_OnDraw;
            Player.OnExecuteOrder += AbilityMain.Player_OnExecuteOrder;
            ObjectManager.OnRemoveEntity += Update.ObjectMgr_OnRemoveEntity;
            Game.OnUpdate += Update.UpdateItems;
            Game.OnUpdate += Process.OnUpdate;
            Game.OnUpdate += MyDamage.Update;
            Game.OnUpdate += EnemyHeroes.Update;
            Game.OnUpdate += EnemyPlayers.Update;
            Game.OnUpdate += AllyHeroes.Update;
            Game.OnUpdate += AllyPlayers.Update;
            Drawing.OnDraw += GankDamage.Drawing_OnDraw;
            Drawing.OnDraw += HpBar.Update;
            Drawing.OnDraw += AbilityOverlay.Drawing_OnDraw;
            if (AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_Rubick)
            {
                Game.OnUpdate += Rubick.Game_OnUpdate;
                rubick = true;
            }
        }

        public static void UnsubscribeAllEvents()
        {
            Game.OnUpdate -= AbilityMain.Game_OnUpdate;
            Drawing.OnDraw -= DamageIndicator.Drawing_OnDraw;
            Player.OnExecuteOrder -= AbilityMain.Player_OnExecuteOrder;
            ObjectManager.OnRemoveEntity -= Update.ObjectMgr_OnRemoveEntity;
            Game.OnUpdate -= Update.UpdateItems;
            Game.OnUpdate -= Process.OnUpdate;
            Game.OnUpdate -= MyDamage.Update;
            Game.OnUpdate -= EnemyHeroes.Update;
            Game.OnUpdate -= EnemyPlayers.Update;
            Game.OnUpdate -= AllyHeroes.Update;
            Game.OnUpdate -= AllyPlayers.Update;
            Drawing.OnDraw -= GankDamage.Drawing_OnDraw;
            Drawing.OnDraw -= HpBar.Update;
            Drawing.OnDraw -= AbilityOverlay.Drawing_OnDraw;
            if (rubick)
            {
                Game.OnUpdate -= Rubick.Game_OnUpdate;
            }

            rubick = false;
        }

        #endregion
    }
}