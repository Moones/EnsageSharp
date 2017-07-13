namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator.Workers
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;

    internal class MagicalSkillDamageCalculatorWorker : SkillManipulatedDamageCalculatorWorker
    {
        internal MagicalSkillDamageCalculatorWorker(IAbilitySkill skill, IAbilityUnit target)
            : base(skill, target)
        {
        }

        public override void UpdateDamage(float rawDamage)
        {
            throw new NotImplementedException();
        }
    }
}
