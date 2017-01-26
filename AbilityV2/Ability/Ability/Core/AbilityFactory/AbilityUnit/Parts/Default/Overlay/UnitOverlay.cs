// <copyright file="UnitOverlay.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.ObjectPanel;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.SkillPanel;

    using SharpDX;

    /// <summary>
    ///     The unit overlay.
    /// </summary>
    public class UnitOverlay : UnitOverlayBase
    {
        #region Constructors and Destructors

        public UnitOverlay(IAbilityUnit unit)
            : base(unit)
        {
        }

        public override void Initialize()
        {
            base.Initialize();


            var itemPanel = new ItemPanel(
                this.Unit,
                PanelDirection.Bottom,
                new Vector2(this.HealthBar.Size.Y * 3, (float)(this.HealthBar.Size.Y * 2.2)));
            this.RightPanel.AddElement(itemPanel);
            var spellPanel = new SkillPanel(
                this.Unit,
                PanelDirection.Bottom,
                new Vector2(this.HealthBar.Size.Y * 3),
                skill => !skill.IsItem,
                o => 10 - (uint)o.Skill.SourceAbility.AbilitySlot);
            this.LeftPanel.AddElement(spellPanel);
        }

        #endregion
    }
}