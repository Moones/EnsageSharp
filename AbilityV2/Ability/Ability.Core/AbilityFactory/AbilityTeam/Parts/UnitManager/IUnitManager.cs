// <copyright file="IUnitManager.cs" company="EnsageSharp">
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
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    /// <summary>
    ///     The UnitManager interface.
    /// </summary>
    public interface IUnitManager : IAbilityTeamPart
    {
        #region Public Properties

        /// <summary>
        ///     Gets the unit added.
        /// </summary>
        DataProvider<IAbilityUnit> UnitAdded { get; }

        /// <summary>
        ///     Gets the unit removed.
        /// </summary>
        DataProvider<IAbilityUnit> UnitRemoved { get; }

        /// <summary>
        ///     Gets or sets the units.
        /// </summary>
        Dictionary<double, IAbilityUnit> Units { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add unit.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        void AddUnit(IAbilityUnit unit);

        /// <summary>
        ///     The remove unit.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        void RemoveUnit(IAbilityUnit unit);

        #endregion
    }
}