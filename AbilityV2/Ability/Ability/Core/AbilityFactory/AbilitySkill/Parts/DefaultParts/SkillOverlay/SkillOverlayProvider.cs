// <copyright file="SkillOverlayProvider.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillOverlay
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillOverlay.Types;

    public class SkillOverlayProvider : ISkillOverlayProvider
    {
        #region Constructors and Destructors

        public SkillOverlayProvider(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        public IAbilitySkill Skill { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public virtual void Dispose()
        {
        }

        /// <summary>
        ///     The generate.
        /// </summary>
        /// <returns>
        ///     The <see cref="ISkillOverlay" />.
        /// </returns>
        public virtual ISkillOverlay Generate()
        {
            if (this.Skill.Charges != null)
            {
                return new ChargesItemOverlay(this.Skill);
            }

            return !this.Skill.IsItem
                       ? (ISkillOverlay)
                       (this.Skill.CastData.CastPoint > 0.18
                            ? new CastPointSpellOverlay(this.Skill)
                            : new SpellOverlay(this.Skill))
                       : new ItemOverlay(this.Skill);
        }

        public virtual void Initialize()
        {
        }

        #endregion
    }
}