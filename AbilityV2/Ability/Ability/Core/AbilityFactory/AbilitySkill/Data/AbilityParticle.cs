// <copyright file="AbilityParticle.cs" company="EnsageSharp">
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
    using System.Collections.ObjectModel;

    using Ability.Utilities;

    using Ensage;

    /// <summary>
    ///     The particle added.
    /// </summary>
    public class AbilityParticle : IObservable<AbilityParticle>
    {
        #region Fields

        /// <summary>
        ///     The particle observers.
        /// </summary>
        private Collection<IObserver<AbilityParticle>> particleObservers = new Collection<IObserver<AbilityParticle>>();

        #endregion

        #region Constructors and Destructors

        public AbilityParticle(IAbilitySkill abilitySkill)
        {
            this.AbilitySkill = abilitySkill;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the ability skill.
        /// </summary>
        public IAbilitySkill AbilitySkill { get; set; }

        /// <summary>
        ///     Gets or sets the particle effect.
        /// </summary>
        public ParticleEffect ParticleEffect { get; set; }

        /// <summary>
        ///     Gets or sets the start time.
        /// </summary>
        public float StartTime { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The particle added.
        /// </summary>
        /// <param name="particleEffect">
        ///     The particle effect.
        /// </param>
        public void ParticleAdded(ParticleEffect particleEffect)
        {
            this.StartTime = Game.RawGameTime - Game.AvgPing / 1000;
            this.AbilitySkill.AreaOfEffect.AbilityParticleEffect(particleEffect);
            foreach (var particleObserver in this.particleObservers)
            {
                particleObserver.OnNext(this);
            }
        }

        /// <summary>Notifies the provider that an observer is to receive notifications.</summary>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has
        ///     finished sending them.
        /// </returns>
        /// <param name="observer">The object that is to receive notifications.</param>
        public IDisposable Subscribe(IObserver<AbilityParticle> observer)
        {
            this.particleObservers.Add(observer);
            return new Unsubscriber<AbilityParticle>(this.particleObservers, observer);
        }

        #endregion
    }
}