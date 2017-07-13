// <copyright file="LevelUpdater.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.Utilities
{
    using System;

    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillLevel;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Level;

    /// <summary>
    ///     The level updater.
    /// </summary>
    /// <typeparam name="TSource">
    ///     The source
    /// </typeparam>
    /// <typeparam name="TValueType">
    ///     The value type
    /// </typeparam>
    internal class LevelUpdater<TSource, TValueType> : IObserver<ISkillLevel>, IObserver<IUnitLevel>
    {
        #region Constructors and Destructors

        internal LevelUpdater(TSource levelSkill, Func<TValueType> updateFunction)
        {
            this.Source = levelSkill;
            this.UpdateFunction = updateFunction;
            this.Value = this.UpdateFunction.Invoke();

            IAbilitySkill skill;
            if ((skill = this.Source as IAbilitySkill) != null)
            {
                skill.Level.Subscribe(this);
            }
            else
            {
                (this.Source as IAbilityUnit).Level.Subscribe(this);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the Source.
        /// </summary>
        public TSource Source { get; set; }

        /// <summary>
        ///     Gets or sets the update function.
        /// </summary>
        public Func<TValueType> UpdateFunction { get; set; }

        /// <summary>
        ///     Gets the value.
        /// </summary>
        public TValueType Value { get; private set; }

        #endregion

        #region Public Methods and Operators

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
        public void OnNext(ISkillLevel value)
        {
            this.Value = this.UpdateFunction.Invoke();
        }

        /// <summary>Provides the observer with new data.</summary>
        /// <param name="value">The current notification information.</param>
        public void OnNext(IUnitLevel value)
        {
            this.Value = this.UpdateFunction.Invoke();
        }

        #endregion
    }
}