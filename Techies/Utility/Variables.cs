namespace Techies.Utility
{
    using System.Collections.Generic;

    using Ensage;

    using global::Techies.Classes;
    using global::Techies.Modules;
    using global::Techies.Modules.ForceStaff;
    using global::Techies.Modules.LandMines;
    using global::Techies.Modules.RemoteMines;
    using global::Techies.Modules.StatisTraps;
    using global::Techies.Modules.SuicideSquadAttack;

    /// <summary>
    ///     The variables.
    /// </summary>
    internal static class Variables
    {
        #region Static Fields

        /// <summary>
        ///     The modules.
        /// </summary>
        public static List<ITechiesModule> Modules = new List<ITechiesModule>
                                                         {
                                                             new AutoDetonateHeroes(), new AutoDetonateCreeps(), 
                                                             new AutoForceStaff(), new AutoSuicide(), 
                                                             new ManageRemoteMines(), new AutoProtection(), 
                                                             new ManageLandMines(), new ManageStasisTraps()
                                                         };

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the damage.
        /// </summary>
        public static Damage Damage { get; set; }

        /// <summary>
        ///     Gets or sets the enemy team.
        /// </summary>
        public static Team EnemyTeam { get; set; }

        /// <summary>
        ///     Gets or sets the force staff.
        /// </summary>
        public static Item ForceStaff { get; set; }

        /// <summary>
        ///     Gets or sets the instance.
        /// </summary>
        public static Bootstrap Instance { get; set; }

        /// <summary>
        ///     Gets or sets the land mines.
        /// </summary>
        public static List<LandMine> LandMines { get; set; }

        /// <summary>
        ///     Gets or sets the land mines ability.
        /// </summary>
        public static Ability LandMinesAbility { get; set; }

        /// <summary>
        ///     Gets or sets the menu.
        /// </summary>
        public static TechiesMenu Menu { get; set; }

        /// <summary>
        ///     Gets or sets the remote mines.
        /// </summary>
        public static List<RemoteMine> RemoteMines { get; set; }

        /// <summary>
        ///     Gets or sets the remote mines ability.
        /// </summary>
        public static Ability RemoteMinesAbility { get; set; }

        /// <summary>
        ///     Gets or sets the stacks.
        /// </summary>
        public static List<Stack> Stacks { get; set; }

        /// <summary>
        ///     Gets or sets the stasis trap ability.
        /// </summary>
        public static Ability StasisTrapAbility { get; set; }

        /// <summary>
        ///     Gets or sets the stasis traps.
        /// </summary>
        public static List<StasisTrap> StasisTraps { get; set; }

        /// <summary>
        ///     Gets or sets the suicide ability.
        /// </summary>
        public static Ability SuicideAbility { get; set; }

        /// <summary>
        ///     Gets or sets the techies.
        /// </summary>
        public static Hero Techies { get; set; }

        #endregion
    }
}