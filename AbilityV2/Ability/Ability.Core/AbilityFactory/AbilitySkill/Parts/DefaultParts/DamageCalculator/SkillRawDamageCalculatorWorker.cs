namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator
{
    using System;

    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillLevel;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    internal abstract class SkillRawDamageCalculatorWorker : ISkillRawDamageCalculatorWorker
    {
        private readonly DataObserver<ISkillLevel> levelObserver;

        private float rawDamageValue;

        private ISkillManipulatedDamageCalculatorWorker manipulatedDamageWorker;

        /// <summary>Initializes a new instance of the <see cref="SkillRawDamageCalculatorWorker"/> class.</summary>
        /// <param name="skill">The skill.</param>
        /// <param name="target">The target.</param>
        /// <param name="manipulatedDamageWorker">The manipulated damage worker.</param>
        protected SkillRawDamageCalculatorWorker(IAbilitySkill skill, IAbilityUnit target, ISkillManipulatedDamageCalculatorWorker manipulatedDamageWorker)
        {
            this.Skill = skill;
            this.Target = target;
            this.ManipulatedDamageWorker = manipulatedDamageWorker;

            this.levelObserver = new DataObserver<ISkillLevel>(
                level =>
                {
                    this.UpdateRawDamage();
                });
            this.levelObserver.Subscribe(this.Skill.Level);
        }

        public ISkillManipulatedDamageCalculatorWorker ManipulatedDamageWorker
        {
            get
            {
                return this.manipulatedDamageWorker;
            }

            set
            {
                this.manipulatedDamageWorker = value;
                this.manipulatedDamageWorker.UpdateDamage(this.RawDamageValue);
            }
        }

        /// <summary>The target.</summary>
        public IAbilityUnit Target { get; set; }

        /// <summary>The skill.</summary>
        public IAbilitySkill Skill { get; set; }

        /// <summary>Gets or sets the raw damage value.</summary>
        public float RawDamageValue
        {
            get
            {
                return this.rawDamageValue;
            }

            set
            {
                if (Math.Abs(value - this.rawDamageValue) < 1)
                {
                    return;
                }

                this.manipulatedDamageWorker.UpdateDamage(this.RawDamageValue);
                this.DamageChanged.Notify();
                this.rawDamageValue = value;
            }
        }

        /// <summary>Gets the damage changed.</summary>
        public Notifier DamageChanged { get; } = new Notifier();

        public abstract void UpdateRawDamage();

        public virtual void Dispose()
        {
            this.levelObserver.Dispose();
            this.DamageChanged.Dispose();
            this.ManipulatedDamageWorker.Dispose();
        }
    }
}
