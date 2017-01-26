// <copyright file="UnitOverlayBase.cs" company="EnsageSharp">
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

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Bars;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.ObjectPanel;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.StatusPanel;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ScreenInfo;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.GetValue;

    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    using SharpDX;

    /// <summary>
    ///     The default unit overlay.
    /// </summary>
    public abstract class UnitOverlayBase : IUnitOverlay
    {
        #region Fields

        private bool offScreen;

        #endregion

        #region Constructors and Destructors

        // private DrawRect circleIcon;
        protected UnitOverlayBase(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public PanelField BotPanel { get; set; }

        /// <summary>
        ///     Gets or sets the elements.
        /// </summary>
        public ICollection<IUnitOverlayElement> Elements { get; } = new List<IUnitOverlayElement>();

        /// <summary>
        ///     Gets or sets the health bar.
        /// </summary>
        public HealthBar HealthBar { get; set; }

        /// <summary>
        ///     Gets or sets the health bar position.
        /// </summary>
        public Vector2 HealthBarPosition { get; set; }

        public PanelField LeftPanel { get; set; }

        /// <summary>
        ///     Gets or sets the mana bar.
        /// </summary>
        public ManaBar ManaBar { get; set; }

        /// <summary>
        ///     Gets the panels.
        /// </summary>
        public ICollection<IUnitOverlayElement> Panels { get; } = new List<IUnitOverlayElement>();

        public PanelField RightPanel { get; set; }

        public PanelField TopPanel { get; set; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        public virtual void Initialize()
        {

            this.HealthBar = new HealthBar(this.Unit, this.Unit.ScreenInfo.HealthBarSize)
            {
                Color = this.Unit.IsEnemy ? new Color(230, 70, 70) : new Color(90, 200, 70),
                BackgroundColor = Color.Black
            };
            this.LeftPanel = new PanelField(
                                 this.Unit,
                                 this.HealthBar,
                                 () => this.HealthBar.Position,
                                 PanelDirection.Left,
                                 element => -new Vector2(element.Size.X, 0))
            {
                Size = this.HealthBar.Size
            };
            this.Panels.Add(this.LeftPanel);
            this.TopPanel = new PanelField(
                                this.Unit,
                                this.HealthBar,
                                () => this.HealthBar.Position,
                                PanelDirection.Top,
                                element => -new Vector2(0, element.Size.Y),
                                vertical: true)
            {
                Size = this.HealthBar.Size
            };
            this.Panels.Add(this.TopPanel);
            this.RightPanel = new PanelField(
                                  this.Unit,
                                  this.HealthBar,
                                  () => this.HealthBar.Position + new Vector2(this.HealthBar.Size.X, 0),
                                  PanelDirection.Right,
                                  positionFromLastElement: element => new Vector2(element.Size.X, 0))
            {
                Size = this.HealthBar.Size
            };
            this.Panels.Add(this.RightPanel);

            // this.Elements.Add(this.HealthBar);
            this.ManaBar = new ManaBar(this.Unit, new Vector2(this.HealthBar.Size.X, (float)(this.HealthBar.Size.Y / 2)))
            {
                Color = new Color(70, 90, 200),
                BackgroundColor = Color.Black
            };

            this.BotPanel = new PanelField(
                                this.Unit,
                                this.ManaBar,
                                () => this.ManaBar.Position + new Vector2(0, this.ManaBar.Size.Y),
                                PanelDirection.Bottom,
                                positionFromLastElement: element => new Vector2(0, element.Size.Y),
                                vertical: true)
            {
                Size = this.ManaBar.Size
            };
            this.Panels.Add(this.BotPanel);

            ///this.Elements.Add(this.ManaBar);
            this.TopPanel.AddElement(new StatusPanel(this.Unit, new Vector2((float)(this.HealthBar.Size.Y * 1.2))) { });

            // this.Unit.Position.Subscribe(this);
            this.Unit.ScreenInfo.UpdateProvider.Subscribe(new DataObserver<IScreenInfo>(info => { this.Update(); }));

            // this.circleIcon = new DrawRect(Textures.GetHeroRoundTexture(this.Unit.Name))
            // { Size = new Vector2((float)(this.HealthBar.Size.Y * 2.5)) };
        }

        public virtual void Dispose()
        {

        }

        #endregion

        #region Public Methods and Operators

        private float distanceFromSide;

        /// <summary>
        ///     The on draw.
        /// </summary>
        public void OnDraw()
        {
            if (!this.Unit.SourceUnit.IsAlive || !this.Unit.SourceUnit.IsVisible)
            {
                return;
            }

            if (this.HealthBarPosition.Equals(Vector2.Zero))
            {
                return;
            }

            if (this.offScreen)
            {
                //if (!this.StickToScreen.Value
                //    || this.DistanceFromLocalHero.Value
                //    < this.Unit.Position.Current.Distance2D(GlobalVariables.LocalHero.Position))
                //{
                //    return;
                //}

                this.HealthBar.Draw();
            }

            if (this.Unit.IsEnemy || this.offScreen)
            {
                this.ManaBar.Draw();
            }

            this.LeftPanel.Draw();
            this.TopPanel.Draw();
            this.RightPanel.Draw();
            this.BotPanel.Draw();
        }

        public void Update()
        {
            this.HealthBarPosition = this.Unit.ScreenInfo.HealthBarPosition;
            if (this.HealthBarPosition.Equals(Vector2.Zero))
            {
                return;
            }

            if (this.HealthBarPosition.X - this.LeftPanel.Size.X < 0
                || this.HealthBarPosition.X + this.HealthBar.Size.X + this.RightPanel.Size.X > HUDInfo.ScreenSizeX()
                || this.HealthBarPosition.Y - this.TopPanel.Size.Y < 0
                || this.HealthBarPosition.Y + this.HealthBar.Size.Y + this.ManaBar.Size.Y
                + Math.Max(
                    this.BotPanel.Size.Y,
                    Math.Max(this.LeftPanel.Size.Y, this.RightPanel.Size.Y) - this.HealthBar.Size.Y
                    - this.ManaBar.Size.Y) > HUDInfo.ScreenSizeY())
            {
                var x = Math.Min(
                    Math.Max(this.HealthBarPosition.X, this.LeftPanel.Size.X),
                    HUDInfo.ScreenSizeX() - this.HealthBar.Size.X - this.RightPanel.Size.X);
                var y = Math.Min(
                    Math.Max(this.HealthBarPosition.Y, this.TopPanel.Size.Y),
                    HUDInfo.ScreenSizeY() - this.HealthBar.Size.Y - this.ManaBar.Size.Y
                    - Math.Max(
                        this.BotPanel.Size.Y,
                        Math.Max(this.LeftPanel.Size.Y, this.RightPanel.Size.Y) - this.HealthBar.Size.Y
                        - this.ManaBar.Size.Y));

                this.HealthBar.Position = new Vector2((float)x, (float)y);
                this.ManaBar.Position = this.HealthBar.Position + this.ManaBar.PositionFromHealthBar;
                this.LeftPanel.Position = this.LeftPanel.BasePosition.Invoke();
                this.RightPanel.Position = this.RightPanel.BasePosition.Invoke();
                this.TopPanel.Position = this.TopPanel.BasePosition.Invoke();
                this.BotPanel.Position = this.BotPanel.BasePosition.Invoke();
                this.offScreen = true;
                return;
            }

            this.offScreen = false;
            this.HealthBar.Position = this.HealthBarPosition;
            this.ManaBar.Position = this.HealthBarPosition + this.ManaBar.PositionFromHealthBar;
            this.LeftPanel.Position = this.LeftPanel.BasePosition.Invoke();
            this.RightPanel.Position = this.RightPanel.BasePosition.Invoke();
            this.TopPanel.Position = this.TopPanel.BasePosition.Invoke();
            this.BotPanel.Position = this.BotPanel.BasePosition.Invoke();
        }

        public GetValue<bool, bool> StickToScreen { get; set; }

        public GetValue<Slider, float> DistanceFromLocalHero { get; set; }

        #endregion
    }
}