// <copyright file="IPanelMenu.cs" company="EnsageSharp">
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
    using Ability.Core.MenuManager.MenuItems;

    using Ensage.Common.Menu;

    /// <summary>
    ///     The PanelMenu interface.
    /// </summary>
    public interface IPanelMenu
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the enable menu item.
        /// </summary>
        ObservableMenuItem<bool> EnableMenuItem { get; set; }

        /// <summary>
        ///     Gets or sets the health bar position menu item.
        /// </summary>
        ObservableMenuItem<StringList> HealthBarPositionMenuItem { get; set; }

        /// <summary>
        ///     Gets or sets the size increase menu item.
        /// </summary>
        ObservableMenuItem<Slider> SizeIncreaseMenuItem { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add item.
        /// </summary>
        /// <param name="item">
        ///     The item.
        /// </param>
        /// <returns>
        ///     The <see cref="MenuItem" />.
        /// </returns>
        MenuItem AddObservableItem(MenuItem item);

        #endregion
    }
}