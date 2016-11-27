namespace Ability.ObjectManager.Towers
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;

    /// <summary>
    ///     The enemy towers.
    /// </summary>
    internal class EnemyTowers
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="EnemyTowers" /> class.
        /// </summary>
        static EnemyTowers()
        {
            Towers =
                ObjectManager.GetEntities<Building>()
                    .Where(x => x.ClassID == ClassID.CDOTA_BaseNPC_Tower && x.Team == AbilityMain.Me.Team && x.IsAlive)
                    .ToList();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the towers.
        /// </summary>
        public static List<Building> Towers { get; set; }

        #endregion
    }
}