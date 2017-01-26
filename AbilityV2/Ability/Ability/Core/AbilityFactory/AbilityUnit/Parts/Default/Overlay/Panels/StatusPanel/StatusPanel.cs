// <copyright file="StatusPanel.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.StatusPanel
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Level;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.ObjectPanel;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.PanelBase;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.MenuItems;
    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;
    using Ability.Utilities;

    using Ensage;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>
    ///     The status panel.
    /// </summary>
    public class StatusPanel : PanelBase, IStatusPanel, IDisposable
    {
        #region Fields

        private readonly Vector2 setSize;

        private DrawRect circleIcon;

        /// <summary>
        ///     The level.
        /// </summary>
        private DrawText level;

        private Number levelNumber;

        private DataObserver<IUnitLevel> levelObserver;

        private Vector2 size;

        private float sizeIncrease;

        /// <summary>
        ///     The unit name.
        /// </summary>
        private DrawText unitName;

        #endregion

        #region Constructors and Destructors

        public StatusPanel(IAbilityUnit unit, Vector2 size)
            : base(unit)
        {
            this.Unit = unit;
            this.unitName = new DrawText
                                {
                                    Color = Color.PapayaWhip, Text = Game.Localize(unit.Name) + " -",
                                    TextSize = new Vector2((float)(size.Y / 2)), Shadow = true
                                };
            this.level = new DrawText()
                             {
                                 Color = Color.PapayaWhip, Text = unit.SourceUnit.Level.ToString(),
                                 TextSize = new Vector2((float)(size.Y / 2)), Shadow = true
                             };
            this.levelNumber = new Number(NumberTextureColor.Default, true, (float)(size.Y )) { Value = 0 };
            this.Background = new DrawRect(this.BackgroundColor) { Size = size };
            this.levelObserver = new DataObserver<IUnitLevel>(
                unitLevel =>
                    {
                        //this.level.Text = unitLevel.Current.ToString();
                        this.levelNumber.Value = (int)unitLevel.Current;
                        this.Panel?.UpdateSize();
                    });
            this.levelObserver.Subscribe(this.Unit.Level);
            this.circleIcon = new DrawRect(this.Unit.Drawer.MinimapIcon) { Size = new Vector2((float)(size.Y * 1.5)) };
            this.setSize = size;

            // this.Size = size;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the background.
        /// </summary>
        public DrawRect Background { get; set; }

        /// <summary>
        ///     Gets or sets the background color.
        /// </summary>
        public Color BackgroundColor { get; set; }

        public override int DefaultHealthBarPosition { get; } = 0;

        public override string Description { get; }

        /// <summary>
        ///     Gets a value indicating whether generate menu.
        /// </summary>
        public override bool GenerateMenuBool { get; } = true;

        /// <summary>
        ///     Gets the menu display name.
        /// </summary>
        public override string MenuDisplayName { get; } = "LevelIcon";

        /// <summary>
        ///     Gets the menu name.
        /// </summary>
        public override string MenuName { get; } = nameof(StatusPanel);

        /// <summary>
        ///     Gets the position from health bar.
        /// </summary>
        public override Vector2 PositionFromHealthBar => new Vector2(0, -this.Size.Y);

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public override Vector2 Size
        {
            get
            {
                return new Vector2(this.circleIcon.Size.X + this.levelNumber.Size.X - this.circleIcon.Size.X / 4, this.circleIcon.Size.Y);
            }

            set
            {
                this.size = value;
                //this.unitName.TextSize = new Vector2((float)(this.size.Y / 2));
                //this.level.TextSize = new Vector2((float)(this.size.Y / 2));
                //this.Background.Size = new Vector2(this.Background.Size.X, this.size.Y);
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
                this.Size = this.setSize * new Vector2(this.sizeIncrease);
            }
        }

        #endregion

        #region Public Methods and Operators

        public override void ConnectSizeIncreaseToMenu()
        {
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            this.levelObserver.Dispose();
        }

        public override Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
                this.circleIcon.Position = this.position + new Vector2(0, -this.circleIcon.Size.X / 10);
                this.levelNumber.Position = this.position + new Vector2((float)(this.circleIcon.Size.X / 1.2), this.circleIcon.Size.X / 12);
            }
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        public override void Draw()
        {
            if (!this.Enabled)
            {
                return;
            }

            //this.Background.Position = this.Position;
            //this.Background.Color = this.BackgroundColor;
            //this.Background.Draw();

            // this.unitName.Position = this.Position + new Vector2(1, 0);
            // this.unitName.Draw();
            this.circleIcon.Draw();

            this.levelNumber.Draw();

            // this.level.Position = this.unitName.Position + new Vector2(this.unitName.Size.X +1, 0);
            // this.level.Draw();
        }

        /// <summary>
        ///     The generate size increase.
        /// </summary>
        public override void GenerateSizeIncrease()
        {
        }

        public override void GenerateHealthBarPosition()
        {
            this.Menu.HealthBarPositionMenuItem =
                new ObservableMenuItem<StringList>(
                    this.MenuName + nameof(this.Menu.HealthBarPositionMenuItem),
                    "Position");
            this.Menu.HealthBarPositionMenuItem.SetValue(
                new StringList(new[] { "Left", "Right" }, this.DefaultHealthBarPosition));
        }

        private bool connectedToMenu;

        private Vector2 position;

        public override void ConnectToMenu(IUnitMenu menu, Menu subMenu)
        {
            if (this.connectedToMenu)
            {
                return;
            }

            this.connectedToMenu = true;
            this.Menu = subMenu as IPanelMenu;
            if (this.Menu != null)
            {
                this.Menu.EnableMenuItem.Provider.Subscribe(new DataObserver<bool>(this.EnabledAction()));
                this.Menu.HealthBarPositionMenuItem.Provider.Subscribe(
                    new DataObserver<StringList>(
                        list =>
                        {
                            if (list.SelectedIndex == 0)
                            {
                                this.ChangePosition(PanelDirection.Left);
                            }
                            else
                            {
                                this.ChangePosition(PanelDirection.Right);
                            }

                            this.Panel?.UpdateSize();
                        }));
            }
        }

        #endregion
    }
}