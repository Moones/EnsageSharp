// <copyright file="ISkillControl.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillControl
{
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastingFunctions;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Types;

    using SharpDX;

    /// <summary>
    ///     The SkillControl interface.
    /// </summary>
    public interface ISkillControl : IAbilitySkillPart
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether added in queue.
        /// </summary>
        bool AddedInQueue { get; set; }

        /// <summary>
        ///     Gets or sets the owner.
        /// </summary>
        IControllableUnit ControllableOwner { get; set; }

        /// <summary>
        ///     Gets or sets the last cast position.
        /// </summary>
        Vector3 LastCastPosition { get; set; }

        /// <summary>
        ///     Gets or sets the last target data.
        /// </summary>
        IAbilityUnit LastTargetData { get; set; }

        /// <summary>
        ///     Gets or sets the last target hit time.
        /// </summary>
        float LastTargetHitTime { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether must execute before attack.
        /// </summary>
        bool MustExecuteBeforeAttack { get; set; }

        /// <summary>
        ///     Gets or sets the casting function.
        /// </summary>
        ISkillCastingFunction SkillCastingFunction { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The cast.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool Cast(IAbilityUnit target);

        #endregion
    }
}