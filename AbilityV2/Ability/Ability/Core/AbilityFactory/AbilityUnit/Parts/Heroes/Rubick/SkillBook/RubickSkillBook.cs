// <copyright file="RubickSkillBook.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Rubick.SkillBook
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;

    using Ensage.Common.Enums;
    using Ensage.Common.Extensions;

    /// <summary>The rubick skill book.</summary>
    public class RubickSkillBook : SkillBook<IAbilitySkill>
    {
        #region Constructors and Destructors

        public RubickSkillBook(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>The spell steal.</summary>
        public IAbilitySkill SpellSteal { get; set; }

        /// <summary>The stolen spell.</summary>
        public IAbilitySkill StolenSpell { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The add skill.</summary>
        /// <param name="skill">The skill.</param>
        public override void AddSkill(IAbilitySkill skill)
        {
            if (skill.SourceAbility.GetAbilityId() == AbilityId.rubick_spell_steal)
            {
                this.SpellSteal = skill;
            }
            else if (!skill.IsItem && !skill.Name.Contains("rubick"))
            {
                if (this.StolenSpell != null)
                {
                    this.RemoveSkill(this.StolenSpell);
                }

                this.StolenSpell = skill;
            }

            base.AddSkill(skill);
        }

        #endregion
    }
}