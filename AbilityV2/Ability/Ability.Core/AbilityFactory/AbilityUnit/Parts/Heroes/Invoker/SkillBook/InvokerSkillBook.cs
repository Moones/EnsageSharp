// <copyright file="InvokerSkillBook.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.SkillBook
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Invoker.CastData;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.Modifiers;
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.Utilities;

    /// <summary>
    ///     The invoker skill book.
    /// </summary>
    public class InvokerSkillBook : SkillBook<IAbilitySkill>, IObserver<OrbsUpdate>
    {
        #region Fields

        private readonly List<InvokerSkillCastData> castDatas = new List<InvokerSkillCastData>();

        private InvokerModifiers modifiers;

        #endregion

        #region Constructors and Destructors

        internal InvokerSkillBook(IAbilityUnit unit)
            : base(unit)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the exort.
        /// </summary>
        public IAbilitySkill Exort { get; set; }

        /// <summary>
        ///     Gets or sets the invokable skill.
        /// </summary>
        public IAbilitySkill InvokableSkill { get; set; }

        public Notifier InvokableSkillChange { get; } = new Notifier();

        public IAbilitySkill Invoke { get; set; }

        public List<IAbilitySkill> PossibleCombo { get; set; } = new List<IAbilitySkill>(5);

        /// <summary>
        ///     Gets or sets the quas.
        /// </summary>
        public IAbilitySkill Quas { get; set; }

        /// <summary>
        ///     Gets or sets the wex.
        /// </summary>
        public IAbilitySkill Wex { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add skill.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        public override void AddSkill(IAbilitySkill skill)
        {
            if (skill.Name == "invoker_quas")
            {
                this.Quas = skill;
            }
            else if (skill.Name == "invoker_wex")
            {
                this.Wex = skill;
            }
            else if (skill.Name == "invoker_exort")
            {
                this.Exort = skill;
            }
            else if (skill.Name == "invoker_invoke")
            {
                this.Invoke = skill;
            }

            if (!skill.IsItem)
            {
                var castData = skill.CastData as InvokerSkillCastData;
                if (castData != null)
                {
                    this.castDatas.Add(castData);
                    if (this.castDatas.Count == 10)
                    {
                        this.modifiers.OrbsUpdate.Subscribe(this);
                    }
                }
            }

            base.AddSkill(skill);
        }

        public override void Initialize()
        {
            this.modifiers = this.Unit.Modifiers as InvokerModifiers;
            base.Initialize();
        }

        /// <summary>Notifies the observer that the provider has finished sending push-based notifications.</summary>
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        /// <summary>Notifies the observer that the provider has experienced an error condition.</summary>
        /// <param name="error">An object that provides additional information about the error.</param>
        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        /// <summary>Provides the observer with new data.</summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(OrbsUpdate value)
        {
            var invokerSkillCastData =
                this.castDatas.FirstOrDefault(
                    x =>
                        x.QuasCount.Equals(this.modifiers.QuasCount) && x.WexCount.Equals(this.modifiers.WexCount)
                        && x.ExortCount.Equals(this.modifiers.ExortCount));
            if (invokerSkillCastData != null)
            {
                this.InvokableSkill = invokerSkillCastData.Skill;
                this.InvokableSkillChange.Notify();
            }
            else
            {
                Logging.Write()(LogLevel.Debug, "Could not find invokable skill");
            }
        }

        #endregion
    }
}