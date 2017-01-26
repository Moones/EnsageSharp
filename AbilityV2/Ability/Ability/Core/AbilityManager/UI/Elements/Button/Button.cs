// <copyright file="Button.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityManager.UI.Elements.Button
{
    using System;

    using Ensage.Common;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>
    ///     The button.
    /// </summary>
    public class Button : IUserInterfaceElement
    {
        #region Fields

        /// <summary>
        ///     The button.
        /// </summary>
        private DrawRect button;

        /// <summary>
        ///     The position.
        /// </summary>
        private Vector2 position;

        /// <summary>
        ///     The pushed.
        /// </summary>
        private bool pushed;

        /// <summary>
        ///     The real position.
        /// </summary>
        private Vector2 realPosition;

        private DrawText text;

        private string text1;

        #endregion

        #region Constructors and Destructors

        public Button(
            Vector2 size,
            Vector2 position,
            string text,
            Color textColor,
            IUserInterfaceElement parent,
            Action action)
        {
            this.Parent = parent;
            this.Size = size;
            this.Position = position;
            this.Color = parent.Color;
            this.HoverColor = this.Color + new Color(50, 50, 50);
            this.PushColor = new Color(this.Color.R - 50, this.Color.G - 50, this.Color.B - 50);
            this.Action = action;
            this.Visible = this.Parent.Visible;

            this.button = new DrawRect(this.Color) { Position = this.realPosition, Size = this.Size };
            this.text = new DrawText { Color = textColor, Text = text, TextSize = new Vector2(size.Y) };
            this.text.CenterOnRectangle(this.button);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the action.
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        ///     Gets or sets the hover color.
        /// </summary>
        public Color HoverColor { get; set; }

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
                this.realPosition = this.Parent.Position + this.position;
            }
        }

        public Color PushColor { get; set; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text1;
            }

            set
            {
                this.text1 = value;
                this.text.Text = this.text1;
                this.text.CenterOnRectangle(this.button);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether visible.
        /// </summary>
        public bool Visible { get; set; }

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

            this.button.Draw();
            this.text.Draw();
        }

        public bool IsHovered(Vector2 mousePosition)
        {
            return Utils.IsUnderRectangle(
                mousePosition,
                this.realPosition.X,
                this.realPosition.Y,
                this.Size.X,
                this.Size.Y);
        }

        /// <summary>
        ///     The mouse down.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public bool MouseDown(Vector2 mousePosition)
        {
            if (!this.Visible)
            {
                return false;
            }

            if (this.IsHovered(mousePosition))
            {
                this.pushed = true;
                this.button.Color = this.PushColor;
                return true;
            }

            return false;
        }

        /// <summary>
        ///     The mouse move.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public void MouseMove(Vector2 mousePosition)
        {
            if (!this.Visible)
            {
                return;
            }

            if (!this.pushed && this.IsHovered(mousePosition))
            {
                this.button.Color = this.HoverColor;
            }
            else
            {
                this.button.Color = this.Color;
            }
        }

        /// <summary>
        ///     The mouse up.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public void MouseUp(Vector2 mousePosition)
        {
            if (!this.Visible)
            {
                return;
            }

            if (this.pushed && this.IsHovered(mousePosition))
            {
                this.Action.Invoke();
                this.button.Color = this.HoverColor;
            }

            this.pushed = false;
        }

        /// <summary>
        ///     The update position.
        /// </summary>
        public void UpdatePosition()
        {
            this.realPosition = this.Parent.Position + this.position;
            this.button.Position = this.realPosition;
            this.text.CenterOnRectangle(this.button);
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