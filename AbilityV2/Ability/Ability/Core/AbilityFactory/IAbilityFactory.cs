// <copyright file="IAbilityFactory.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilitySkill.Types;
    using Ability.Core.AbilityFactory.AbilityTeam;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Types;

    using Ensage;

    /// <summary>
    ///     The AbilityFactory interface.
    /// </summary>
    public interface IAbilityFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The compose new skill.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        /// <param name="owner">
        ///     The owner.
        /// </param>
        /// <returns>
        ///     The <see cref="IControllableSkill" />.
        /// </returns>
        IControllableSkill CreateNewControllableSkill(Ability skill, IAbilityUnit owner);

        /// <summary>
        ///     The create new controllable unit.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="team">
        ///     The team.
        /// </param>
        /// <returns>
        ///     The <see cref="IControllableUnit" />.
        /// </returns>
        IControllableUnit CreateNewControllableUnit(Unit unit, IAbilityTeam team);

        /// <summary>
        ///     The create new skill.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        /// <param name="owner">
        ///     The owner.
        /// </param>
        /// <returns>
        ///     The <see cref="IAbilitySkill" />.
        /// </returns>
        IUncontrollableSkill CreateNewSkill(Ability skill, IAbilityUnit owner);

        /// <summary>
        ///     The create new unit.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="team">
        ///     The team.
        /// </param>
        /// <returns>
        ///     The <see cref="IAbilityUnit" />.
        /// </returns>
        IUncontrollableUnit CreateNewUnit(Unit unit, IAbilityTeam team);

        #endregion
    }
}