// <copyright file="IPriorityDatabase.cs" company="EnsageSharp">
//    Copyright (c) 2016 Moones.
// </copyright>
namespace Ability.Core.AbilityFactory.Database
{
    using Ability.Core.AbilityFactory.AbilitySkill.Data;

    /// <summary>
    ///     The PriorityDatabase interface.
    /// </summary>
    internal interface IAbilityDatabase
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The get priority.
        /// </summary>
        /// <param name="skillName">
        ///     The skill name.
        /// </param>
        /// <returns>
        ///     The <see cref="uint" />.
        /// </returns>
        uint GetCastPriority(string skillName);

        /// <summary>
        /// The get damage dealt priority.
        /// </summary>
        /// <param name="skillName">
        /// The skill name.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        uint GetDamageDealtPriority(string skillName);

        /// <summary>
        ///     The get priority.
        /// </summary>
        /// <param name="skillName">
        ///     The skill name.
        /// </param>
        /// <param name="heroName">
        ///     The hero name.
        /// </param>
        /// <returns>
        ///     The <see cref="uint" />.
        /// </returns>
        uint GetCastPriority(string skillName, string heroName);

        /// <summary>
        /// The get skill data.
        /// </summary>
        /// <param name="skillName">
        /// The skill name.
        /// </param>
        /// <returns>
        /// The <see cref="SkillJson"/>.
        /// </returns>
        SkillJson GetSkillData(string skillName);

        #endregion
    }
}