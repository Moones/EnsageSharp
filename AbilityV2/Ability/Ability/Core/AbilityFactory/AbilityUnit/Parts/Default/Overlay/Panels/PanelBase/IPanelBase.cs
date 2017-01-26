// <copyright file="IPanelBase.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.PanelBase
{
    /// <summary>
    ///     The PanelBase interface.
    /// </summary>
    public interface IPanelBase : IUnitOverlayElement
    {
        #region Public Properties

        /// <summary>
        ///     Gets the default health bar position.
        /// </summary>
        int DefaultHealthBarPosition { get; }

        /// <summary>Gets the description.</summary>
        string Description { get; }

        /// <summary>
        ///     Gets or sets the menu.
        /// </summary>
        IPanelMenu Menu { get; set; }

        /// <summary>
        ///     Gets the menu display name.
        /// </summary>
        string MenuDisplayName { get; }

        /// <summary>
        ///     Gets the menu name.
        /// </summary>
        string MenuName { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The generate enable.
        /// </summary>
        void GenerateEnable();

        /// <summary>
        ///     The generate health bar position.
        /// </summary>
        void GenerateHealthBarPosition();

        /// <summary>
        ///     The generate size increase.
        /// </summary>
        void GenerateSizeIncrease();

        #endregion
    }
}