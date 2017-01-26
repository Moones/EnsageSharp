// <copyright file="AbilityTeam.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Core.AbilityFactory.AbilityTeam
{
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityTeam.Parts.UnitManager;
    using Ability.Core.AbilityFactory.AbilityUnit;

    using Ensage;

    /// <summary>
    ///     The ability team.
    /// </summary>
    public class AbilityTeam : IAbilityTeam
    {
        #region Constructors and Destructors

        public AbilityTeam(Team team)
        {
            this.Name = team;
            this.UnitManager = new UnitManager(this);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public Team Name { get; set; }

        /// <summary>
        ///     Gets or sets the other teams.
        /// </summary>
        public ICollection<IAbilityTeam> OtherTeams { get; set; } = new List<IAbilityTeam>();

        /// <summary>
        ///     Gets or sets the unit manager.
        /// </summary>
        public IUnitManager UnitManager { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            this.UnitManager.Units = new Dictionary<double, IAbilityUnit>();
            this.UnitManager = null;
        }

        #endregion
    }
}