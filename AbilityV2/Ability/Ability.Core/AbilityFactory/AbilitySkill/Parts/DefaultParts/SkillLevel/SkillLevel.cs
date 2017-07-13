// <copyright file="SkillLevel.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillLevel
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;

    /// <summary>
    ///     The skill level.
    /// </summary>
    public class SkillLevel : ISkillLevel
    {
        #region Fields

        /// <summary>
        ///     The provider.
        /// </summary>
        private readonly DataProvider<ISkillLevel> provider = new DataProvider<ISkillLevel>();

        /// <summary>
        ///     The current.
        /// </summary>
        private uint current;

        #endregion

        #region Constructors and Destructors

        public SkillLevel(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        public uint Current
        {
            get
            {
                return this.current;
            }

            set
            {
                if (this.current == value)
                {
                    return;
                }

                this.current = value;
                this.provider.Next(this);
            }
        }

        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        public IAbilitySkill Skill { get; set; }

        #endregion

        private ActionExecutor levelUpdater;

        #region Public Methods and Operators

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public virtual void Dispose()
        {
            this.levelUpdater.Dispose();
        }

        /// <summary>The initialize.</summary>
        public virtual void Initialize()
        {
            this.levelUpdater = new ActionExecutor(this.Update);
            this.levelUpdater.Subscribe(this.Skill.Owner.DataReceiver.Updates);
        }

        /// <summary>Notifies the provider that an observer is to receive notifications.</summary>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has
        ///     finished sending them.
        /// </returns>
        /// <param name="observer">The object that is to receive notifications.</param>
        public IDisposable Subscribe(IObserver<ISkillLevel> observer)
        {
            return this.provider.Subscribe(observer);
        }

        /// <summary>
        ///     The update.
        /// </summary>
        public virtual void Update()
        {
            this.Current = this.Skill.SourceAbility.Level;
        }

        #endregion
    }
}