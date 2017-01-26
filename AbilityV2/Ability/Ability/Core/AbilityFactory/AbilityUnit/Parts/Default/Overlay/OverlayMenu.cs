// <copyright file="OverlayMenu.cs" company="EnsageSharp">
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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Bars;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.GetValue;
    using Ability.Core.MenuManager.MenuItems;
    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;

    using Ensage.Common.Menu;

    /// <summary>
    ///     The exploit menu.
    /// </summary>
    [Export(typeof(IUnitMenu))]
    public class OverlayMenu : IUnitMenu
    {
        #region Fields

        /// <summary>
        ///     The menu types.
        /// </summary>
        private readonly Dictionary<Type, Menu> menuTypes = new Dictionary<Type, Menu>();

        #endregion

        #region Constructors and Destructors

        public OverlayMenu()
        {
            this.Menu = new Menu("Overlay", Constants.AssemblyName + nameof(IUnitOverlay));

            //this.DistanceFromLocalHero = new ObservableMenuItem<Slider>(
            //    this.Menu.Name + "maxDistanceFromLocalHero",
            //    "Distance");
            //this.DistanceFromLocalHero.SetValue(new Slider(2000, 500, 5000));
            //this.DistanceFromLocalHero.SetTooltip(
            //    "Distance must be less then specified value in order to draw overlay out of screen");
            ////var added = false;
            //this.DrawOutOfDisplay = new ObservableMenuItem<bool>(
            //    this.Menu.Name + "drawWhenOutOfDisplay",
            //    "Stick to side of screen");
            //this.DrawOutOfDisplay.SetValue(true)
            //    .SetTooltip(
            //        "Stick the overlay to side of screen when distance from your hero is less then specified value");
            //this.Menu.AddItem(this.DistanceFromLocalHero);
            ////this.DrawOutOfDisplay.Provider.Subscribe(
            ////    new DataObserver<bool>(
            ////        b =>
            ////            {
            ////                if (b & !added)
            ////                {
            ////                    this.Menu.AddItem(this.DistanceFromLocalHero);
            ////                    added = true;
            ////                }
            ////                else if (!b & added)
            ////                {
            ////                    this.Menu.Items.Remove(this.DistanceFromLocalHero);
            ////                    added = false;
            ////                }
            ////            }));

            //this.Menu.AddItem(this.DrawOutOfDisplay);
        }

        #endregion

        #region Public Properties

        /// <summary>Gets the distance from local hero.</summary>
        //public ObservableMenuItem<Slider> DistanceFromLocalHero { get; }

        ///// <summary>Gets the draw out of display.</summary>
        //public ObservableMenuItem<bool> DrawOutOfDisplay { get; }

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
            //unit.Overlay.StickToScreen = new GetValue<bool, bool>(this.DrawOutOfDisplay, b => b);
            //unit.Overlay.DistanceFromLocalHero = new GetValue<Slider, float>(
            //    this.DistanceFromLocalHero,
            //    slider => slider.Value);
            foreach (var unitOverlayElement in unit.Overlay.Panels)
            {
                var panelField = unitOverlayElement as PanelField;
                foreach (var overlayElement in panelField.StoredElements)
                {
                    // Console.WriteLine("element: " + overlayElement.GetType());
                    var submenu = this.menuTypes.FirstOrDefault(x => x.Key == overlayElement.GetType()).Value;
                    if (submenu == null && overlayElement.GenerateMenuBool)
                    {
                        this.menuTypes.Add(overlayElement.GetType(), submenu = overlayElement.GenerateMenu(this));
                    }

                    overlayElement.ConnectToMenu(this, submenu);
                }
            }

            foreach (var unitOverlayElement in unit.Overlay.Elements)
            {
                var submenu = this.menuTypes.FirstOrDefault(x => x.Key == unitOverlayElement.GetType()).Value;
                if (submenu == null && unitOverlayElement.GenerateMenuBool)
                {
                    this.menuTypes.Add(unitOverlayElement.GetType(), submenu = unitOverlayElement.GenerateMenu(this));
                }

                unitOverlayElement.ConnectToMenu(this, submenu);
            }
        }

        #endregion
    }
}