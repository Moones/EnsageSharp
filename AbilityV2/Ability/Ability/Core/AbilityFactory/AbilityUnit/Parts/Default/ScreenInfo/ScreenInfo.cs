// <copyright file="ScreenInfo.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ScreenInfo
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Position;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common;

    using SharpDX;

    /// <summary>
    ///     The screen position.
    /// </summary>
    public class ScreenInfo : IScreenInfo
    {
        #region Fields

        private Vector2 healthBarPosition;

        private bool isVisible;

        private Vector2 minimapPosition;

        private Vector2 screenPosition;

        #endregion

        #region Constructors and Destructors

        public ScreenInfo(IAbilityUnit unit)
        {
            this.Unit = unit;

            var hero = unit.SourceUnit as Hero;
            if (hero != null)
            {
                this.TopPanelPosition = HUDInfo.GetTopPanelPosition(hero);
                this.TopPanelIconSize = new Vector2(
                    (float)HUDInfo.GetTopPanelSizeX(hero),
                    (float)HUDInfo.GetTopPanelSizeY(hero));
            }

            this.HealthBarSize = new Vector2(
                HUDInfo.GetHPBarSizeX(hero ?? unit.SourceUnit),
                HUDInfo.GetHpBarSizeY(hero ?? unit.SourceUnit));
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the health bar position.
        /// </summary>
        public Vector2 HealthBarPosition => this.healthBarPosition;

        /// <summary>
        ///     Gets the health bar size.
        /// </summary>
        public Vector2 HealthBarSize { get; }

        /// <summary>
        ///     Gets a value indicating whether is visible.
        /// </summary>
        public bool IsVisible => this.isVisible;

        /// <summary>
        ///     Gets the minimap position.
        /// </summary>
        public Vector2 MinimapPosition => this.minimapPosition;

        public Vector2 Position
        {
            get
            {
                return this.screenPosition;
            }

            set
            {
                this.screenPosition = value;
                if (this.screenPosition == Vector2.Zero)
                {
                    this.healthBarPosition = Vector2.Zero;
                    this.isVisible = false;
                }
                else
                {
                    this.healthBarPosition = this.UpdateHealthBarPosition();
                }

                this.UpdateProvider.Next(this);
            }
        }

        /// <summary>
        ///     Gets the top panel icon size.
        /// </summary>
        public Vector2 TopPanelIconSize { get; }

        /// <summary>
        ///     Gets the top panel position.
        /// </summary>
        public Vector2 TopPanelPosition { get; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        /// <summary>
        ///     Gets the update provider.
        /// </summary>
        public DataProvider<IScreenInfo> UpdateProvider { get; } = new DataProvider<IScreenInfo>();

        #endregion

        #region Public Methods and Operators

        public virtual void Dispose()
        {
        }

        public virtual void Initialize()
        {
            this.Unit.Position.Subscribe(
                new DataObserver<IPosition>(
                    position =>
                        {
                            // this.Position =
                            // Drawing.WorldToScreen(
                            // position.Current
                            // + new Vector3(0, 0, this.Unit.SourceUnit.HealthBarOffset));
                            this.minimapPosition = position.Current.WorldToMinimap();
                        }));
        }

        public void Update()
        {
            this.Position =
                Drawing.WorldToScreen(
                    this.Unit.SourceUnit.Position + new Vector3(0, 0, this.Unit.SourceUnit.HealthBarOffset));
        }

        /// <summary>
        ///     The update health bar position.
        /// </summary>
        public virtual Vector2 UpdateHealthBarPosition()
        {
            return this.screenPosition
                   + new Vector2((float)(-HUDInfo.HpBarX * HUDInfo.Monitor), -HUDInfo.HpBarY * HUDInfo.Monitor);
        }

        #endregion
    }
}