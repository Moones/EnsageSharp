// <copyright file="IUnitOverlayElement.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Bars;
    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;

    using Ensage.Common.Menu;

    using SharpDX;

    /// <summary>
    ///     The HealthBarBounding interface.
    /// </summary>
    public interface IUnitOverlayElement
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether enabled.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        ///     Gets a value indicating whether generate menu.
        /// </summary>
        bool GenerateMenuBool { get; }

        /// <summary>
        ///     Gets or sets the parent element.
        /// </summary>
        PanelField Panel { get; set; }

        /// <summary>
        ///     Gets or sets the parent element.
        /// </summary>
        IUnitOverlayElement ParentElement { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        ///     Gets the position from health bar.
        /// </summary>
        Vector2 PositionFromHealthBar { get; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        Vector2 Size { get; set; }

        /// <summary>
        ///     Gets or sets the size increase.
        /// </summary>
        float SizeIncrease { get; set; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add submenu.
        /// </summary>
        /// <param name="menu">
        ///     The menu.
        /// </param>
        /// <returns>
        ///     The <see cref="Menu" />.
        /// </returns>
        Menu AddSubmenu(IUnitMenu menu);

        /// <summary>
        ///     The connect to menu.
        /// </summary>
        /// <param name="menu">
        ///     The menu.
        /// </param>
        /// <param name="subMenu">
        ///     The sub Menu.
        /// </param>
        void ConnectToMenu(IUnitMenu menu, Menu subMenu);

        /// <summary>
        ///     The draw.
        /// </summary>
        void Draw();

        /// <summary>
        ///     The generate menu.
        /// </summary>
        /// <param name="menu">
        ///     The menu.
        /// </param>
        /// <returns>
        ///     The <see cref="Menu" />.
        /// </returns>
        Menu GenerateMenu(IUnitMenu menu);

        #endregion
    }
}