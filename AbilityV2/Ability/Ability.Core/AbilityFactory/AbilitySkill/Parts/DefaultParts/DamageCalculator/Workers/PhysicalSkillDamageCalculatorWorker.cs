namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator.Workers
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;

    /// <summary>The physical skill damage calculator worker.</summary>
    internal class PhysicalSkillDamageCalculatorWorker : SkillManipulatedDamageCalculatorWorker
    {
        internal PhysicalSkillDamageCalculatorWorker(IAbilitySkill skill, IAbilityUnit target)
            : base(skill, target)
        {
        }

        public override void UpdateDamage(float rawDamage)
        {
            throw new NotImplementedException();
        }
    }
}
