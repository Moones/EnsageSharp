// <copyright file="Window.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityManager.UI.Elements.Window
{
    using System;
    using System.Collections.ObjectModel;

    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.AbilityManager.UI.Elements.Body;
    using Ability.Core.AbilityManager.UI.Elements.Button;
    using Ability.Core.AbilityManager.UI.Elements.TopPanel;

    using Ensage.Common;

    using SharpDX;

    /// <summary>
    ///     The window.
    /// </summary>
    public class Window : IUserInterfaceElement
    {
        #region Fields

        private readonly Body body;

        private readonly TopPanel topPanel;

        /// <summary>
        ///     The close button.
        /// </summary>
        private Button closeButton;

        private Collection<IUserInterfaceElement> elements = new Collection<IUserInterfaceElement>();

        private Button hideButton;

        private bool visible;

        #endregion

        #region Constructors and Destructors

        public Window(Vector2 size, Vector2 position, string name, Body body)
        {
            this.Size = size;
            this.Position = position;

            this.body = body;
            this.topPanel = new TopPanel(
                new Vector2(size.X, (float)(HUDInfo.GetHpBarSizeY() * 2.5)),
                position - new Vector2(0, HUDInfo.GetHpBarSizeY() * 3),
                name);
            this.body.Position = this.topPanel.Position + new Vector2(0, this.topPanel.Size.Y);
            this.closeButton = new Button(
                new Vector2((float)(this.topPanel.Size.Y * 1.2), this.topPanel.Size.Y),
                new Vector2((float)(this.topPanel.Size.X - this.topPanel.Size.Y * 1.2), 0),
                "x",
                Color.White,
                this.topPanel,
                () =>
                    {
                        this.topPanel.Visible = false;
                        this.body.Visible = false;
                        this.closeButton.Visible = false;
                        this.hideButton.Visible = false;
                    });
            this.elements.Add(this.closeButton);
            this.hideButton = new Button(
                this.closeButton.Size,
                new Vector2(this.topPanel.Size.X - this.closeButton.Size.X * 2 - 1, 0),
                "-",
                Color.White,
                this.topPanel,
                () =>
                    {
                        this.body.Visible = !this.body.Visible;
                        this.hideButton.Text = !this.body.Visible ? "+" : "-";
                    });
            this.elements.Add(this.hideButton);

            this.topPanel.PositionUpdate.Subscribe(
                new DataObserver<Vector2>(
                    vector2 =>
                        {
                            this.body.Position = vector2 + new Vector2(0, this.topPanel.Size.Y);
                            foreach (var userInterfaceElement in this.elements)
                            {
                                userInterfaceElement.UpdatePosition();
                            }
                        }));
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
        public Vector2 Position { get; set; }

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
                this.closeButton.Visible = this.visible;
                this.hideButton.Visible = this.visible;
                this.body.Visible = this.visible;
                this.topPanel.Visible = this.visible;
            }
        }

        /// <summary>
        ///     The close button.
        /// </summary>
        public Button CloseButton
        {
            get
            {
                return this.closeButton;
            }
            set
            {
                this.closeButton = value;
            }
        }

        public Button HideButton
        {
            get
            {
                return this.hideButton;
            }
            set
            {
                this.hideButton = value;
            }
        }

        public Body Body1
        {
            get
            {
                return this.body;
            }
        }

        public TopPanel Panel
        {
            get
            {
                return this.topPanel;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        public void Draw()
        {
            this.topPanel.Draw();
            this.body.Draw();
            this.closeButton.Draw();
            this.hideButton.Draw();
        }

        /// <summary>
        ///     The mouse down.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool MouseDown(Vector2 mousePosition)
        {
            return this.closeButton.MouseDown(mousePosition) || this.hideButton.MouseDown(mousePosition)
                   || this.body.MouseDown(mousePosition) || this.topPanel.MouseDown(mousePosition);
        }

        /// <summary>
        ///     The mouse move.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public void MouseMove(Vector2 mousePosition)
        {
            this.body.MouseMove(mousePosition);
            this.topPanel.MouseMove(mousePosition);
            this.closeButton.MouseMove(mousePosition);
            this.hideButton.MouseMove(mousePosition);
        }

        /// <summary>
        ///     The mouse up.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public void MouseUp(Vector2 mousePosition)
        {
            this.body.MouseUp(mousePosition);
            this.topPanel.MouseUp(mousePosition);
            this.closeButton.MouseUp(mousePosition);
            this.hideButton.MouseUp(mousePosition);
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