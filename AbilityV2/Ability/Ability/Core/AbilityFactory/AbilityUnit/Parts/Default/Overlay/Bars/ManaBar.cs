// <copyright file="ManaBar.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Bars
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Mana;
    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;

    using Ensage;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>
    ///     The mana bar.
    /// </summary>
    public class ManaBar : DrawObject, IBar, IObserver<IMana>
    {
        #region Fields

        /// <summary>
        ///     The bg pos.
        /// </summary>
        private Vector2 barPos;

        /// <summary>
        ///     The bg size.
        /// </summary>
        private Vector2 barSize;

        /// <summary>
        ///     The fill.
        /// </summary>
        private float fill;

        /// <summary>
        ///     The fill size.
        /// </summary>
        private Vector2 fillSize;

        /// <summary>
        ///     The pos.
        /// </summary>
        private Vector2 pos;

        /// <summary>
        ///     The size.
        /// </summary>
        private Vector2 size;

        #endregion

        #region Constructors and Destructors

        public ManaBar(IAbilityUnit unit, Vector2 size)
        {
            this.Unit = unit;
            this.Unit.Mana.Subscribe(this);
            this.Size = size;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the background color.
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     Gets or sets the fill percentage.
        /// </summary>
        public float FillPercentage
        {
            get
            {
                return this.fill;
            }

            set
            {
                this.fill = value;
                this.fillSize = new Vector2(this.barSize.X / 100 * this.fill, this.barSize.Y);
            }
        }

        /// <summary>
        ///     Gets a value indicating whether generate menu.
        /// </summary>
        public bool GenerateMenuBool { get; }

        /// <summary>
        ///     Gets the parent element.
        /// </summary>
        public PanelField Panel { get; set; }

        /// <summary>
        ///     Gets or sets the parent element.
        /// </summary>
        public IUnitOverlayElement ParentElement { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public override Vector2 Position
        {
            get
            {
                return this.pos;
            }

            set
            {
                this.pos = value;
                this.barPos = this.pos + new Vector2(this.size.X / 40, 0);
            }
        }

        /// <summary>
        ///     Gets or sets the position from health bar.
        /// </summary>
        public Vector2 PositionFromHealthBar => new Vector2(0, this.Unit.ScreenInfo.HealthBarSize.Y);

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public override Vector2 Size
        {
            get
            {
                return this.size;
            }

            set
            {
                this.size = value;
                this.barSize = this.size - new Vector2(this.size.X / 20, this.size.X / 40);
                this.fillSize = new Vector2(this.barSize.X / 100 * this.fill, this.barSize.Y);
            }
        }

        /// <summary>
        ///     Gets or sets the size increase.
        /// </summary>
        public float SizeIncrease { get; set; }

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
        ///     The <see cref="Menu" />.
        /// </returns>
        public Menu AddSubmenu(IUnitMenu menu)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     The connect to menu.
        /// </summary>
        /// <param name="menu">
        ///     The menu.
        /// </param>
        public void ConnectToMenu(IUnitMenu menu, Menu subMenu)
        {
            // this.BotPanel.ConnectToMenu(menu, subMenu);
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        public override void Draw()
        {
            Drawing.DrawRect(this.Position, this.Size, this.BackgroundColor);
            Drawing.DrawRect(this.barPos, this.fillSize, this.Color);
        }

        /// <summary>
        ///     The generate menu.
        /// </summary>
        /// <param name="menu">
        ///     The menu.
        /// </param>
        /// <returns>
        ///     The <see cref="Menu" />.
        /// </returns>
        public Menu GenerateMenu(IUnitMenu menu)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     The generate menu.
        /// </summary>
        /// <returns>
        ///     The <see cref="Menu" />.
        /// </returns>
        public Menu GenerateMenu()
        {
            throw new NotImplementedException();
        }

        /// <summary>Notifies the observer that the provider has finished sending push-based notifications.</summary>
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        /// <summary>Notifies the observer that the provider has experienced an error condition.</summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        /// <summary>Provides the observer with new data.</summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(IMana value)
        {
            this.FillPercentage = value.Percentage;
        }

        /// <summary>
        ///     The set position.
        /// </summary>
        /// <param name="healthbarPosition">
        ///     The healthbar position.
        /// </param>
        /// <param name="healthbarSize">
        ///     The healthbar Size.
        /// </param>
        public void SetPosition(Vector2 healthbarPosition, Vector2 healthbarSize)
        {
            this.Position = healthbarPosition + new Vector2(0, healthbarSize.Y);
        }

        #endregion
    }
}