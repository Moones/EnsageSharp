// <copyright file="InvokerSkillOverlayProvider.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Invoker.Overlay
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillOverlay;

    /// <summary>
    ///     The invoker skill overlay provider.
    /// </summary>
    public class InvokerSkillOverlayProvider : SkillOverlayProvider
    {
        #region Constructors and Destructors

        public InvokerSkillOverlayProvider(IAbilitySkill skill)
            : base(skill)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The generate.
        /// </summary>
        /// <returns>
        ///     The <see cref="ISkillOverlay" />.
        /// </returns>
        public override ISkillOverlay Generate()
        {
            return new InvokerSkillOverlay(this.Skill);
        }

        #endregion
    }
}