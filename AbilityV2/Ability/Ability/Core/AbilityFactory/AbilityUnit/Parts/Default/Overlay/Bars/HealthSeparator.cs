using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Bars
{
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>The health separator.</summary>
    public class HealthSeparator : DrawObject
    {
        /// <summary>The x coordinate.</summary>
        private float xCoordinate;

        private HealthBar healthBar;

        private float separatedValue;

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

        /// <summary>Gets the line.</summary>
        public DrawVerticalLine Line { get; }

        public bool Visible { get; set; } = true;

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
            //this.Visible = this.healthBar.Unit.Health.Current > this.separatedValue;
            this.xCoordinate = this.healthBar.Size.X * (this.separatedValue / this.healthBar.Unit.Health.Maximum);
            this.HealthBarPositionChange();
        }

        public override void Draw()
        {
            this.Line.Draw();
        }
    }
}
