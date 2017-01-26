// <copyright file="ItemPanel.cs" company="EnsageSharp">
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

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Overlay.Panels.ObjectPanel;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;
    using Ability.Core.AbilityFactory.Utilities;

    using SharpDX;

    /// <summary>
    ///     The item panel.
    /// </summary>
    public class ItemPanel : SkillPanel
    {
        #region Constructors and Destructors

        public ItemPanel(
            IAbilityUnit unit,
            PanelDirection direction,
            Vector2 defaultObjectSize,
            Func<SkillPanelObject, uint> orderFunction = null)
            : base(unit,
                direction,
                defaultObjectSize,
                skill =>
                    {
                        if (!skill.IsItem)
                        {
                            return false;
                        }
                        
                        //Console.WriteLine(skill.Name + " " + skill.SourceItem.IsHidden);
                        return skill.IsItem;
                        
                    },
                orderFunction,
                (o, vector2) =>
                    {
                        if (o.Skill.Cooldown == null && o.Skill.Charges == null)
                        {
                            return vector2 * new Vector2(0.65f);
                        }

                        return vector2;
                    })
        {
            unit.SkillBook.SkillRemove.Subscribe(
                new DataObserver<SkillRemove>(
                    remove =>
                        {
                            if (!this.SelectionCondition.Invoke(remove.Skill))
                            {
                                return;
                            }

                            this.RemoveObject(this.TempDictionary[remove.Skill.SkillHandle]);
                        }));
        }

        #endregion

        #region Public Properties

        public override int DefaultHealthBarPosition { get; } = (int)PanelDirection.Bottom;

        /// <summary>
        ///     Gets a value indicating whether generate menu.
        /// </summary>
        public override bool GenerateMenuBool { get; } = true;

        /// <summary>
        ///     Gets the menu display name.
        /// </summary>
        public override string MenuDisplayName { get; } = "ItemPanel";

        /// <summary>
        ///     Gets the menu name.
        /// </summary>
        public override string MenuName { get; } = nameof(ItemPanel);

        #endregion
    }
}