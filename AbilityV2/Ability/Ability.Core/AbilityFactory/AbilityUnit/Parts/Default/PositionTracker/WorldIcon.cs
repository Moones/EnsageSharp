// <copyright file="WorldIcon.cs" company="EnsageSharp">
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
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.ScreenInfo;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    using SharpDX;

    /// <summary>The world icon.</summary>
    internal class WorldIcon : ScreenStickingRectangle, IAbilityUnitPart
    {
        #region Fields

        private DataObserver<IScreenInfo> screenInfoObserver;

        #endregion

        #region Constructors and Destructors

        internal WorldIcon(IAbilityUnit unit)
        {
            this.Unit = unit;
            this.screenInfoObserver = new DataObserver<IScreenInfo>(info => this.UpdatePosition());
            this.drawWorldIcon = new ActionExecutor(this.Draw);
        }

        #endregion

        #region Public Properties

        public override bool CalculateEdgePosition { get; set; } = true;

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>The draw.</summary>
        public void Draw()
        {
            if (this.Position.Equals(Vector2.Zero))
            {
                return;
            }

            var pos = this.IsOffScreen ? this.EdgePosition : this.Position;

            // var camerapos = new Vector2(HUDInfo.ScreenSizeX() / 2, HUDInfo.ScreenSizeY() / 2);

            // var direction = (this.Position - this.EdgePosition).Normalized();

            // var a = camerapos.Extend(pos)
            // Drawing.DrawLine();
            Drawing.DrawRect(pos, this.Unit.IconDrawer.WorldIconSize, this.Unit.IconDrawer.Icon);
        }

        private ActionExecutor drawWorldIcon;

        public void EndDrawing()
        {
            this.drawWorldIcon.Dispose();
            this.screenInfoObserver.Dispose();
        }

        public void Initialize()
        {
        }

        public void StartDrawing()
        {
            this.Size = this.Unit.IconDrawer.WorldIconSize;
            this.UpdatePosition();
            this.screenInfoObserver.Subscribe(this.Unit.ScreenInfo.UpdateProvider);
            this.drawWorldIcon.Subscribe(this.Unit.DataReceiver.Drawings);
        }

        public void UpdatePosition()
        {
            this.Position = this.Unit.ScreenInfo.Position;
            this.Check();
        }

        #endregion
    }
}