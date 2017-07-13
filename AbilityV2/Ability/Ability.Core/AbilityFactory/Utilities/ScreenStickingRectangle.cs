// <copyright file="ScreenStickingRectangle.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.Utilities
{
    using System;

    using Ensage.Common;

    using SharpDX;

    /// <summary>The screen sticking rectangle.</summary>
    public abstract class ScreenStickingRectangle
    {
        #region Public Properties

        /// <summary>Gets or sets a value indicating whether calculate edge position.</summary>
        public virtual bool CalculateEdgePosition { get; set; }

        /// <summary>Gets or sets the distance from screen edge.</summary>
        public float DistanceFromScreenEdge { get; set; }

        /// <summary>Gets or sets the edge position.</summary>
        public Vector2 EdgePosition { get; set; }

        /// <summary>Gets or sets a value indicating whether is off screen.</summary>
        public bool IsOffScreen { get; set; }

        /// <summary>Gets or sets a value indicating whether is visible.</summary>
        public bool IsVisible { get; set; }

        /// <summary>Gets or sets the position.</summary>
        public virtual Vector2 Position { get; set; }

        /// <summary>Gets or sets the size.</summary>
        public virtual Vector2 Size { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The update.</summary>
        public void Check()
        {
            if (this.CheckMinimap())
            {
                return;
            }

            float difference;
            float ydifference;
            if (this.Position.X < 0)
            {
                difference = -this.Position.X;

                // Console.WriteLine("1 " + Game.RawGameTime);
                this.IsOffScreen = true;

                if ((ydifference = this.Position.Y + this.Size.Y - HUDInfo.ScreenSizeY()) >= 0)
                {
                    if (!this.CalculateEdgePosition)
                    {
                        this.IsVisible = this.Position.X > -this.Size.X && ydifference < this.Size.Y;
                        return;
                    }

                    this.EdgePosition = new Vector2(0, HUDInfo.ScreenSizeY() - this.Size.Y);

                    // var x = Math.Min(this.Position.X + ydifference, HUDInfo.ScreenSizeX() - this.Size.X);
                    // var y = Math.Max(this.Position.Y - difference, 0);
                    // this.EdgePosition = new Vector2(Math.Max(0, x), Math.Min(y, HUDInfo.ScreenSizeY() - this.Size.Y));
                }
                else
                {
                    if (!this.CalculateEdgePosition)
                    {
                        this.IsVisible = this.Position.X > -this.Size.X && this.Position.Y > -this.Size.Y;
                        return;
                    }

                    this.EdgePosition = this.Position.Y < 0 ? Vector2.Zero : new Vector2(0, this.Position.Y);

                    // var x = Math.Min(this.Position.X + ydifference, HUDInfo.ScreenSizeX() - this.Size.X);
                    // var y = Math.Min(this.Position.Y + difference, HUDInfo.ScreenSizeY() - this.Size.Y);
                    // this.EdgePosition = new Vector2(Math.Max(0, x), Math.Max(y, 0));
                }

                // this.DistanceFromScreenEdge = this.Position.Distance(this.EdgePosition);
            }
            else if (this.Position.Y < 0)
            {
                ydifference = -this.Position.Y;

                // Console.WriteLine("2 " + Game.RawGameTime);
                this.IsOffScreen = true;

                if ((difference = this.Position.X + this.Size.X - HUDInfo.ScreenSizeX()) >= 0)
                {
                    if (!this.CalculateEdgePosition)
                    {
                        this.IsVisible = difference < this.Size.X && this.Position.Y > -this.Size.Y;
                        return;
                    }

                    this.EdgePosition = new Vector2(HUDInfo.ScreenSizeX() - this.Size.X, 0);

                    // var x = Math.Max(this.Position.X - ydifference, 0);
                    // var y = Math.Min(this.Position.Y + difference, HUDInfo.ScreenSizeY() - this.Size.Y);
                    // this.EdgePosition = new Vector2(Math.Min(HUDInfo.ScreenSizeX() - this.Size.X, x), Math.Max(y, 0));
                }
                else
                {
                    if (!this.CalculateEdgePosition)
                    {
                        this.IsVisible = this.Position.X > -this.Size.X && this.Position.Y > -this.Size.Y;
                        return;
                    }

                    this.EdgePosition = this.Position.X < 0 ? Vector2.Zero : new Vector2(this.Position.X, 0);

                    // var x = Math.Min(this.Position.X + ydifference, HUDInfo.ScreenSizeX() - this.Size.X);
                    // var y = Math.Min(this.Position.Y + difference, HUDInfo.ScreenSizeY() - this.Size.Y);
                    // this.EdgePosition = new Vector2(Math.Max(0, x), Math.Max(y, 0));
                }

                // this.DistanceFromScreenEdge = this.Position.Distance(this.EdgePosition);
            }
            else if ((difference = this.Position.X + this.Size.X - HUDInfo.ScreenSizeX()) >= 0)
            {
                // Console.WriteLine("3 " + Game.RawGameTime);
                this.IsOffScreen = true;

                if ((ydifference = this.Position.Y + this.Size.Y - HUDInfo.ScreenSizeY()) >= 0)
                {
                    if (!this.CalculateEdgePosition)
                    {
                        this.IsVisible = difference < this.Size.X && ydifference < this.Size.Y;
                        return;
                    }

                    this.EdgePosition = new Vector2(
                        HUDInfo.ScreenSizeX() - this.Size.X,
                        HUDInfo.ScreenSizeY() - this.Size.Y);

                    // var x = Math.Max(this.Position.X - ydifference, 0);
                    // var y = Math.Max(this.Position.Y - difference, 0);
                    // this.EdgePosition = new Vector2(
                    // Math.Min(HUDInfo.ScreenSizeX() - this.Size.X, x),
                    // Math.Min(y, HUDInfo.ScreenSizeY() - this.Size.Y));
                }
                else
                {
                    if (!this.CalculateEdgePosition)
                    {
                        this.IsVisible = difference < this.Size.X;
                        return;
                    }

                    this.EdgePosition = new Vector2(HUDInfo.ScreenSizeX() - this.Size.X, this.Position.Y);

                    // var x = Math.Max(this.Position.X - ydifference, 0);
                    // var y = Math.Min(this.Position.Y + difference, HUDInfo.ScreenSizeY() - this.Size.Y);
                    // this.EdgePosition = new Vector2(Math.Min(HUDInfo.ScreenSizeX() - this.Size.X, x), Math.Max(y, 0));
                }

                // this.DistanceFromScreenEdge =
                // new Vector2(this.Position.X + this.Size.X, this.Position.Y).Distance(this.EdgePosition);
            }
            else if ((ydifference = this.Position.Y + this.Size.Y - HUDInfo.ScreenSizeY()) >= 0)
            {
                // Console.WriteLine("4 " + Game.RawGameTime);
                this.IsOffScreen = true;

                if ((difference = this.Position.X + this.Size.X - HUDInfo.ScreenSizeX()) >= 0)
                {
                    if (!this.CalculateEdgePosition)
                    {
                        this.IsVisible = difference < this.Size.X && ydifference < this.Size.Y;
                        return;
                    }

                    this.EdgePosition = new Vector2(
                        HUDInfo.ScreenSizeX() - this.Size.X,
                        HUDInfo.ScreenSizeY() - this.Size.Y);

                    // var x = Math.Max(this.Position.X - ydifference, 0);
                    // var y = Math.Max(this.Position.Y - difference, 0);
                    // this.EdgePosition = new Vector2(
                    // Math.Min(HUDInfo.ScreenSizeX() - this.Size.X, x),
                    // Math.Min(y, HUDInfo.ScreenSizeY() - this.Size.Y));
                }
                else
                {
                    if (!this.CalculateEdgePosition)
                    {
                        this.IsVisible = ydifference < this.Size.Y;
                        return;
                    }

                    // var x = Math.Min(this.Position.X + ydifference, HUDInfo.ScreenSizeX() - this.Size.X);
                    // var y = Math.Max(this.Position.Y - difference, 0);
                    // this.EdgePosition = new Vector2(Math.Max(0, x), Math.Min(y, HUDInfo.ScreenSizeY() - this.Size.Y));
                    this.EdgePosition = new Vector2(this.Position.X, HUDInfo.ScreenSizeY() - this.Size.Y);
                }

                // this.DistanceFromScreenEdge =
                // new Vector2(this.Position.X + this.Size.X, this.Position.Y).Distance(this.EdgePosition);
            }
            else
            {
                this.IsOffScreen = false;
            }
        }

        #endregion

        #region Methods

        private bool CheckMinimap()
        {
            if (HUDInfo.MinimapIsOnRight)
            {
                var minimapPos = new Vector2(
                    HUDInfo.ScreenSizeX() - HUDInfo.Minimap.Position.X - HUDInfo.Minimap.Size.X,
                    HUDInfo.ScreenSizeY() - HUDInfo.Minimap.Position.Y - HUDInfo.Minimap.Size.Y);

                var difference = this.Position.X + this.Size.X - minimapPos.X;
                if (difference >= 0)
                {
                    var ydifference = this.Position.Y + this.Size.Y - minimapPos.Y;
                    if (ydifference >= 0)
                    {
                        // Console.WriteLine("1 " + ydifference + " " + difference);
                        this.IsOffScreen = true;
                        if (!this.CalculateEdgePosition)
                        {
                            this.IsVisible = (difference > this.Size.X && ydifference < this.Size.Y
                                              || difference < this.Size.X
                                              && (ydifference < this.Size.Y || ydifference > this.Size.Y))
                                             && this.Position.X < HUDInfo.ScreenSizeX()
                                             && this.Position.Y < HUDInfo.ScreenSizeY();
                            return true;
                        }

                        var y = Math.Max(this.Position.Y - difference, minimapPos.Y - this.Size.Y);
                        var x = Math.Max(this.Position.X - ydifference, minimapPos.X - this.Size.X);
                        this.EdgePosition = new Vector2(
                            Math.Min(x, HUDInfo.ScreenSizeX() - this.Size.X),
                            Math.Min(y, HUDInfo.ScreenSizeY() - this.Size.Y));

                        // this.DistanceFromScreenEdge = this.Position.Distance(this.EdgePosition);
                        return true;
                    }

                    return false;
                }
            }
            else
            {
                var minimapPos = new Vector2(
                    HUDInfo.Minimap.Position.X + HUDInfo.Minimap.Size.X,
                    HUDInfo.ScreenSizeY() - HUDInfo.Minimap.Position.Y - HUDInfo.Minimap.Size.Y);
                var difference = minimapPos.X - this.Position.X;
                if (difference >= 0)
                {
                    var ydifference = this.Position.Y + this.Size.Y - minimapPos.Y;
                    if (ydifference >= 0)
                    {
                        // Console.WriteLine("1 " + ydifference + " " + difference);
                        this.IsOffScreen = true;
                        if (!this.CalculateEdgePosition)
                        {
                            this.IsVisible = (difference > this.Size.X && ydifference < this.Size.Y
                                              || difference < this.Size.X
                                              && (ydifference < this.Size.Y || ydifference > this.Size.Y))
                                             && this.Position.X + this.Size.X > 0
                                             && this.Position.Y < HUDInfo.ScreenSizeY();
                            return true;
                        }

                        var y = Math.Max(this.Position.Y - difference, minimapPos.Y - this.Size.Y);
                        var x = Math.Min(this.Position.X + ydifference, minimapPos.X);
                        this.EdgePosition = new Vector2(
                            Math.Max(x, 0),
                            Math.Min(y, HUDInfo.ScreenSizeY() - this.Size.Y));

                        // this.DistanceFromScreenEdge = this.Position.Distance(this.EdgePosition);
                        return true;
                    }

                    return false;
                }
            }

            // return false;
            return false;
        }

        #endregion
    }
}