// <copyright file="Body.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityManager.UI.Elements.Body
{
    using System;

    using Ensage;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;
    using SharpDX.Mathematics.Interop;

    /// <summary>
    ///     The body.
    /// </summary>
    public abstract class Body : IUserInterfaceElement
    {
        #region Fields

        /// <summary>
        ///     The body.
        /// </summary>
        private readonly DrawRect body;

        private Vector2 position;

        private Vector2 size;

        #endregion

        #region Constructors and Destructors

        protected Body(Vector2 size, Vector2 position)
        {
            this.Color = new Color(0, 0, 0, 160);
            Drawing.DrawRect(Vector2.One, Vector2.One, new Color());
            this.body = new DrawRect(this.Color)
                            {
                               Border = true, Position = position, Size = this.Size, Color = this.Color 
                            };
            this.Size = size;
            this.Position = position;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        ///     Gets or sets the parent.
        /// </summary>
        public IUserInterfaceElement Parent { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
                this.body.Position = this.position;
                this.UpdatePosition();
            }
        }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return this.size;
            }

            set
            {
                this.size = value;
                this.body.Size = this.size;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether visible.
        /// </summary>
        public bool Visible { get; set; } = true;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        public void Draw()
        {
            if (!this.Visible)
            {
                return;
            }

            this.body.Draw();
            this.DrawElements();
        }

        public abstract void DrawElements();

        /// <summary>
        ///     The mouse down.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public virtual bool MouseDown(Vector2 mousePosition)
        {
            return false;
        }

        /// <summary>
        ///     The mouse move.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public virtual void MouseMove(Vector2 mousePosition)
        {
        }

        /// <summary>
        ///     The mouse up.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public virtual void MouseUp(Vector2 mousePosition)
        {
        }

        /// <summary>
        ///     The update position.
        /// </summary>
        public virtual void UpdatePosition()
        {
        }

        /// <summary>
        ///     The update size.
        /// </summary>
        public void UpdateSize()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}