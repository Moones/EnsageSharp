// <copyright file="ObjectPanelMenu.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.ObjectPanel
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.PanelBase;
    using Ability.Core.MenuManager.MenuItems;

    using Ensage.Common.Menu;

    /// <summary>
    ///     The object panel menu.
    /// </summary>
    public class ObjectPanelMenu : Menu, IPanelMenu
    {
        #region Constructors and Destructors

        public ObjectPanelMenu(
            string displayName,
            string name,
            bool isRootMenu = false,
            string textureName = null,
            bool showTextWithTexture = false)
            : base(displayName, name, isRootMenu, textureName, showTextWithTexture)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the enable menu item.
        /// </summary>
        public ObservableMenuItem<bool> EnableMenuItem { get; set; }

        /// <summary>
        ///     Gets or sets the health bar position menu item.
        /// </summary>
        public ObservableMenuItem<StringList> HealthBarPositionMenuItem { get; set; }

        /// <summary>
        ///     Gets or sets the size increase menu item.
        /// </summary>
        public ObservableMenuItem<Slider> SizeIncreaseMenuItem { get; set; }

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
        public MenuItem AddObservableItem(MenuItem item)
        {
            return this.AddItem(item);
        }

        #endregion
    }
}