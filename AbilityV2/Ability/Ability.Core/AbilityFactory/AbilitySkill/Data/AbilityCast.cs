// <copyright file="AbilityCast.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Data
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    /// <summary>
    ///     The ability cast.
    /// </summary>
    public class AbilityCast : IObservable<AbilityCast>
    {
        #region Fields

        #endregion

        #region Constructors and Destructors

        public AbilityCast(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the cast time.
        /// </summary>
        public float CastTime { get; set; }

        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        public IAbilitySkill Skill { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The casted.
        /// </summary>
        public void Casted()
        {
            this.CastTime = Game.RawGameTime;
            this.abilityCastProvider.Next(this);
        }

        private DataProvider<AbilityCast> abilityCastProvider = new DataProvider<AbilityCast>();

        /// <summary>Notifies the provider that an observer is to receive notifications.</summary>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has
        ///     finished sending them.
        /// </returns>
        /// <param name="observer">The object that is to receive notifications.</param>
        public IDisposable Subscribe(IObserver<AbilityCast> observer)
        {
            return this.abilityCastProvider.Subscribe(observer);
        }

        #endregion
    }
}