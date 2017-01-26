// <copyright file="IModifiers.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Modifiers
{
    using Ensage;

    /// <summary>
    ///     The Modifiers interface.
    /// </summary>
    public interface IModifiers : IAbilityUnitPart
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the last added modifier.
        /// </summary>
        ModifierAdd LastAddedModifier { get; set; }

        /// <summary>
        ///     Gets or sets the last removed modifier.
        /// </summary>
        ModifierRemove LastRemovedModifier { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The modifier removed.
        /// </summary>
        /// <param name="modifier">
        ///     The modifier.
        /// </param>
        void ModifierRemoved(Modifier modifier);

        /// <summary>
        ///     The new modifier received.
        /// </summary>
        /// <param name="modifier">
        ///     The modifier.
        /// </param>
        void NewModifierReceived(Modifier modifier);

        #endregion
    }
}