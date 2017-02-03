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

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.PanelFields;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.AbilityManager;
    using Ability.Core.MenuManager.GetValue;
    using Ability.Core.MenuManager.MenuItems;
    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;

    using Ensage.Common;
    using Ensage.Common.Menu;

    using SharpDX;

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

            this.EnableOverlay = new ObservableMenuItem<bool>(this.Menu.Name + "enableOverlay", "Enable Overlay");
            this.EnableOverlay.SetValue(true);
            this.EnableOverlay.SetFontColor(Color.GreenYellow);
            this.EnableOverlay.Provider.Subscribe(
                new DataObserver<bool>(
                    (enable) =>
                        {
                            if (this.AbilityManager == null)
                            {
                                if (!enable)
                                {
                                    DelayAction.Add(
                                        100,
                                        () =>
                                            {
                                                foreach (var keyValuePair in this.AbilityManager.Value.Units)
                                                {
                                                    var overlay = keyValuePair.Value.GetPart<IUnitOverlay>();
                                                    if (overlay == null)
                                                    {
                                                        return;
                                                    }

                                                    keyValuePair.Value.GetPart<IUnitOverlay>().Dispose();
                                                    keyValuePair.Value.RemovePart<IUnitOverlay>();
                                                    keyValuePair.Value.Overlay = null;
                                                }
                                            });
                                }

                                return;
                            }

                            if (!enable)
                            {
                                foreach (var keyValuePair in this.AbilityManager.Value.Units)
                                {
                                    var overlay = keyValuePair.Value.GetPart<IUnitOverlay>();
                                    if (overlay == null)
                                    {
                                        return;
                                    }

                                    keyValuePair.Value.GetPart<IUnitOverlay>().Dispose();
                                    keyValuePair.Value.RemovePart<IUnitOverlay>();
                                    keyValuePair.Value.Overlay = null;
                                }
                            }
                            else
                            {
                                foreach (var keyValuePair in this.AbilityManager.Value.Units)
                                {
                                    var overlay = keyValuePair.Value.GetPart<IUnitOverlay>();
                                    if (overlay != null)
                                    {
                                        return;
                                    }

                                    keyValuePair.Value.UnitComposer.Assignments[typeof(IUnitOverlay)].Invoke(
                                        keyValuePair.Value);
                                    this.SetValues(keyValuePair.Value);
                                    keyValuePair.Value.GetPart<IUnitOverlay>().Initialize();
                                    this.ConnectPanels(keyValuePair.Value);
                                }
                            }
                        }));

            this.Menu.AddItem(this.EnableOverlay);

            this.DistanceFromLocalHero = new ObservableMenuItem<Slider>(
                this.Menu.Name + "maxDistanceFromLocalHero",
                "Max distance from local hero");
            this.DistanceFromLocalHero.SetValue(new Slider(2000, 500, 5000));
            this.DistanceFromLocalHero.SetTooltip(
                "If unit distance from your hero is less then specified value, overlay will stick to screen edge");

            // var added = false;
            this.DrawOutOfDisplay = new ObservableMenuItem<bool>(
                this.Menu.Name + "drawWhenOutOfDisplay",
                "Stick to screen edges");
            this.DrawOutOfDisplay.SetValue(true).SetTooltip("Stick the overlay to screen edges when its out of view");
            this.Menu.AddItem(this.DrawOutOfDisplay);
            this.Menu.AddItem(this.DistanceFromLocalHero);

            this.DistanceFromScreenEdge = new ObservableMenuItem<Slider>(
                this.Menu.Name + "maxDistanceFromEdge",
                "Max distance from camera position");
            this.DistanceFromScreenEdge.SetValue(new Slider((int)(HUDInfo.ScreenSizeY() * 2), 0, 5000));
            this.DistanceFromScreenEdge.SetTooltip(
                "If unit distance from camera position is less then specified value, overlay will stick to screen edge");
            this.Menu.AddItem(this.DistanceFromScreenEdge);

            // this.DrawOutOfDisplay.Provider.Subscribe(
            // new DataObserver<bool>(
            // b =>
            // {
            // if (b & !added)
            // {
            // this.Menu.AddItem(this.DistanceFromLocalHero);
            // added = true;
            // }
            // else if (!b & added)
            // {
            // this.Menu.Items.Remove(this.DistanceFromLocalHero);
            // added = false;
            // }
            // }));
        }

        #endregion

        #region Public Properties

        /// <summary>Gets the distance from local hero.</summary>
        public ObservableMenuItem<Slider> DistanceFromLocalHero { get; }

        /// <summary>Gets the distance from screen edge.</summary>
        public ObservableMenuItem<Slider> DistanceFromScreenEdge { get; }

        /// <summary>Gets the draw out of display.</summary>
        public ObservableMenuItem<bool> DrawOutOfDisplay { get; }

        public ObservableMenuItem<bool> EnableOverlay { get; }

        /// <summary>
        ///     Gets or sets the menu.
        /// </summary>
        public Menu Menu { get; set; }

        #endregion

        #region Properties

        [Import(typeof(IAbilityManager))]
        protected Lazy<IAbilityManager> AbilityManager { get; set; }

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
            this.SetValues(unit);

            this.ConnectPanels(unit);
        }

        #endregion

        #region Methods

        /// <summary>The connect panels.</summary>
        /// <param name="unit">The unit.</param>
        private void ConnectPanels(IAbilityUnit unit)
        {
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

        /// <summary>The set values.</summary>
        /// <param name="unit">The unit.</param>
        private void SetValues(IAbilityUnit unit)
        {
            unit.Overlay.StickToScreen = new GetValue<bool, bool>(this.DrawOutOfDisplay, b => b);
            unit.Overlay.DistanceFromLocalHero = new GetValue<Slider, float>(
                this.DistanceFromLocalHero,
                slider => slider.Value);
            unit.Overlay.DistanceFromScreen = new GetValue<Slider, float>(
                this.DistanceFromScreenEdge,
                slider => slider.Value);
        }

        #endregion
    }
}