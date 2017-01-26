// <copyright file="IAbilityAction.cs" company="EnsageSharp">
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
namespace Ability.Core.ActionQueue.AbilityAction
{
    using System;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit;

    /// <summary>
    ///     The AbilityAction interface.
    /// </summary>
    public interface IAbilityAction
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the cancel delay.
        /// </summary>
        Func<float> CancelDelay { get; set; }

        /// <summary>
        ///     Gets or sets the cancel function.
        /// </summary>
        Func<bool> CancelFunction { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether casting id.
        /// </summary>
        uint CastingId { get; set; }

        /// <summary>
        ///     Gets or sets the wait time after execution.
        /// </summary>
        Func<bool> ConfirmExecutionFunction { get; set; }

        /// <summary>
        ///     Gets or sets the execution duration.
        /// </summary>
        Func<float> ExecutionDuration { get; set; }

        /// <summary>
        ///     Gets or sets the execution id.
        /// </summary>
        uint ExecutionId { get; set; }

        /// <summary>
        ///     Gets or sets the execution interval sleep.
        /// </summary>
        float ExecutionIntervalSleep { get; set; }

        /// <summary>
        ///     Gets or sets the hold execution function.
        /// </summary>
        Func<IAbilitySkill, bool> HoldExecutionFunction { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        uint Id { get; set; }

        /// <summary>
        ///     Gets or sets the is executed.
        /// </summary>
        Func<bool> IsExecuted { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is getting executed.
        /// </summary>
        bool IsGettingExecuted { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        IAbilitySkill Skill { get; set; }

        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        IAbilityUnit Source { get; set; }

        /// <summary>
        ///     Gets or sets the target.
        /// </summary>
        IAbilityUnit Target { get; set; }

        /// <summary>
        ///     Gets or sets the target affected delay.
        /// </summary>
        Func<float> TargetAffectedDelay { get; set; }

        /// <summary>
        ///     Gets or sets the action.
        /// </summary>
        Func<bool> TryExecute { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        AbilityActionType Type { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether wait for disable.
        /// </summary>
        bool WaitForDisable { get; set; }

        #endregion
    }
}