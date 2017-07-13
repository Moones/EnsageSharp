// <copyright file="Cooldown.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.Cooldown
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Objects.UtilityObjects;

    /// <summary>
    ///     The cooldown.
    /// </summary>
    internal class Cooldown : DataProvider<ICooldown>, ICooldown
    {
        #region Fields

        /// <summary>The cooldown sleeper.</summary>
        private readonly Sleeper cooldownSleeper = new Sleeper();

        /// <summary>
        ///     The current.
        /// </summary>
        private float current;

        /// <summary>The last change time.</summary>
        private float lastChangeTime;

        private double lastValue;

        private bool manualCalculation;

        /// <summary>
        ///     The was ready.
        /// </summary>
        private bool wasReady;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Cooldown" /> class.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        internal Cooldown(IAbilitySkill skill)
        {
            this.Skill = skill;
            this.Max = skill.SourceAbility.CooldownLength;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        public float Current
        {
            get
            {
                return this.current;
            }

            set
            {
                var isZero = value.Equals(0);
                if (isZero && !this.current.Equals(0))
                {
                    this.OffCooldownProvider.Next(this);
                }

                this.current = value;
                if (!this.manualCalculation && !this.lastValue.Equals(this.current))
                {
                    this.lastChangeTime = Game.RawGameTime;
                }

                if (this.current > this.Max)
                {
                    this.Max = this.current;
                }

                if (isZero)
                {
                    this.wasReady = true;
                }
                else if (this.wasReady)
                {
                    this.wasReady = false;
                    this.Skill.AbilityCast?.Casted();
                }

                this.Percentage = this.current * 100 / this.Max;
                this.Next(this);
            }
        }

        /// <summary>
        ///     Gets or sets the max.
        /// </summary>
        public float Max { get; set; }

        /// <summary>
        ///     Gets the off cooldown provider.
        /// </summary>
        public DataProvider<ICooldown> OffCooldownProvider { get; } = new DataProvider<ICooldown>();

        /// <summary>
        ///     Gets or sets the percentage.
        /// </summary>
        public float Percentage { get; set; }

        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        public IAbilitySkill Skill { get; set; }

        #endregion

        /// <summary>The cooldown updater.</summary>
        private ActionExecutor cooldownUpdater;

        #region Public Methods and Operators

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public virtual void Dispose()
        {
            this.cooldownUpdater.Dispose();
        }

        /// <summary>The initialize.</summary>
        public virtual void Initialize()
        {
            this.cooldownUpdater = new ActionExecutor(this.Update);
            this.cooldownUpdater.Subscribe(this.Skill.Owner.DataReceiver.Updates);
        }

        #endregion

        #region Methods

        /// <summary>The update.</summary>
        private void Update()
        {
            if (!this.Skill.SourceAbility.IsValid)
            {
                return;
            }

            if (!this.Skill.Owner.Visibility.Visible)
            {
                if (this.Current <= 0)
                {
                    return;
                }

                this.manualCalculation = true;
                var calculatedValue = this.lastValue - Math.Floor(Game.RawGameTime - this.lastChangeTime);
                if (calculatedValue <= 0)
                {
                    this.manualCalculation = false;
                    this.Current = 0;
                    return;
                }

                this.Current = (float)Math.Ceiling(calculatedValue);
                this.manualCalculation = false;
                return;
            }

            if (!this.cooldownSleeper.Sleeping)
            {
                this.lastValue = this.Skill.SourceAbility.Cooldown - Math.Min(Game.AvgPing, 1000) / 1000;
                this.Current = (float)Math.Ceiling(this.lastValue);
                this.cooldownSleeper.Sleep(250);
            }
            else if (this.Current <= 0.5 && this.Skill.SourceAbility.Cooldown > 0.5)
            {
                this.lastValue = this.Skill.SourceAbility.Cooldown;
                this.Current = (float)Math.Ceiling(this.lastValue);
                this.cooldownSleeper.Sleep(1000);
            }
            else if (this.Current > 0.5 && this.Skill.SourceAbility.Cooldown <= 0.5)
            {
                this.lastValue = this.Skill.SourceAbility.Cooldown;
                this.Current = (float)Math.Ceiling(this.lastValue);
                this.cooldownSleeper.Sleep(1000);
            }
        }

        #endregion
    }
}