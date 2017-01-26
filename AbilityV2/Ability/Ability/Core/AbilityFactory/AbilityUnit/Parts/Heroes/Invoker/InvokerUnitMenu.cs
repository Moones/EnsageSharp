// <copyright file="InvokerUnitMenu.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.Overlay;

    using Ensage.Common.Menu;

    /// <summary>The invoker unit menu.</summary>
    internal class InvokerUnitMenu : HeroMenuBase
    {
        #region Public Properties

        /// <summary>Gets the hero name.</summary>
        public override string HeroName { get; } = "npc_dota_hero_invoker";

        /// <summary>Gets the menu display name.</summary>
        public override string MenuDisplayName { get; } = "Invoker";

        /// <summary>The skill panel sub menu.</summary>
        private Menu skillPanelSubMenu;

        /// <summary>The connect unit.</summary>
        /// <param name="unit">The unit.</param>
        public override void ConnectUnit(IAbilityUnit unit)
        {
            var overlay = unit.Overlay as InvokerUnitOverlay;
            if (this.skillPanelSubMenu == null)
            {
                this.skillPanelSubMenu = ((IUnitOverlayElement)overlay.InvokerSkillPanel).GenerateMenu(this);
            }

            overlay.InvokerSkillPanel.ConnectToMenu(this, this.skillPanelSubMenu);
        }

        #endregion


    }
}