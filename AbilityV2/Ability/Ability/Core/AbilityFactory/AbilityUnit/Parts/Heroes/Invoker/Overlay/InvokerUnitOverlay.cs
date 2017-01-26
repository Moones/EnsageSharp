// <copyright file="InvokerUnitOverlay.cs" company="EnsageSharp">
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

    using Ability.Core.AbilityFactory.AbilitySkill.Data;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.ObjectPanel;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.SkillPanel;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Visibility;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.Modifiers;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.SkillBook;

    using Ensage;
    using Ensage.Common;

    using SharpDX;

    /// <summary>
    ///     The invoker unit overlay.
    /// </summary>
    public class InvokerUnitOverlay : UnitOverlayBase,
                                      IObserver<AbilityCast>,
                                      IObserver<SkillAdd>
    {
        #region Fields

        private InvokerSkillBook skillbook;
        private SkillPanel spellPanel2;

        private InvokerModifiers modifiers;

        #endregion

        #region Constructors and Destructors

        public InvokerUnitOverlay(IAbilityUnit unit)
            : base(unit)
        {
            
        }

        #endregion

        public InvokerSkillPanel InvokerSkillPanel { get; set; }

        #region Public Methods and Operators

        public override void Initialize()
        {
            base.Initialize();

            this.BotPanel.AddElement(
                new ItemPanel(
                    this.Unit,
                    PanelDirection.Bottom,
                    new Vector2(this.HealthBar.Size.Y * 3, (float)(this.HealthBar.Size.Y * 2.2))));
            this.skillbook = this.Unit.SkillBook as InvokerSkillBook;
            this.InvokerSkillPanel = new InvokerSkillPanel(
                this.Unit,
                PanelDirection.Bottom,
                new Vector2(this.HealthBar.Size.Y * 3),
                skill =>
                    !skill.IsItem && skill.Name != "invoker_empty1" && skill.Name != "invoker_empty2"
                    && !(skill.SourceAbility.AbilitySlot == AbilitySlot.Slot_1
                         || skill.SourceAbility.AbilitySlot == AbilitySlot.Slot_2
                         || skill.SourceAbility.AbilitySlot == AbilitySlot.Slot_3
                         || skill.SourceAbility.AbilitySlot == AbilitySlot.Slot_6)
                    && (!skill.SourceAbility.IsHidden
                        || skill.SkillHandle.Equals(this.skillbook?.InvokableSkill?.SkillHandle)));

            // {
            // OrderFunction =
            // skill =>
            // skill.SourceAbility.IsHidden ? 0 : 10 + (uint)skill.SourceAbility.AbilitySlot
            // };
            // this.spellPanel1.PositionFromHealthBarFunc = () => new Vector2(-this.spellPanel1.Size.X, 0);
            this.LeftPanel.AddElement(this.InvokerSkillPanel);
            this.spellPanel2 = new SkillPanel(
                this.Unit,
                PanelDirection.Bottom,
                new Vector2(this.HealthBar.Size.Y * 2),
                skill =>
                    !skill.IsItem
                    && !(skill.SourceAbility.AbilitySlot == AbilitySlot.Slot_1
                         || skill.SourceAbility.AbilitySlot == AbilitySlot.Slot_2
                         || skill.SourceAbility.AbilitySlot == AbilitySlot.Slot_3
                         || skill.SourceAbility.AbilitySlot == AbilitySlot.Slot_6) && skill.Name != "invoker_empty1"
                    && skill.Name != "invoker_empty2" && skill.SourceAbility.IsHidden
                    && !skill.Equals(this.skillbook.InvokableSkill),
                o => o.Skill.Json.CastPriority);

            // this.spellPanel2.PositionFromHealthBarLeftFunc =
            // () => new Vector2(-this.spellPanel1.Size.X - this.spellPanel2.Size.X, 0);
            this.LeftPanel.AddElement(this.spellPanel2);

            this.modifiers = this.Unit.Modifiers as InvokerModifiers;
            this.Unit.SkillBook.SkillAdd.Subscribe(this);
            this.skillbook.InvokableSkillChange.Subscribe(
                (() =>
                    {
                        this.InvokerSkillPanel.UpdateSkills();
                        this.spellPanel2.UpdateSkills();
                    }));
            this.Unit.Visibility.CameOutOfFogNotifier.Subscribe(
                () =>
                    {
                        this.InvokerSkillPanel.UpdateSkills();
                        this.spellPanel2.UpdateSkills();
                    });
        }

        /// <summary>Notifies the observer that the provider has finished sending push-based notifications.</summary>
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        /// <summary>Notifies the observer that the provider has experienced an error condition.</summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        /// <summary>Provides the observer with new data.</summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(AbilityCast value)
        {
            this.InvokerSkillPanel.UpdateSkills();
            this.spellPanel2.UpdateSkills();
        }

        /// <summary>Provides the observer with new data.</summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(SkillAdd value)
        {
            if (value.Skill.Name == "invoker_invoke")
            {
                value.Skill.AbilityCast.Subscribe(this);
            }

            // if (!value.Skill.IsItem)
            // {
            // this.spellPanel1.UpdateSkills();
            // this.spellPanel2.UpdateSkills();
            // }
        }

        #endregion
    }
}