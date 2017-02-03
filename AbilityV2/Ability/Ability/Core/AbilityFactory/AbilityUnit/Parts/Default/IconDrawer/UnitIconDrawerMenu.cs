// <copyright file="UnitIconDrawerMenu.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.IconDrawer
{
    using System.ComponentModel.Composition;

    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;

    using Ensage.Common;
    using Ensage.Common.Menu;

    using SharpDX;

    /// <summary>The unit icon drawer menu.</summary>
    [Export(typeof(IUnitMenu))]
    internal class UnitIconDrawerMenu : IUnitMenu
    {
        #region Fields

        /// <summary>The size menu item.</summary>
        private readonly MenuItem sizeMenuItem;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="UnitIconDrawerMenu" /> class.</summary>
        internal UnitIconDrawerMenu()
        {
            this.Menu = new Menu("IconDrawer", Constants.AssemblyName + "UnitIconDrawer");
            var reso = "(" + HUDInfo.ScreenSizeX() + "x" + HUDInfo.ScreenSizeY() + ")";
            this.sizeMenuItem =
                this.Menu.AddItem(
                    new MenuItem(
                            this.Menu.Name + nameof(this.sizeMenuItem) + HUDInfo.ScreenSizeY() + HUDInfo.ScreenSizeX(),
                            "Minimap icon size " + reso).SetValue(
                            new Slider((int)(HUDInfo.GetHpBarSizeY() * 1.5), (int)(HUDInfo.GetHpBarSizeY() * 1.5), 64))
                        .SetTooltip("Set minimap icon size for resolution " + reso));
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the menu.</summary>
        public Menu Menu { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The add to menu.</summary>
        /// <param name="parentMenu">The parent menu.</param>
        public void AddToMenu(Menu parentMenu)
        {
            parentMenu.AddSubMenu(this.Menu);
        }

        /// <summary>The connect to unit.</summary>
        /// <param name="unit">The unit.</param>
        public void ConnectToUnit(IAbilityUnit unit)
        {
            unit.IconDrawer.MinimapIconSize = new Vector2(this.sizeMenuItem.GetValue<Slider>().Value);
            this.sizeMenuItem.ValueChanged +=
                (sender, args) => { unit.IconDrawer.MinimapIconSize = new Vector2(args.GetNewValue<Slider>().Value); };
        }

        #endregion
    }
}