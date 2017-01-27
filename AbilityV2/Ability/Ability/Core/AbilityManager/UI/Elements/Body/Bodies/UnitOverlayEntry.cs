// <copyright file="UnitOverlayEntry.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityManager.UI.Elements.Body.Bodies
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillOverlay;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Bars;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Objects;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>
    ///     The unit entry.
    /// </summary>
    public class UnitOverlayEntry : IUnitOverlayEntry
    {
        #region Fields

        private DrawRect bg = new DrawRect(new Color(0, 0, 0, 150)) { Border = true };

        private HealthBar healthBar;

        /// <summary>
        ///     The item overlays.
        /// </summary>
        private Dictionary<double, ISkillOverlay> itemOverlays = new Dictionary<double, ISkillOverlay>();

        private ManaBar manaBar;

        private Vector2 position;

        private DrawRect roundIcon;

        private Dictionary<double, ISkillOverlay> skillOverlays = new Dictionary<double, ISkillOverlay>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnitOverlayEntry" /> class.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="parent">
        ///     The parent.
        /// </param>
        public UnitOverlayEntry(IAbilityUnit unit, IUserInterfaceElement parent)
        {
            this.Unit = unit;
            this.Parent = parent;
            this.Size = new Vector2(this.Parent.Size.X, this.Parent.Size.X / 15);
            this.bg.Size = this.Size;
            this.RoundIcon = Textures.GetHeroRoundTexture(this.Unit.SourceUnit.Name);
            this.roundIcon = new DrawRect(this.RoundIcon) { Size = new Vector2(this.Parent.Size.X / 20) };
            this.healthBar = new HealthBar(
                                 unit,
                                 new Vector2((float)(this.roundIcon.Size.X * 2.3), this.roundIcon.Size.X / 4))
                                 {
                                     Color = this.Unit.IsEnemy ? new Color(230, 70, 70) : new Color(90, 200, 70),
                                     BackgroundColor = Color.Black
                                 };
            this.manaBar = new ManaBar(unit, new Vector2(this.healthBar.Size.X, this.roundIcon.Size.X / 8))
                               {
                                  Color = new Color(70, 90, 200), BackgroundColor = Color.Black 
                               };
            var spellSize = this.roundIcon.Size / (float)1.5;
            foreach (var keyValuePair in this.Unit.SkillBook.Spells)
            {
                var entry = keyValuePair.Value.OverlayProvider.Generate();
                this.skillOverlays.Add(keyValuePair.Key, entry);
                entry.Size = spellSize;
            }

            var itemSize = new Vector2(this.roundIcon.Size.X / (float)1.2, (float)(this.Size.Y / 2.1));
            foreach (var keyValuePair in this.Unit.SkillBook.Items)
            {
                var entry = keyValuePair.Value.OverlayProvider.Generate();
                this.itemOverlays.Add(keyValuePair.Key, entry);
                entry.Size = itemSize;
            }

            this.Position = this.position;
            this.Unit.SkillBook.SkillAdd.Subscribe(
                new DataObserver<SkillAdd>(
                    add =>
                        {
                            if (!add.Skill.IsItem)
                            {
                                var entry = add.Skill.OverlayProvider.Generate();
                                this.skillOverlays.Add(add.Skill.SkillHandle, entry);
                                entry.Size = spellSize;
                            }
                            else
                            {
                                var entry = add.Skill.OverlayProvider.Generate();
                                this.itemOverlays.Add(add.Skill.SkillHandle, entry);
                                entry.Size = itemSize;
                            }

                            this.Position = this.position;
                        }));

            this.Unit.SkillBook.SkillRemove.Subscribe(
                new DataObserver<SkillRemove>(
                    remove =>
                        {
                            if (!remove.Skill.IsItem)
                            {
                                this.skillOverlays.Remove(remove.Skill.SkillHandle);
                            }
                            else
                            {
                                this.itemOverlays.Remove(remove.Skill.SkillHandle);
                            }

                            this.Position = this.position;
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
        public Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
                this.bg.Position = this.position;
                this.roundIcon.CenterOnRectangleHorizontally(this.bg, 5);
                this.healthBar.CenterOnRectangleHorizontally(this.roundIcon);
                this.healthBar.Position = this.healthBar.Position
                                          + new Vector2(
                                              (float)(this.roundIcon.Size.X / 1.2),
                                              -(this.roundIcon.Size.X / 4));
                this.manaBar.Position = this.healthBar.Position + new Vector2(0, this.healthBar.Size.Y);
                var pos = this.healthBar.Position + new Vector2((float)(this.healthBar.Size.X * 1.2), 0);
                foreach (var skillOverlay in this.skillOverlays)
                {
                    skillOverlay.Value.Position = pos;
                    pos += new Vector2(skillOverlay.Value.Size.X, 0);
                }

                var startPos = new Vector2(pos.X + this.healthBar.Size.X / 5, this.position.Y);
                pos = startPos;
                var count = 1;
                foreach (var skillOverlay in this.itemOverlays)
                {
                    skillOverlay.Value.Position = pos;
                    pos += new Vector2(skillOverlay.Value.Size.X, 0);
                    count++;
                    if (count == 4)
                    {
                        pos = startPos + new Vector2(0, skillOverlay.Value.Size.Y);
                    }
                }
            }
        }

        /// <summary>
        ///     Gets the round icon.
        /// </summary>
        public DotaTexture RoundIcon { get; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether visible.
        /// </summary>
        public bool Visible { get; set; }

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        public void Draw()
        {
            // Console.WriteLine("drawing entry");
            this.bg.Draw();
            this.healthBar.Draw();
            this.manaBar.Draw();
            this.roundIcon.Draw();
            foreach (var skillOverlay in this.skillOverlays)
            {
                skillOverlay.Value.Draw();
            }

            foreach (var skillOverlay in this.itemOverlays)
            {
                skillOverlay.Value.Draw();
            }
        }

        public void Initialize()
        {
            throw new NotImplementedException();
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
        }

        /// <summary>
        ///     The mouse up.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public void MouseUp(Vector2 mousePosition)
        {
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