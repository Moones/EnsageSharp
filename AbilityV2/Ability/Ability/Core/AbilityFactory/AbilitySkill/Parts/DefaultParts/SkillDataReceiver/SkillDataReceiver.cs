// <copyright file="SkillDataReceiver.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillDataReceiver
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Ensage;
    using Ensage.Common.Extensions;

    /// <summary>
    ///     The skill data receiver.
    /// </summary>
    public class SkillDataReceiver : ISkillDataReceiver
    {
        #region Fields

        private bool skillRemoved;

        #endregion

        #region Constructors and Destructors

        public SkillDataReceiver(IAbilitySkill skill)
        {
            this.Skill = skill;
            this.Skill.DisposeNotifier.Subscribe(() => this.skillRemoved = true);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        public IAbilitySkill Skill { get; set; }

        /// <summary>Gets the updates.</summary>
        public Collection<Action> Updates { get; } = new Collection<Action>();

        #endregion

        #region Public Methods and Operators

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public virtual void Dispose()
        {
        }

        /// <summary>
        ///     The entity_ on bool property change.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void Entity_OnBoolPropertyChange(Entity sender, BoolPropertyChangeEventArgs args)
        {
        }

        /// <summary>
        ///     The entity_ on float property change.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void Entity_OnFloatPropertyChange(Entity sender, FloatPropertyChangeEventArgs args)
        {
        }

        /// <summary>
        ///     The entity_ on particle effect added.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void Entity_OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
        {
            if (this.Skill.Json.ParticleNames.Any(particleName => args.Name.EndsWith(particleName)))
            {
                this.Skill.AbilityParticle.ParticleAdded(args.ParticleEffect);
            }
        }

        /// <summary>
        ///     The cooldown sleeper.
        /// </summary>
        /// <summary>
        ///     The game_ on update.
        /// </summary>
        public void Game_OnUpdate()
        {
            if (this.skillRemoved)
            {
                return;
            }

            // if (!this.Skill.SourceAbility.IsValid)
            // {
            // Console.WriteLine("invalid ability: " + this.Skill.Name + " source:" + this.Skill.Owner.Name);
            // return;
            // }
            foreach (var update in this.Updates)
            {
                update.Invoke();
            }
        }

        /// <summary>The initialize.</summary>
        public void Initialize()
        {
        }

        /// <summary>
        ///     The object manager_ on add tracking projectile.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void ObjectManager_OnAddTrackingProjectile(TrackingProjectileEventArgs args)
        {
            if (args.Projectile.Source.Equals(this.Skill.Owner.SourceUnit)
                && this.Skill.SourceAbility.GetProjectileSpeed().Equals(args.Projectile.Speed))
            {
                this.Skill.AbilityProjectile.ProjectileAdded(args.Projectile);
            }
        }

        #endregion
    }
}