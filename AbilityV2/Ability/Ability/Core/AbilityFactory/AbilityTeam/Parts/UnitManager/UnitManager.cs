// <copyright file="UnitManager.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityTeam.Parts.UnitManager
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage.Common;

    /// <summary>
    ///     The unit manager.
    /// </summary>
    public class UnitManager : IUnitManager
    {
        #region Constructors and Destructors

        public UnitManager(IAbilityTeam team)
        {
            this.Team = team;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the team.
        /// </summary>
        public IAbilityTeam Team { get; set; }

        /// <summary>
        ///     Gets the unit added.
        /// </summary>
        public DataProvider<IAbilityUnit> UnitAdded { get; } = new DataProvider<IAbilityUnit>();

        /// <summary>
        ///     Gets the unit removed.
        /// </summary>
        public DataProvider<IAbilityUnit> UnitRemoved { get; } = new DataProvider<IAbilityUnit>();

        /// <summary>
        ///     Gets or sets the units.
        /// </summary>
        public Dictionary<double, IAbilityUnit> Units { get; set; } = new Dictionary<double, IAbilityUnit>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add unit.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        public void AddUnit(IAbilityUnit unit)
        {
            unit.Team = this.Team;
            this.Units.Add(unit.UnitHandle, unit);
            DelayAction.Add(-1, () => this.UnitAdded.Next(unit));
        }

        /// <summary>
        ///     The remove unit.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        public void RemoveUnit(IAbilityUnit unit)
        {
            this.Units.Remove(unit.UnitHandle);
            this.UnitRemoved.Next(unit);
        }

        #endregion
    }
}