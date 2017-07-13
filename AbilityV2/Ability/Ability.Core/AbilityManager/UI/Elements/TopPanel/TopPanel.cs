// <copyright file="TopPanel.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityManager.UI.Elements.TopPanel
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;

    using Ensage.Common;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;
    using SharpDX.Mathematics.Interop;

    /// <summary>
    ///     The top panel.
    /// </summary>
    public class TopPanel : IUserInterfaceElement
    {
        #region Fields

        private bool drag;

        private Vector2 mousedifference;

        /// <summary>
        ///     The name.
        /// </summary>
        private DrawText nameText;

        private Vector2 position;

        /// <summary>
        ///     The top panel.
        /// </summary>
        private DrawRect topPanel;

        private bool visible = true;

        #endregion

        #region Constructors and Destructors

        public TopPanel(Vector2 size, Vector2 position, string name)
        {
            this.Size = size;
            this.position = position;
            this.Color = new Color(50, 130, 0);

            this.nameText = new DrawText
                                {
                                    Color = Color.White, Text = name,
                                    TextSize = new Vector2((float)(this.Size.Y / 1.3), 0)
                                };
            this.topPanel = new DrawRect(this.Color) { Position = this.position, Size = this.Size };
            this.nameText.CenterOnRectangleHorizontally(this.topPanel, 5);
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
                this.PositionUpdate.Next(this.position);
                this.topPanel.Position = this.position;
                this.nameText.CenterOnRectangleHorizontally(this.topPanel, 5);
            }
        }

        public DataProvider<Vector2> PositionUpdate { get; } = new DataProvider<Vector2>();

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether visible.
        /// </summary>
        public bool Visible
        {
            get
            {
                return this.visible;
            }

            set
            {
                this.visible = value;
            }
        }

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

            this.topPanel.Draw();
            this.nameText.Draw();
        }

        public bool IsHovered(Vector2 mousePosition)
        {
            return Utils.IsUnderRectangle(mousePosition, this.Position.X, this.Position.Y, this.Size.X, this.Size.Y);
        }

        /// <summary>
        ///     The mouse down.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public bool MouseDown(Vector2 mousePosition)
        {
            if (this.IsHovered(mousePosition))
            {
                this.drag = true;
                this.mousedifference = mousePosition - this.Position;
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
            if (this.drag)
            {
                this.Position = mousePosition - this.mousedifference;
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
            this.drag = false;
        }

        /// <summary>
        ///     The update position.
        /// </summary>
        public void UpdatePosition()
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