// <copyright file="HealthSeparator.cs" company="EnsageSharp">
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
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>The health separator.</summary>
    public class HealthSeparator : DrawObject
    {
        #region Fields

        private HealthBar healthBar;

        private float separatedValue;

        /// <summary>The x coordinate.</summary>
        private float xCoordinate;

        #endregion

        #region Constructors and Destructors

        public HealthSeparator(float separatedValue, HealthBar healthBar)
        {
            this.healthBar = healthBar;
            Color color;
            if (separatedValue == 1000 || separatedValue == 2000 || separatedValue == 3000 || separatedValue == 4000)
            {
                color = Color.Black;
            }
            else
            {
                color = new Color(40, 40, 40, 120);
            }

            this.separatedValue = separatedValue;
            this.xCoordinate = healthBar.Size.X * (separatedValue / healthBar.Unit.Health.Maximum);
            this.Line = new DrawVerticalLine(healthBar.Size.Y, color);
            this.HealthBarPositionChange();
        }

        #endregion

        #region Public Properties

        /// <summary>Gets the line.</summary>
        public DrawVerticalLine Line { get; }

        public bool Visible { get; set; } = true;

        #endregion

        #region Public Methods and Operators

        public override void Draw()
        {
            this.Line.Draw();
        }

        public void HealthBarPositionChange()
        {
            this.Line.Position = this.healthBar.Position + new Vector2(this.xCoordinate, 0);
        }

        public void HealthChange()
        {
            this.Visible = this.healthBar.Unit.Health.Current > this.separatedValue;
        }

        public void MaxHealthChange()
        {
            // this.Visible = this.healthBar.Unit.Health.Current > this.separatedValue;
            this.xCoordinate = this.healthBar.Size.X * (this.separatedValue / this.healthBar.Unit.Health.Maximum);
            this.HealthBarPositionChange();
        }

        #endregion
    }
}