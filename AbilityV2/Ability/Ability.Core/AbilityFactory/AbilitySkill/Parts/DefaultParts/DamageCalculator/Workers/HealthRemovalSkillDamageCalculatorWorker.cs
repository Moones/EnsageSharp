namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator.Workers
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    internal class HealthRemovalSkillDamageCalculatorWorker : SkillManipulatedDamageCalculatorWorker
    {
        internal HealthRemovalSkillDamageCalculatorWorker(IAbilitySkill skill, IAbilityUnit target)
            : base(skill, target)
        {
        }

        public override void UpdateDamage(float rawDamage)
        {
        }
    }
}
