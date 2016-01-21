namespace Ability.Drawings
{
    using System;
    using System.Collections.Generic;

    using Ability.ObjectManager;
    using Ability.ObjectManager.Heroes;

    using Ensage.Common;

    using SharpDX;

    internal class HpBar
    {
        #region Static Fields

        public static Dictionary<string, Vector2> HpBarPositionDictionary = new Dictionary<string, Vector2>();

        private static readonly float HpBarSizeX;

        private static readonly float HpBarSizeY;

        #endregion

        #region Constructors and Destructors

        static HpBar()
        {
            HpBarSizeX = HUDInfo.GetHPBarSizeX();
            HpBarSizeY = HUDInfo.GetHpBarSizeY();
        }

        #endregion

        #region Public Properties

        public static float SizeX
        {
            get
            {
                return HpBarSizeX;
            }
        }

        public static float SizeY
        {
            get
            {
                return HpBarSizeY;
            }
        }

        #endregion

        #region Public Methods and Operators

        public static void Update(EventArgs args)
        {
            if (!Utils.SleepCheck("HpBar.Update"))
            {
                return;
            }

            // Utils.Sleep(1, "HpBar.Update");
            foreach (var enemyHero in EnemyHeroes.UsableHeroes)
            {
                var name = NameManager.Name(enemyHero);
                if (!HpBarPositionDictionary.ContainsKey(name))
                {
                    HpBarPositionDictionary.Add(name, HUDInfo.GetHPbarPosition(enemyHero));
                }
                else
                {
                    HpBarPositionDictionary[name] = HUDInfo.GetHPbarPosition(enemyHero);
                }
            }

            foreach (var enemyHero in AllyHeroes.UsableHeroes)
            {
                var name = NameManager.Name(enemyHero);
                if (!HpBarPositionDictionary.ContainsKey(name))
                {
                    HpBarPositionDictionary.Add(name, HUDInfo.GetHPbarPosition(enemyHero));
                }
                else
                {
                    HpBarPositionDictionary[name] = HUDInfo.GetHPbarPosition(enemyHero);
                }
            }
        }

        #endregion
    }
}