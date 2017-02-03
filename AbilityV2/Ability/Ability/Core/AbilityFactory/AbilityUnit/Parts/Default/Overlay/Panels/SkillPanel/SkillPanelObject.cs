// <copyright file="SkillPanelObject.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.SkillPanel
{
    using System;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillOverlay;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.PanelFields;
    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;

    using Ensage.Common.Menu;

    using SharpDX;

    /// <summary>The skill panel object.</summary>
    public class SkillPanelObject : IUnitOverlayElement
    {
        #region Fields

        private ISkillOverlay overlay;

        /// <summary>
        ///     The position.
        /// </summary>
        private Vector2 position;

        /// <summary>
        ///     The size.
        /// </summary>
        private Vector2 size;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="SkillPanelObject" /> class.</summary>
        /// <param name="skill">The skill.</param>
        public SkillPanelObject(IAbilitySkill skill)
        {
            this.Unit = skill.Owner;
            this.Skill = skill;
            this.overlay = this.Skill.OverlayProvider.Generate();
        }

        #endregion

        #region Public Properties

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
                this.overlay.Position = this.position;
            }
        }

        /// <summary>
        ///     Gets the position from health bar.
        /// </summary>
        public Vector2 PositionFromHealthBar { get; }

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
                this.overlay.Size = this.size;
            }
        }

        /// <summary>
        ///     Gets or sets the size increase.
        /// </summary>
        public float SizeIncrease { get; set; }

        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        public IAbilitySkill Skill { get; set; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

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
        public void ConnectToMenu(IUnitMenu menu)
        {
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
            throw new NotImplementedException();
        }

        /// <summary>The dispose.</summary>
        public void Dispose()
        {
            this.overlay.Dispose();
            this.overlay = null;
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        public void Draw()
        {
            this.overlay.Draw();
        }

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

        #endregion
    }
}