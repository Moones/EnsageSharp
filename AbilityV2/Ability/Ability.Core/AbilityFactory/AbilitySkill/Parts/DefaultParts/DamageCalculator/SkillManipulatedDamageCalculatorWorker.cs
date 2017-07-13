namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    /// <summary>The skill damage calculator worker.</summary>
    internal abstract class SkillManipulatedDamageCalculatorWorker : ISkillManipulatedDamageCalculatorWorker
    {
        private float damageValue;

        /// <summary>Initializes a new instance of the <see cref="SkillManipulatedDamageCalculatorWorker"/> class.</summary>
        /// <param name="skill">The skill.</param>
        /// <param name="target">The target.</param>
        protected SkillManipulatedDamageCalculatorWorker(IAbilitySkill skill, IAbilityUnit target)
        {
            this.Skill = skill;
            this.Target = target;
        }

        /// <summary>The target.</summary>
        public IAbilityUnit Target { get; set; }

        /// <summary>The skill.</summary>
        public IAbilitySkill Skill { get; set; }

        /// <summary>Gets or sets the damage value.</summary>
        public float DamageValue
        {
            get
            {
                return this.damageValue;
            }

            set
            {
                if (Math.Abs(this.damageValue - value) < 1)
                {
                    return;
                }

                this.DamageChanged.Notify();
                this.damageValue = value;
            }
        }

        public Notifier DamageChanged { get; } = new Notifier();

        public abstract void UpdateDamage(float rawDamage);

        public virtual void Dispose()
        {
            this.DamageChanged.Dispose();
        }
    }
}
