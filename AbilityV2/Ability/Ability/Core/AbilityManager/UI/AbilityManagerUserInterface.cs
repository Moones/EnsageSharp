// <copyright file="AbilityManagerUserInterface.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityManager.UI
{
    using System;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.AbilityManager.UI.Elements.Body;
    using Ability.Core.AbilityManager.UI.Elements.Body.Bodies;
    using Ability.Core.AbilityManager.UI.Elements.Window;
    using Ability.Core.MenuManager;
    using Ability.Core.MenuManager.MenuItems;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Menu;

    using SharpDX;

    /// <summary>
    ///     The ability manager user interface.
    /// </summary>
    public class AbilityManagerUserInterface : IDisposable
    {
        #region Fields

        private Body units;

        private Window window;

        #endregion

        #region Constructors and Destructors

        public AbilityManagerUserInterface(Vector2 position, Vector2 size, IAbilityManager abilityManager)
        {
            AbilityBootstrapper.ComposeParts(this);
            this.units = new Units(size, position, abilityManager);
            this.window = new Window(size, position, "Ability# Manager", this.units);

            var menu = new Menu("AbilityManagerUI", this.MainMenuManager.Value.MainMenu.Name + "abilityManagerUi");
            var enable = new ObservableMenuItem<bool>(menu.Name + "enable", "Enable");
            menu.AddItem(enable.SetValue(false));
            enable.Provider.Subscribe(
                new DataObserver<bool>(
                    b =>
                        {
                            this.window.Visible = b;
                        }));
            this.MainMenuManager.Value.MainMenu.SettingsMenu.AddSubMenu(menu);
            Game.OnWndProc += this.Game_OnWndProc;
            Drawing.OnDraw += this.Drawing_OnDraw;
        }

        #endregion

        /// <summary>Gets or sets the main menu manager.</summary>
        [Import(typeof(IMainMenuManager))]
        protected Lazy<IMainMenuManager> MainMenuManager { get; set; }

        #region Methods

        private void Drawing_OnDraw(EventArgs args)
        {
            if (Game.IsPaused)
            {
                return;
            }

            this.window.Draw();
        }

        /// <summary>
        ///     The game_ on wnd proc.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Game_OnWndProc(WndEventArgs args)
        {
            if (args.Msg == (ulong)Utils.WindowsMessages.WM_LBUTTONDOWN)
            {
                var mousePosition = Game.MouseScreenPosition;
                this.window.MouseDown(mousePosition);
            }
            else if (args.Msg == (ulong)Utils.WindowsMessages.WM_LBUTTONUP)
            {
                var mousePosition = Game.MouseScreenPosition;
                this.window.MouseUp(mousePosition);
            }
            else if (args.Msg == (ulong)Utils.WindowsMessages.WM_MOUSEMOVE)
            {
                var mousePosition = Game.MouseScreenPosition;
                this.window.MouseMove(mousePosition);
            }
        }

        /// <summary>The dispose.</summary>
        public void Dispose()
        {
            Game.OnWndProc -= this.Game_OnWndProc;
            Drawing.OnDraw -= this.Drawing_OnDraw;
        }

        #endregion
    }
}