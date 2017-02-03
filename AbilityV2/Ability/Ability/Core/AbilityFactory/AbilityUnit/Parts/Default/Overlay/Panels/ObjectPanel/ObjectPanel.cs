// <copyright file="ObjectPanel.cs" company="EnsageSharp">
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

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.PanelBase;

    using Ensage.Common.Menu;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>The object panel.</summary>
    /// <typeparam name="TObject">Type of object</typeparam>
    public abstract class ObjectPanel<TObject> : PanelBase, IObjectPanel<TObject>
        where TObject : class, IUnitOverlayElement
    {
        #region Fields

        private float sizeIncrease;

        #endregion

        #region Constructors and Destructors

        protected ObjectPanel(
            IAbilityUnit unit,
            PanelDirection direction,
            Vector2 defaultObjectSize,
            Func<TObject, uint> orderFunction = null,
            Func<TObject, Vector2, Vector2> objectSizeFunction = null,
            bool generateMenu = false,
            string menuName = null,
            string menuDisplayName = null)
            : base(unit)
        {
            this.ObjectManager = new PanelObjectManager<TObject>(
                this,
                direction,
                defaultObjectSize,
                orderFunction,
                objectSizeFunction);
            this.GenerateMenuBool = generateMenu;
            this.MenuName = menuName;
            this.MenuDisplayName = menuDisplayName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the background.
        /// </summary>
        public DrawRect Background { get; set; }

        /// <summary>Gets the description.</summary>
        public override string Description { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether draw background.
        /// </summary>
        public bool DrawBackground { get; set; }

        /// <summary>
        ///     Gets a value indicating whether generate menu.
        /// </summary>
        public override bool GenerateMenuBool { get; }

        /// <summary>
        ///     Gets the menu display name.
        /// </summary>
        public override string MenuDisplayName { get; }

        /// <summary>
        ///     Gets the menu name.
        /// </summary>
        public override string MenuName { get; }

        /// <summary>
        ///     Gets the object manager.
        /// </summary>
        public PanelObjectManager<TObject> ObjectManager { get; set; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public override Vector2 Size
        {
            get
            {
                return this.ObjectManager.Size;
            }

            set
            {
                this.ObjectManager.Size = value;
            }
        }

        /// <summary>
        ///     Gets or sets the size increase.
        /// </summary>
        public override float SizeIncrease
        {
            get
            {
                return this.sizeIncrease;
            }

            set
            {
                this.sizeIncrease = value;
                this.ObjectManager.SizeIncrease = this.sizeIncrease;
                this.Panel?.UpdateSize();
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
            this.ObjectManager.AddObject(panelObject);
            this.Panel?.UpdateSize();
        }

        /// <summary>The dispose.</summary>
        public override void Dispose()
        {
            base.Dispose();
            this.ObjectManager.Dispose();
            this.ObjectManager = null;
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        public override void Draw()
        {
            if (this.DrawBackground)
            {
                this.Background.Size = this.ObjectManager.Size;
                this.Background.Position = this.Position;
                this.Background.Draw();
            }

            this.ObjectManager.DrawObjects();
        }

        /// <summary>
        ///     The health bar position action.
        /// </summary>
        /// <returns>
        ///     The <see cref="Action" />.
        /// </returns>
        public override Action<StringList> HealthBarPositionAction()
        {
            return list =>
                {
                    var direction = (PanelDirection)list.SelectedIndex;
                    this.ObjectManager.Direction = direction == PanelDirection.Left || direction == PanelDirection.Right
                                                       ? PanelDirection.Bottom
                                                       : PanelDirection.Right;
                };
        }

        /// <summary>
        ///     The remove object.
        /// </summary>
        /// <param name="panelObject">
        ///     The panel object.
        /// </param>
        public void RemoveObject(TObject panelObject)
        {
            this.ObjectManager.RemoveObject(panelObject);
            this.Panel?.UpdateSize();
        }

        #endregion
    }
}