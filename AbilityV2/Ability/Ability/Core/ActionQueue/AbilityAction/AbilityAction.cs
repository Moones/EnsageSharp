// <copyright file="AbilityAction.cs" company="EnsageSharp">
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
    ///     The ability action.
    /// </summary>
    public class AbilityAction : IAbilityAction
    {
        #region Constructors and Destructors

        public AbilityAction(IAbilityUnit source, AbilityActionType type, IAbilitySkill skill = null)
        {
            if (source != null)
            {
                this.Id = source.UnitHandle + (uint)type;
            }

            if (skill != null)
            {
                this.Id += skill.SkillHandle;
            }

            this.Type = type;
            this.Skill = skill;
            this.Source = source;
            this.CastingId = this.Id + 1;
            this.ExecutionId = this.Id + 2;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the action delay.
        /// </summary>
        public Func<float> ActionDelay { get; set; }

        /// <summary>
        ///     Gets or sets the cancel delay.
        /// </summary>
        public Func<float> CancelDelay { get; set; }

        /// <summary>
        ///     Gets or sets the cancel function.
        /// </summary>
        public Func<bool> CancelFunction { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether casting id.
        /// </summary>
        public uint CastingId { get; set; }

        /// <summary>
        ///     Gets or sets the wait time after execution.
        /// </summary>
        public Func<bool> ConfirmExecutionFunction { get; set; }

        /// <summary>
        ///     Gets or sets the execution duration.
        /// </summary>
        public Func<float> ExecutionDuration { get; set; }

        /// <summary>
        ///     Gets or sets the execution id.
        /// </summary>
        public uint ExecutionId { get; set; }

        /// <summary>
        ///     Gets or sets the execution interval sleep.
        /// </summary>
        public float ExecutionIntervalSleep { get; set; }

        /// <summary>
        ///     Gets or sets the hold execution function.
        /// </summary>
        public Func<IAbilitySkill, bool> HoldExecutionFunction { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        ///     Gets or sets the is executed.
        /// </summary>
        public Func<bool> IsExecuted { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is getting executed.
        /// </summary>
        public bool IsGettingExecuted { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        public IAbilitySkill Skill { get; set; }

        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        public IAbilityUnit Source { get; set; }

        /// <summary>
        ///     Gets or sets the target.
        /// </summary>
        public IAbilityUnit Target { get; set; }

        /// <summary>
        ///     Gets or sets the target affected delay.
        /// </summary>
        public Func<float> TargetAffectedDelay { get; set; }

        /// <summary>
        ///     Gets or sets the action.
        /// </summary>
        public Func<bool> TryExecute { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        public AbilityActionType Type { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether wait for disable.
        /// </summary>
        public bool WaitForDisable { get; set; }

        #endregion
    }
}