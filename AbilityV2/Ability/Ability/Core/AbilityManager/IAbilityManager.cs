// <copyright file="IAbilityManager.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityManager
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityTeam;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Types;

    using Ensage;

    /// <summary>
    ///     The AbilityUnitManager interface.
    /// </summary>
    public interface IAbilityManager : IAbilityService, IObservable<IAbilityUnit>
    {
        #region Public Events

        /// <summary>
        ///     The skill added.
        /// </summary>
        event SkillEventHandler SkillAdded;

        /// <summary>
        ///     The skill removed.
        /// </summary>
        event SkillEventHandler SkillRemoved;

        /// <summary>
        ///     The unit added.
        /// </summary>
        event UnitEventHandler UnitAdded;

        /// <summary>
        ///     The unit removed.
        /// </summary>
        event UnitEventHandler UnitRemoved;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the allies.
        /// </summary>
        IReadOnlyDictionary<double, Ally> Allies { get; set; }

        /// <summary>
        ///     Gets or sets the units.
        /// </summary>
        IReadOnlyDictionary<double, IControllableUnit> ControllableUnits { get; set; }

        /// <summary>
        ///     Gets or sets the enemies.
        /// </summary>
        IReadOnlyDictionary<double, Enemy> Enemies { get; set; }

        /// <summary>
        ///     Gets or sets the local team.
        /// </summary>
        IAbilityTeam LocalTeam { get; set; }

        /// <summary>
        ///     Gets or sets the teams.
        /// </summary>
        ICollection<IAbilityTeam> Teams { get; set; }

        /// <summary>
        ///     Gets or sets the units.
        /// </summary>
        IReadOnlyDictionary<double, IAbilityUnit> Units { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add skill.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        void AddTemporarySkill(Ability skill);

        /// <summary>
        ///     The add unit.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        void AddUnit(Unit unit);

        /// <summary>
        ///     The remove skill.
        /// </summary>
        /// <param name="skillName">
        ///     The skill Name.
        /// </param>
        void RemoveTemporarySkill(string skillName);

        /// <summary>
        ///     The remove unit.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        void RemoveUnit(Unit unit);

        #endregion
    }
}