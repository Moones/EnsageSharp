// <copyright file="MainMenuManager.cs" company="EnsageSharp">
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
namespace Ability.Core.MenuManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityManager;
    using Ability.Core.MenuManager.Menus;
    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;
    using Ability.Utilities;

    using Ensage.Common.Menu;

    /// <summary>
    ///     The main menu manager.
    /// </summary>
    [Export(typeof(IMainMenuManager))]
    public class MainMenuManager : IMainMenuManager
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainMenuManager" /> class.
        /// </summary>
        public MainMenuManager()
        {
            Logging.Write()(LogLevel.Info, "MainMenuManager Initialized");
        }

        #endregion

        #region Public Properties

        /// <summary>Gets a value indicating whether generate menu.</summary>
        public bool GenerateMenu { get; } = false;

        /// <summary>
        ///     Gets the main menu.
        /// </summary>
        public MainMenu MainMenu { get; } = new MainMenu();

        #endregion

        #region Properties

        /// <summary>Gets or sets the ability services.</summary>
        [ImportMany]
        internal IEnumerable<Lazy<IAbilityService>> AbilityServices { get; set; }

        /// <summary>
        ///     Gets or sets the ability unit manager.
        /// </summary>
        [Import(typeof(IAbilityManager))]
        internal Lazy<IAbilityManager> AbilityUnitManager { get; set; }

        /// <summary>Gets or sets the hero menus.</summary>
        [ImportMany]
        internal IEnumerable<Lazy<IHeroMenu>> HeroMenus { get; set; }

        /// <summary>
        ///     Gets or sets the unit menus.
        /// </summary>
        [ImportMany]
        internal IEnumerable<Lazy<IUnitMenu>> UnitMenus { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The menu.</summary>
        /// <returns>The <see cref="Menu" />.</returns>
        public Menu Menu()
        {
            return null;
        }

        /// <summary>
        ///     The on close.
        /// </summary>
        public void OnClose()
        {
            this.MainMenu.RemoveFromMainMenu();
        }

        /// <summary>
        ///     The on load.
        /// </summary>
        public void OnLoad()
        {
            this.MainMenu.AddToMainMenu();

            foreach (var heroMenu in this.HeroMenus)
            {
                heroMenu.Value.AddToMenu(this.MainMenu.SettingsMenu.Units);
                foreach (var keyValuePair in this.AbilityUnitManager.Value.Units)
                {
                    heroMenu.Value.ConnectToUnit(keyValuePair.Value);
                }
            }

            foreach (var unitMenu in this.UnitMenus)
            {
                unitMenu.Value.AddToMenu(this.MainMenu.SettingsMenu.Units);
                foreach (var keyValuePair in this.AbilityUnitManager.Value.Units)
                {
                    unitMenu.Value.ConnectToUnit(keyValuePair.Value);
                }
            }

            foreach (var abilityService in this.AbilityServices)
            {
                if (!abilityService.Value.GenerateMenu)
                {
                    continue;
                }

                this.MainMenu.SettingsMenu.Services.AddSubMenu(abilityService.Value.Menu());
            }

            this.AbilityUnitManager.Value.UnitAdded += this.Value_UnitAdded;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The value_ unit added.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Value_UnitAdded(UnitEventArgs args)
        {
            foreach (var heroMenu in this.HeroMenus)
            {
                heroMenu.Value.ConnectToUnit(args.AbilityUnit);
            }

            foreach (var unitMenu in this.UnitMenus)
            {
                unitMenu.Value.ConnectToUnit(args.AbilityUnit);
            }
        }

        #endregion
    }
}