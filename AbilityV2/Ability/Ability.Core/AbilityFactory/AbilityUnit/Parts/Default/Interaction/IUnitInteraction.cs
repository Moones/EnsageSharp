// <copyright file="IUnitInteraction.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Interaction
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Modifiers;

    /// <summary>
    ///     The UnitInteraction interface.
    /// </summary>
    public interface IUnitInteraction : IAbilityUnitPart
    {
        #region Public Properties

        /// <summary>
        ///     Gets the allies to interact with.
        /// </summary>
        // Dictionary<IAbilityUnit, UnitInteractionWorker> AlliesToInteractWith { get; }

        ///// <summary>
        /////     Gets the ally interaction provider.
        ///// </summary>
        // DataProvider<UnitInteractionData> AllyInteractionProvider { get; }

        ///// <summary>
        /////     Gets the units to interact with.
        ///// </summary>
        // Dictionary<IAbilityUnit, UnitInteractionWorker> EnemiesToInteractWith { get; }

        ///// <summary>
        /////     Gets the enemy interaction provider.
        ///// </summary>
        // DataProvider<UnitInteractionData> EnemyInteractionProvider { get; }
        /// <summary>
        ///     Gets a value indicating whether is attackable.
        /// </summary>
        bool IsAttackable { get; }

        #endregion
    }
}