// <copyright file="IUnitControl.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.UnitControl
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Types;
    using Ability.Core.ActionQueue.AbilityAction;

    using Ensage;

    /// <summary>
    ///     The UnitControl interface.
    /// </summary>
    public interface IUnitControl : IAbilityUnitPart
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the attack action.
        /// </summary>
        IAbilityAction AttackAction { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether attacking.
        /// </summary>
        bool Attacking { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether can cast.
        /// </summary>
        bool Busy { get; set; }

        /// <summary>
        ///     Gets the casting queue.
        /// </summary>
        Queue<IAbilityAction> CastingQueue { get; }

        /// <summary>
        ///     Gets or sets the hold execution function.
        /// </summary>
        Func<IAbilitySkill, bool> HoldExecutionFunction { get; set; }

        /// <summary>
        ///     Gets or sets the last action.
        /// </summary>
        IAbilityAction LastAction { get; set; }

        /// <summary>
        ///     Gets or sets the move action.
        /// </summary>
        IAbilityAction MoveAction { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether orbwalking.
        /// </summary>
        bool Orbwalking { get; set; }

        /// <summary>
        ///     Gets or sets the processed action.
        /// </summary>
        IAbilityAction ProcessedAction { get; set; }

        /// <summary>
        ///     Gets or sets the target.
        /// </summary>
        IAbilityUnit Target { get; set; }

        /// <summary>
        ///     Gets or sets the unit obstacles.
        /// </summary>
        Dictionary<uint, Unit> UnitObstacles { get; set; }

        /// <summary>
        ///     Gets or sets the urgent action.
        /// </summary>
        IAbilityAction UrgentAction { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The enqueue action.
        /// </summary>
        /// <param name="action">
        ///     The action.
        /// </param>
        void EnqueueAction(IAbilityAction action);

        /// <summary>
        ///     The set busy.
        /// </summary>
        /// <param name="duration">
        ///     The duration.
        /// </param>
        void SetBusy(float duration);

        /// <summary>
        ///     The set orbwalking state.
        /// </summary>
        /// <param name="state">
        ///     The state.
        /// </param>
        void SetOrbwalkingState(bool state);

        /// <summary>
        ///     The use soul ring.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool UseSoulRing();

        #endregion
    }
}