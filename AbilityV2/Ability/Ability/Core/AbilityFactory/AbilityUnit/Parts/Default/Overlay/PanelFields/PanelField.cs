// <copyright file="PanelField.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.PanelFields
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.ObjectPanel;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;

    using Ensage.Common.Menu;

    using SharpDX;

    /// <summary>
    ///     The panel field.
    /// </summary>
    public abstract class PanelField : IUnitOverlayElement
    {
        #region Fields

        /// <summary>
        ///     The position.
        /// </summary>
        private Vector2 position;

        private List<IUnitOverlayElement> storedElements;

        #endregion

        #region Constructors and Destructors

        protected PanelField(
            IAbilityUnit unit,
            IUnitOverlayElement parent,
            Func<Vector2> basePosition,
            PanelDirection panelDirection,
            Func<IUnitOverlayElement, Vector2> positionFromHealthbar = null,
            Func<IUnitOverlayElement, Vector2> positionFromLastElement = null)
        {
            this.Unit = unit;
            this.BasePosition = basePosition;
            this.PanelDirection = panelDirection;
            this.ParentElement = parent;
            this.StoredElements = new List<IUnitOverlayElement>();
            this.Position = this.BasePosition.Invoke();
            this.PositionFromElementHealthbarFunc = positionFromHealthbar;
            this.PositionFromLastElementFunc = positionFromLastElement;
            this.Vertical = panelDirection == PanelDirection.Top || panelDirection == PanelDirection.Bottom;
            this.Size = parent.Size;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the base position.
        /// </summary>
        public Func<Vector2> BasePosition { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     Gets a value indicating whether generate menu.
        /// </summary>
        public bool GenerateMenuBool { get; }

        /// <summary>
        ///     Gets or sets the parent element.
        /// </summary>
        public PanelField Panel { get; set; }

        /// <summary>
        ///     Gets the panel direction.
        /// </summary>
        public PanelDirection PanelDirection { get; }

        /// <summary>
        ///     Gets or sets the parent element.
        /// </summary>
        public IUnitOverlayElement ParentElement { get; set; }

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
                var baseposition = this.position;
                foreach (var unitOverlayElement in this.StoredElements)
                {
                    baseposition += this.UpdateElementPosition(unitOverlayElement, baseposition);
                }
            }
        }

        /// <summary>
        ///     Gets the position from element healthbar function.
        /// </summary>
        public Func<IUnitOverlayElement, Vector2> PositionFromElementHealthbarFunc { get; }

        /// <summary>
        ///     Gets the position from health bar.
        /// </summary>
        public Vector2 PositionFromHealthBar { get; set; }

        /// <summary>
        ///     Gets the position from last element func.
        /// </summary>
        public Func<IUnitOverlayElement, Vector2> PositionFromLastElementFunc { get; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>Gets the size changed.</summary>
        public Notifier SizeChanged { get; } = new Notifier();

        /// <summary>
        ///     Gets or sets the size increase.
        /// </summary>
        public float SizeIncrease { get; set; }

        /// <summary>
        ///     Gets the stored elements.
        /// </summary>
        public List<IUnitOverlayElement> StoredElements
        {
            get
            {
                return this.storedElements;
            }

            set
            {
                this.storedElements = value;
                this.UpdateSize();
            }
        }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        /// <summary>
        ///     Gets a value indicating whether vertical.
        /// </summary>
        public bool Vertical { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add element.
        /// </summary>
        /// <param name="element">
        ///     The element.
        /// </param>
        public void AddElement(IUnitOverlayElement element)
        {
            var tempList = this.StoredElements.ToList();
            element.Panel = this;
            tempList.Add(element);
            this.StoredElements = tempList.OrderByDescending(this.ElementPriority).ToList();

            // switch (this.PanelDirection)
            // {
            // case PanelDirection.Left:
            // this.StoredElements = tempList.OrderByDescending(x => x.Size.Y + x.Size.X / 2).ToList();
            // break;
            // case PanelDirection.Right:
            // this.StoredElements = tempList.OrderByDescending(x => x.Size.Y + x.Size.X / 2).ToList();
            // break;
            // case PanelDirection.Bottom:
            // this.StoredElements = tempList.OrderByDescending(x => x.Size.X + x.Size.Y / 2).ToList();
            // break;
            // default:
            // this.StoredElements = tempList.OrderByDescending(x => x.Size.X + x.Size.Y / 2).ToList();
            // break;
            // }
        }

        /// <summary>
        ///     The add submenu.
        /// </summary>
        /// <param name="menu">
        ///     The menu.
        /// </param>
        /// <returns>
        ///     The <see cref="Menu" />.
        /// </returns>
        public Menu AddSubmenu(IUnitMenu menu)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     The connect to menu.
        /// </summary>
        /// <param name="menu">
        ///     The menu.
        /// </param>
        /// <param name="subMenu">
        ///     The sub Menu.
        /// </param>
        public void ConnectToMenu(IUnitMenu menu, Menu subMenu)
        {
        }

        public void Dispose()
        {
            this.SizeChanged.Reacters.Clear();

            foreach (var unitOverlayElement in this.storedElements)
            {
                unitOverlayElement.Dispose();
            }

            this.storedElements.Clear();
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        public void Draw()
        {
            // switch (this.PanelDirection)
            // {
            // case PanelDirection.Top:
            // Drawing.DrawRect(
            // this.Position + new Vector2(-1, -this.Size.Y + 1),
            // this.Size + new Vector2(2),
            // Color.White,
            // true);
            // break;
            // case PanelDirection.Bottom:
            // Drawing.DrawRect(this.Position + new Vector2(-1, -1), this.Size + new Vector2(2), Color.White, true);
            // break;
            // case PanelDirection.Left:
            // Drawing.DrawRect(
            // this.Position + new Vector2(-1 - this.Size.X, -1),
            // this.Size + new Vector2(2),
            // Color.White,
            // true);
            // break;
            // case PanelDirection.Right:
            // Drawing.DrawRect(this.Position + new Vector2(-1, -1), this.Size + new Vector2(2), Color.White, true);
            // break;
            // }
            foreach (var unitOverlayElement in this.StoredElements)
            {
                if (!unitOverlayElement.Enabled)
                {
                    continue;
                }

                unitOverlayElement.Draw();
            }
        }

        /// <summary>The element priority.</summary>
        /// <param name="element">The element.</param>
        /// <returns>The <see cref="float" />.</returns>
        public abstract float ElementPriority(IUnitOverlayElement element);

        /// <summary>
        ///     The generate menu.
        /// </summary>
        /// <param name="menu">
        ///     The menu.
        /// </param>
        /// <returns>
        ///     The <see cref="Menu" />.
        /// </returns>
        public Menu GenerateMenu(IUnitMenu menu)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     The generate menu.
        /// </summary>
        /// <returns>
        ///     The <see cref="Menu" />.
        /// </returns>
        public Menu GenerateMenu()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     The remove element.
        /// </summary>
        /// <param name="element">
        ///     The element.
        /// </param>
        public void RemoveElement(IUnitOverlayElement element)
        {
            var tempList = this.StoredElements.ToList();
            element.Panel = null;
            tempList.Remove(element);
            this.StoredElements = tempList;
        }

        /// <summary>The update element position.</summary>
        /// <param name="unitOverlayElement">The unit overlay element.</param>
        /// <param name="basePosition">The base position.</param>
        /// <returns>The <see cref="Vector2" />.</returns>
        public virtual Vector2 UpdateElementPosition(IUnitOverlayElement unitOverlayElement, Vector2 basePosition)
        {
            if (!unitOverlayElement.Enabled)
            {
                return Vector2.Zero;
            }

            var addition = Vector2.Zero;
            if (this.PositionFromElementHealthbarFunc != null)
            {
                addition += this.PositionFromElementHealthbarFunc.Invoke(unitOverlayElement);
            }

            unitOverlayElement.Position = addition + basePosition;

            // if (this.Vertical)
            // {
            // unitOverlayElement.Position +=
            // new Vector2(this.Unit.ScreenInfo.HealthBarSize.X / 2 - unitOverlayElement.Size.X / 2, 0);
            // }
            if (this.PositionFromLastElementFunc != null)
            {
                addition += this.PositionFromLastElementFunc.Invoke(unitOverlayElement);
            }

            return addition;
        }

        /// <summary>The update panel field size.</summary>
        public abstract void UpdatePanelFieldSize();

        /// <summary>The update size.</summary>
        public void UpdateSize()
        {
            this.UpdatePanelFieldSize();

            // if (this is TopPanelField)
            // {
            // Console.WriteLine("toppanel " + this.SizeChanged.Reacters.Count + " " + this.Size);
            // }
            this.SizeChanged.Notify();

            // switch (this.PanelDirection)
            // {
            // case PanelDirection.Top:
            // this.Size = new Vector2(this.ParentElement.Size.X, 0);
            // foreach (var unitOverlayElement in this.storedElements)
            // {
            // if (!unitOverlayElement.Enabled)
            // {
            // continue;
            // }

            // this.Size = new Vector2(
            // Math.Max(this.Size.X, unitOverlayElement.Size.X),
            // this.Size.Y + unitOverlayElement.Size.Y);
            // }

            // break;
            // case PanelDirection.Bottom:
            // this.Size = new Vector2(this.ParentElement.Size.X, 0);
            // foreach (var unitOverlayElement in this.storedElements)
            // {
            // if (!unitOverlayElement.Enabled)
            // {
            // continue;
            // }

            // this.Size = new Vector2(
            // Math.Max(this.Size.X, unitOverlayElement.Size.X),
            // this.Size.Y + unitOverlayElement.Size.Y);
            // }

            // break;
            // case PanelDirection.Left:
            // this.Size = new Vector2(0, this.ParentElement.Size.Y);
            // foreach (var unitOverlayElement in this.storedElements)
            // {
            // if (!unitOverlayElement.Enabled)
            // {
            // continue;
            // }

            // this.Size = new Vector2(
            // this.Size.X + unitOverlayElement.Size.X,
            // Math.Max(this.Size.Y, unitOverlayElement.Size.Y));
            // }

            // break;
            // case PanelDirection.Right:
            // this.Size = new Vector2(0, this.ParentElement.Size.Y);
            // foreach (var unitOverlayElement in this.storedElements)
            // {
            // if (!unitOverlayElement.Enabled)
            // {
            // continue;
            // }

            // this.Size = new Vector2(
            // this.Size.X + unitOverlayElement.Size.X,
            // Math.Max(this.Size.Y, unitOverlayElement.Size.Y));
            // }

            // break;
            // }
        }

        #endregion
    }
}