// <copyright file="AbilityPhase.cs" company="EnsageSharp">
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
    ///     The ability phase.
    /// </summary>
    public class AbilityPhase : IObservable<AbilityPhase>
    {
        #region Fields

        private Collection<IObserver<AbilityPhase>> castStartObservers = new Collection<IObserver<AbilityPhase>>();

        #endregion

        #region Constructors and Destructors

        public AbilityPhase(IAbilitySkill skill)
        {
            this.AbilitySkill = skill;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the ability skill.
        /// </summary>
        public IAbilitySkill AbilitySkill { get; set; }

        public float Duration => Game.RawGameTime - Game.Ping / 1000 - this.StartTime;

        /// <summary>
        ///     Gets or sets a value indicating whether running.
        /// </summary>
        public bool Running { get; set; }

        /// <summary>
        ///     Gets or sets the time.
        /// </summary>
        public float StartTime { get; set; }

        public float TimeRemaining => (float)(this.AbilitySkill.CastData.CastPoint - this.Duration);

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The start.
        /// </summary>
        public void Start()
        {
            this.StartTime = Game.RawGameTime - Game.Ping / 1000;
            this.Running = true;

            // this.AbilitySkill.AreaOfEffect.AbilityPhase(true);
            foreach (var castStartObserver in this.castStartObservers)
            {
                castStartObserver.OnNext(this);
            }
        }

        /// <summary>
        ///     The stop.
        /// </summary>
        public void Stop()
        {
            this.Running = false;

            // this.AbilitySkill.AreaOfEffect.AbilityPhase(false);
            foreach (var castStartObserver in this.castStartObservers)
            {
                castStartObserver.OnNext(this);
            }
        }

        /// <summary>Notifies the provider that an observer is to receive notifications.</summary>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has
        ///     finished sending them.
        /// </returns>
        /// <param name="observer">The object that is to receive notifications.</param>
        public IDisposable Subscribe(IObserver<AbilityPhase> observer)
        {
            this.castStartObservers.Add(observer);
            return new Unsubscriber<AbilityPhase>(this.castStartObservers, observer);
        }

        #endregion
    }
}