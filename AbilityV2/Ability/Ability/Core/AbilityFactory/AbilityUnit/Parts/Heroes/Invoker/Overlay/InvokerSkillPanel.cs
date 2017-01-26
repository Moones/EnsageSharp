// <copyright file="InvokerSkillPanel.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.Overlay
{
    using System;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.ObjectPanel;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.SkillPanel;
    using Ability.Core.MenuManager.Menus.Submenus.UnitMenu;

    using Ensage.Common.Menu;

    using SharpDX;

    public class InvokerSkillPanel : SkillPanel
    {
        #region Constructors and Destructors

        public InvokerSkillPanel(
            IAbilityUnit unit,
            PanelDirection direction,
            Vector2 defaultObjectSize,
            Func<IAbilitySkill, bool> selectionCondition,
            Func<SkillPanelObject, uint> orderFunction = null)
            : base(unit, direction, defaultObjectSize, selectionCondition, orderFunction)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether generate menu.
        /// </summary>
        public override bool GenerateMenuBool { get; } = false;

        /// <summary>
        ///     Gets the menu display name.
        /// </summary>
        public override string MenuDisplayName { get; } = "InvokerSpellPanel";

        /// <summary>
        ///     Gets the menu name.
        /// </summary>
        public override string MenuName { get; } = nameof(InvokerSkillPanel);

        /// <summary>Gets the default health bar position.</summary>
        public override int DefaultHealthBarPosition { get; } = (int)PanelDirection.Top;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add submenu.
        /// </summary>
        /// <param name="menu">
        ///     The menu.
        /// </param>
        /// <returns>
        ///     The <see cref="Ensage.Common.Menu.Menu" />.
        /// </returns>
        //public override Menu AddSubmenu(IUnitMenu menu)
        //{
        //    return menu.Menu.AddSubMenu(this.Menu as Menu);
        //}

        #endregion
    }
}