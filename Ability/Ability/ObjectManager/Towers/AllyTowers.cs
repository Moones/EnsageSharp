namespace Ability.ObjectManager.Towers
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    /// <summary>
    ///     The ally towers.
    /// </summary>
    internal class AllyTowers
    {
        #region Static Fields

        /// <summary>
        ///     The towers.
        /// </summary>
        public static List<Building> Towers;

        /// <summary>
        ///     The closest tower.
        /// </summary>
        private static Building closestTower;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="AllyTowers" /> class.
        /// </summary>
        static AllyTowers()
        {
            Towers =
                ObjectManager.GetEntities<Building>()
                    .Where(x => x.ClassID == ClassID.CDOTA_BaseNPC_Tower && x.Team == AbilityMain.Me.Team && x.IsAlive)
                    .ToList();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the get closest.
        /// </summary>
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