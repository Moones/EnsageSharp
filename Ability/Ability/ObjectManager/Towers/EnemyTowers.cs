namespace Ability.ObjectManager.Towers
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    internal class EnemyTowers
    {
        #region Static Fields

        public static List<Building> Towers;

        private static Building closestTower;

        #endregion

        #region Constructors and Destructors

        static EnemyTowers()
        {
            Towers =
                ObjectMgr.GetEntities<Building>()
                    .Where(x => x.ClassID == ClassID.CDOTA_BaseNPC_Tower && x.Team == AbilityMain.Me.Team && x.IsAlive)
                    .ToList();
        }

        #endregion

        #region Public Properties

        public static Building GetClosest
        {
            get
            {
                if (!Utils.SleepCheck("GetClosestTower") && closestTower != null && closestTower.IsValid)
                {
                    return closestTower;
                }
                closestTower =
                    Towers.FirstOrDefault(x => x.IsValid && x.IsAlive && x.Distance2D(MyHeroInfo.Position) < 1500);
                Utils.Sleep(250, "GetClosestTower");
                return closestTower;
            }
        }

        #endregion
    }
}