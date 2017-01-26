// <copyright file="PanelObjectManager.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.ObjectPanel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SharpDX;

    /// <summary>
    ///     The panel object manager.
    /// </summary>
    /// <typeparam name="TObject">
    ///     The type of objects
    /// </typeparam>
    public class PanelObjectManager<TObject>
        where TObject : class, IUnitOverlayElement
    {
        #region Fields

        private readonly Vector2 defaultObjectSize;

        /// <summary>
        ///     The direction.
        /// </summary>
        private PanelDirection direction;

        private List<TObject> objects = new List<TObject>();

        private Vector2 size;

        private float sizeIncrease;

        #endregion

        #region Constructors and Destructors

        public PanelObjectManager(
            IObjectPanel<TObject> objectPanel,
            PanelDirection direction,
            Vector2 defaultObjectSize,
            Func<TObject, uint> orderFunction,
            Func<TObject, Vector2, Vector2> objectSizeFunction = null)
        {
            this.ObjectPanel = objectPanel;
            this.Objects = new List<TObject>();
            this.OrderFunction = orderFunction;
            this.Direction = direction;
            this.defaultObjectSize = defaultObjectSize;
            this.ObjectSize = defaultObjectSize;
            this.ObjectSizeFunction = objectSizeFunction ?? ((o, vector2) => this.ObjectSize);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the direction.
        /// </summary>
        public PanelDirection Direction
        {
            get
            {
                return this.direction;
            }

            set
            {
                this.direction = value;
                this.UpdateSize();
            }
        }

        /// <summary>
        ///     Gets the object panel.
        /// </summary>
        public IObjectPanel<TObject> ObjectPanel { get; }

        /// <summary>
        ///     Gets or sets the objects.
        /// </summary>
        public List<TObject> Objects
        {
            get
            {
                return this.objects;
            }

            set
            {
                this.objects = value;
                this.UpdateSize();
            }
        }

        /// <summary>
        ///     Gets or sets the object size.
        /// </summary>
        public Vector2 ObjectSize { get; private set; }

        /// <summary>
        ///     Gets or sets the object size function.
        /// </summary>
        public Func<TObject, Vector2, Vector2> ObjectSizeFunction { get; set; }

        /// <summary>
        ///     Gets or sets the order function.
        /// </summary>
        public Func<TObject, uint> OrderFunction { get; set; }

        /// <summary>
        ///     Gets or sets the position change after object.
        /// </summary>
        public Func<TObject, Vector2> PositionChangeAfterObject { get; set; }

        /// <summary>
        ///     Gets or sets the position change before object.
        /// </summary>
        public Func<TObject, Vector2> PositionChangeBeforeObject { get; set; }

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
            }
        }

        /// <summary>
        ///     Gets or sets the size increase.
        /// </summary>
        public float SizeIncrease
        {
            get
            {
                return this.sizeIncrease;
            }

            set
            {
                this.sizeIncrease = value;
                this.ObjectSize = this.defaultObjectSize * new Vector2(this.sizeIncrease);
                this.UpdateSize();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add object.
        /// </summary>
        /// <param name="panelObject">
        ///     The panel object.
        /// </param>
        public void AddObject(TObject panelObject)
        {
            this.SetObjectSize(panelObject);
            var temp = this.Objects.ToList();
            temp.Add(panelObject);
            if (this.OrderFunction != null)
            {
                temp = temp.OrderByDescending(this.OrderFunction).ToList();
            }

            this.Objects = temp;
        }

        public void DrawObjects()
        {
            var position = this.ObjectPanel.Position;
            foreach (var o in this.Objects)
            {
                if (this.ObjectPanel.Panel.PanelDirection == PanelDirection.Left && o.Size.X < this.defaultObjectSize.X)
                {
                    o.Position = position + new Vector2(this.ObjectSize.X - o.Size.X, 0);
                }
                else if (this.ObjectPanel.Panel.PanelDirection == PanelDirection.Top && o.Size.Y < this.defaultObjectSize.Y)
                {
                    o.Position = position + new Vector2(0, this.ObjectSize.Y - o.Size.Y);
                }
                else
                {
                    o.Position = position;
                }

                o.Draw();
                if (this.direction == PanelDirection.Bottom)
                {
                    position += new Vector2(0, o.Size.Y);
                }
                else
                {
                    position += new Vector2(o.Size.X, 0);
                }
            }
        }

        /// <summary>
        ///     The remove object.
        /// </summary>
        /// <param name="panelObject">
        ///     The panel object.
        /// </param>
        public void RemoveObject(TObject panelObject)
        {
            var temp = this.Objects.ToList();
            temp.Remove(panelObject);
            this.Objects = temp;
        }

        public void SetObjectSize(TObject panelObject)
        {
            var size = this.ObjectSizeFunction.Invoke(panelObject, this.ObjectSize);
            if (this.Direction == PanelDirection.Bottom && size.X > this.ObjectSize.X)
            {
                var diff = size.X - this.ObjectSize.X;
                var percentage = diff / size.X;
                size -= new Vector2(diff, size.Y * percentage);
            }
            else if (this.Direction == PanelDirection.Right && size.Y > this.ObjectSize.Y)
            {
                var diff = size.Y - this.ObjectSize.Y;
                var percentage = diff / size.Y;
                size -= new Vector2(this.ObjectSize.X * percentage, diff);
            }

            if (this.direction == PanelDirection.Bottom)
            {
                this.Size += new Vector2(0, size.Y);
            }
            else
            {
                this.Size += new Vector2(size.X, 0);
            }

            panelObject.Size = size;
        }

        public void UpdateSize()
        {
            if (!this.Objects.Any())
            {
                this.Size = Vector2.Zero;
                return;
            }

            if (this.direction == PanelDirection.Bottom)
            {
                this.Size = new Vector2(this.ObjectSize.X, 0);
            }
            else if (this.direction == PanelDirection.Right)
            {
                this.Size = new Vector2(0, this.ObjectSize.Y);
            }

            foreach (var o in this.Objects)
            {
                this.SetObjectSize(o);
            }

            this.ObjectPanel.Panel.UpdateSize();
        }

        #endregion
    }
}