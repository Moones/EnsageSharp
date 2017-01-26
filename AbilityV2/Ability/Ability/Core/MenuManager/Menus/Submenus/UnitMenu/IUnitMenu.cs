// <copyright file="IUnitMenu.cs" company="EnsageSharp">
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
namespace Ability.Core.MenuManager.Menus.Submenus.UnitMenu
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    using Ensage.Common.Menu;

    /// <summary>
    ///     The UnitMenu interface.
    /// </summary>
    public interface IUnitMenu
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the menu.
        /// </summary>
        Menu Menu { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add to menu.
        /// </summary>
        /// <param name="parentMenu">
        ///     The parent menu.
        /// </param>
        void AddToMenu(Menu parentMenu);

        /// <summary>
        ///     The connect to unit.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        void ConnectToUnit(IAbilityUnit unit);

        #endregion
    }
}