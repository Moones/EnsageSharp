// <copyright file="SettingsMenu.cs" company="EnsageSharp">
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
namespace Ability.Core.MenuManager.Menus
{
    using Ensage.Common.Menu;

    /// <summary>
    ///     The settings menu.
    /// </summary>
    public class SettingsMenu : Menu
    {
        #region Fields

        /// <summary>
        ///     The orbwalker cancel delay.
        /// </summary>
        private readonly MenuItem orbwalkerCancelDelay;

        #endregion

        #region Constructors and Destructors

        public SettingsMenu()
            : base("Settings", "Ability#.Settings")
        {
            // var menu = new Menu("Orbwalker", Constants.AssemblyName + "Orbwalker");
            // this.orbwalkerCancelDelay =
            // menu.AddItem(
            // new MenuItem(Constants.AssemblyName + nameof(this.orbwalkerCancelDelay), "Animation cancel delay")
            // .SetValue(new Slider(0, -200, 200)));
            // this.orbwalkerCancelDelay.ValueChanged +=
            // (sender, args) => { this.OrbwalkerCancelDelay = args.GetNewValue<Slider>().Value; };
            // this.OrbwalkerCancelDelay = this.orbwalkerCancelDelay.GetValue<Slider>().Value;
            // var debugMenu = new Menu("Debug", Constants.AssemblyName + "Debug");
            // this.AddSubMenu(debugMenu);
            // this.ShowDamage =
            // debugMenu.AddItem(
            // new OnOffSlider(Constants.AssemblyName + "showDamage", "(Debug) Show ability damage", false));
            // this.AddSubMenu(menu);
            this.Units = new Menu("Units", Constants.AssemblyName + " Units");
            this.AddSubMenu(this.Units);
            this.Services = new Menu("Services", Constants.AssemblyName + " Services");
            this.AddSubMenu(this.Services);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the orbwalker cancel delay.
        /// </summary>
        public float OrbwalkerCancelDelay { get; private set; }

        /// <summary>
        ///     Gets or sets the services.
        /// </summary>
        public Menu Services { get; set; }

        /// <summary>
        ///     Gets a value indicating whether show damage.
        /// </summary>
        public MenuItem ShowDamage { get; private set; }

        /// <summary>
        ///     Gets or sets the units.
        /// </summary>
        public Menu Units { get; set; }

        #endregion
    }
}