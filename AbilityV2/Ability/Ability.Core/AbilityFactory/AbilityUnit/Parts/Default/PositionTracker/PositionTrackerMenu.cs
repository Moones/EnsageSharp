// <copyright file="PositionTrackerMenu.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker
{
    using System.ComponentModel.Composition;

    using Ability.Core.MenuManager.GetValue;
    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;

    using Ensage.Common.Menu;

    using SharpDX;

    /// <summary>
    ///     The position tracker menu.
    /// </summary>
    //[Export(typeof(IUnitMenu))]
    internal class PositionTrackerMenu : IUnitMenu
    {
        #region Fields

        private MenuItem drawonmap;

        private MenuItem drawonminimap;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="PositionTrackerMenu" /> class.</summary>
        internal PositionTrackerMenu()
        {
            this.Menu = new Menu("PositionTracker", Constants.AssemblyName + "PositionTracker");
            this.Menu.AddItem(
                new MenuItem(this.Menu.Name + "description", "Position Tracker").SetTooltip(
                    "Uses various methods to track position of enemy heroes in fog of war"));
            this.Menu.AddItem(
                    new MenuItem(this.Menu.Name + "notification", "Minimap drawing requires -dx9").SetTooltip(
                        "You need to go to steam>library>right click on dota2>properties>set launch options and input -dx9"))
                .SetFontColor(Color.OrangeRed);

            this.drawonmap =
                this.Menu.AddItem(new MenuItem(this.Menu.Name + "drawonmap", "Draw on world").SetValue(true));
            this.drawonminimap =
                this.Menu.AddItem(new MenuItem(this.Menu.Name + "drawonminimap", "Draw on Minimap").SetValue(true));
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the menu.
        /// </summary>
        public Menu Menu { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add to menu.
        /// </summary>
        /// <param name="parentMenu">
        ///     The parent menu.
        /// </param>
        public void AddToMenu(Menu parentMenu)
        {
            parentMenu.AddSubMenu(this.Menu);
        }

        /// <summary>
        ///     The connect to unit.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        public void ConnectToUnit(IAbilityUnit unit)
        {
            //unit.PositionTracker.Menu = this.Menu;
            //unit.PositionTracker.DrawOnMap = new GetValue<bool, bool>(this.drawonmap, b => b);
            //unit.PositionTracker.DrawOnMinimap = new GetValue<bool, bool>(this.drawonminimap, b => b);
        }

        #endregion
    }
}