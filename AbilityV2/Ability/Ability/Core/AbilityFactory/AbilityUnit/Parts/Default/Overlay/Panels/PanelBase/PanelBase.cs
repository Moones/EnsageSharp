// <copyright file="PanelBase.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.PanelBase
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Bars;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.ObjectPanel;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.MenuItems;
    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;

    using Ensage.Common.Menu;

    using SharpDX;

    /// <summary>
    ///     The panel base.
    /// </summary>
    public abstract class PanelBase : IPanelBase
    {
        #region Fields

        private bool connectedToMenu;

        #endregion

        #region Constructors and Destructors

        protected PanelBase(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the default health bar position.
        /// </summary>
        public virtual int DefaultHealthBarPosition { get; }

        /// <summary>Gets the description.</summary>
        public abstract string Description { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     Gets a value indicating whether generate menu.
        /// </summary>
        public abstract bool GenerateMenuBool { get; }

        /// <summary>
        ///     Gets or sets the menu.
        /// </summary>
        public IPanelMenu Menu { get; set; }

        /// <summary>
        ///     Gets the menu display name.
        /// </summary>
        public abstract string MenuDisplayName { get; }

        /// <summary>
        ///     Gets the menu name.
        /// </summary>
        public abstract string MenuName { get; }

        /// <summary>
        ///     Gets or sets the parent element.
        /// </summary>
        public PanelField Panel { get; set; }

        /// <summary>
        ///     Gets or sets the parent element.
        /// </summary>
        public IUnitOverlayElement ParentElement { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public virtual Vector2 Position { get; set; }

        /// <summary>
        ///     Gets the position from health bar.
        /// </summary>
        public virtual Vector2 PositionFromHealthBar { get; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public virtual Vector2 Size { get; set; }

        /// <summary>
        ///     Gets or sets the size increase.
        /// </summary>
        public virtual float SizeIncrease { get; set; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add submenu.
        /// </summary>
        /// <param name="menu">
        ///     The menu.
        /// </param>
        /// <returns>
        ///     The <see cref="Ensage.Common.Menu.Menu" />.
        /// </returns>
        public virtual Menu AddSubmenu(IUnitMenu menu)
        {
            return menu.Menu.AddSubMenu(this.Menu as Menu);
        }

        public void ChangePosition(PanelDirection direction)
        {
            this.Panel?.RemoveElement(this);
            switch (direction)
            {
                case PanelDirection.Bottom:
                    this.Unit.Overlay.BotPanel.AddElement(this);
                    break;
                case PanelDirection.Left:
                    this.Unit.Overlay.LeftPanel.AddElement(this);
                    break;
                case PanelDirection.Right:
                    this.Unit.Overlay.RightPanel.AddElement(this);
                    break;
                case PanelDirection.Top:
                    this.Unit.Overlay.TopPanel.AddElement(this);
                    break;
            }
        }

        public virtual void ConnectSizeIncreaseToMenu()
        {
            this.Menu.SizeIncreaseMenuItem.Provider.Subscribe(
                new DataObserver<Slider>(slider => { this.SizeIncrease = (float)slider.Value / 20; }));
        }

        /// <summary>
        ///     The connect to menu.
        /// </summary>
        /// <param name="menu">
        ///     The menu.
        /// </param>
        public virtual void ConnectToMenu(IUnitMenu menu, Menu subMenu)
        {
            if (this.connectedToMenu)
            {
                return;
            }

            this.connectedToMenu = true;
            this.Menu = subMenu as IPanelMenu;
            if (this.Menu != null)
            {
                this.Menu.EnableMenuItem.Provider.Subscribe(new DataObserver<bool>(this.EnabledAction()));
                this.ConnectSizeIncreaseToMenu();
                this.Menu.HealthBarPositionMenuItem.Provider.Subscribe(
                    new DataObserver<StringList>(
                        list =>
                            {
                                this.HealthBarPositionAction().Invoke(list);
                                this.ChangePosition((PanelDirection)list.SelectedIndex);
                                this.Panel?.UpdateSize();
                            }));
            }
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        public abstract void Draw();

        /// <summary>
        ///     The enabled action.
        /// </summary>
        /// <returns>
        ///     The <see cref="Action" />.
        /// </returns>
        public virtual Action<bool> EnabledAction()
        {
            return b => this.Enabled = b;
        }

        /// <summary>
        ///     The generate enable.
        /// </summary>
        public void GenerateEnable()
        {
            this.Menu.EnableMenuItem = new ObservableMenuItem<bool>(
                this.MenuName + nameof(this.Menu.EnableMenuItem),
                "Enable");
            this.Menu.EnableMenuItem.SetValue(true);
        }

        /// <summary>
        ///     The generate health bar position.
        /// </summary>
        public virtual void GenerateHealthBarPosition()
        {
            this.Menu.HealthBarPositionMenuItem =
                new ObservableMenuItem<StringList>(
                    this.MenuName + nameof(this.Menu.HealthBarPositionMenuItem),
                    "Position");
            this.Menu.HealthBarPositionMenuItem.SetValue(
                new StringList(new[] { "Top", "Bottom", "Left", "Right" }, this.DefaultHealthBarPosition));
        }

        /// <summary>
        ///     The generate panel menu.
        /// </summary>
        public void GeneratePanelMenu()
        {
            this.Menu = new ObjectPanelMenu(this.MenuDisplayName, this.MenuName);
        }

        /// <summary>
        ///     The generate size increase.
        /// </summary>
        public virtual void GenerateSizeIncrease()
        {
            this.Menu.SizeIncreaseMenuItem =
                new ObservableMenuItem<Slider>(this.MenuName + nameof(this.Menu.SizeIncreaseMenuItem), "Size");
            this.Menu.SizeIncreaseMenuItem.SetValue(new Slider(15, 7, 21));
        }

        /// <summary>
        ///     The health bar position action.
        /// </summary>
        /// <returns>
        ///     The <see cref="Action" />.
        /// </returns>
        public virtual Action<StringList> HealthBarPositionAction()
        {
            return list => { };
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        ///     The generate menu.
        /// </summary>
        /// <returns>
        ///     The <see cref="Ensage.Common.Menu.Menu" />.
        /// </returns>
        Menu IUnitOverlayElement.GenerateMenu(IUnitMenu menu)
        {
            this.GeneratePanelMenu();
            this.GenerateEnable();
            this.GenerateSizeIncrease();
            this.GenerateHealthBarPosition();

            var subMenu = this.AddSubmenu(menu);
            subMenu.AddItem(this.Menu.EnableMenuItem);
            if (this.Menu.SizeIncreaseMenuItem != null)
            {
                subMenu.AddItem(this.Menu.SizeIncreaseMenuItem);
            }

            subMenu.AddItem(this.Menu.HealthBarPositionMenuItem);

            return subMenu;
        }

        #endregion
    }
}