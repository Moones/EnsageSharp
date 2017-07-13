// <copyright file="InvokerSkillCastData.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Invoker.CastData
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillCastData;

    /// <summary>
    ///     The invoker skill cast data.
    /// </summary>
    public class InvokerSkillCastData : SkillCastData
    {
        #region Constructors and Destructors

        public InvokerSkillCastData(IAbilitySkill skill, uint quasCount, uint wexCount, uint exortCount)
            : base(skill)
        {
            this.QuasCount = quasCount;
            this.WexCount = wexCount;
            this.ExortCount = exortCount;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the exort count.
        /// </summary>
        public uint ExortCount { get; }

        /// <summary>
        ///     Gets the quas count.
        /// </summary>
        public uint QuasCount { get; }

        /// <summary>
        ///     Gets the wex count.
        /// </summary>
        public uint WexCount { get; }

        #endregion
    }
}