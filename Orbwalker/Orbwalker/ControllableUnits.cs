namespace Orbwalker
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common.Objects.UtilityObjects;

    /// <summary>
    ///     The controllable units.
    /// </summary>
    public class ControllableUnits
    {
        #region Fields

        /// <summary>
        ///     The sleeper.
        /// </summary>
        private readonly Sleeper sleeper;

        /// <summary>
        ///     The team.
        /// </summary>
        private readonly Team team;

        /// <summary>
        ///     The units.
        /// </summary>
        private List<Unit> units;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ControllableUnits" /> class.
        /// </summary>
        /// <param name="team">
        ///     The team.
        /// </param>
        public ControllableUnits(Team team)
        {
            this.team = team;
            this.sleeper = new Sleeper();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the units.
        /// </summary>
        public List<Unit> Units
        {
            get
            {
                if (this.sleeper.Sleeping)
                {
                    return this.units;
                }

                this.units =
                    ObjectManager.GetEntities<Unit>()
                        .Where(x => !(x is Courier) && x.Team == this.team && x.IsControllable)
                        .ToList();
                this.sleeper.Sleep(1000);
                return this.units;
            }
        }

        #endregion
    }
}