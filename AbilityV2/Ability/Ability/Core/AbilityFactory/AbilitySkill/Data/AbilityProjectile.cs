// <copyright file="AbilityProjectile.cs" company="EnsageSharp">
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
    ///     The ability projectile.
    /// </summary>
    public class AbilityProjectile : IObservable<AbilityProjectile>
    {
        #region Fields

        /// <summary>
        ///     The projectile observers.
        /// </summary>
        private Collection<IObserver<AbilityProjectile>> projectileObservers =
            new Collection<IObserver<AbilityProjectile>>();

        #endregion

        #region Constructors and Destructors

        public AbilityProjectile(IAbilitySkill skill)
        {
            this.AbilitySkill = skill;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the ability skill.
        /// </summary>
        public IAbilitySkill AbilitySkill { get; set; }

        /// <summary>
        ///     Gets or sets the start time.
        /// </summary>
        public float StartTime { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The projectile added.
        /// </summary>
        /// <param name="projectile">
        ///     The projectile.
        /// </param>
        public void ProjectileAdded(TrackingProjectile projectile)
        {
            this.StartTime = Game.RawGameTime - Game.AvgPing / 1000;
            this.AbilitySkill.AreaOfEffect.AbilityProjectile(projectile);
            foreach (var particleObserver in this.projectileObservers)
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
        public IDisposable Subscribe(IObserver<AbilityProjectile> observer)
        {
            this.projectileObservers.Add(observer);
            return new Unsubscriber<AbilityProjectile>(this.projectileObservers, observer);
        }

        #endregion
    }
}