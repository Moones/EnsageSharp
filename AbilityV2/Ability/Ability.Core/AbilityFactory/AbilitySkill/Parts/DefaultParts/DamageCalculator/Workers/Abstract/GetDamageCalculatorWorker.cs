namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator.Workers.Abstract
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    internal class GetDamageCalculatorWorker : SkillRawDamageCalculatorWorker
    {
        internal GetDamageCalculatorWorker(IAbilitySkill skill, IAbilityUnit target, ISkillManipulatedDamageCalculatorWorker manipulatedDamageWorker)
            : base(skill, target, manipulatedDamageWorker)
        {
        }

        public override void UpdateRawDamage()
        {
            this.RawDamageValue = this.Skill.SourceAbility.GetDamage(this.Skill.Level.Current - 1);
        }
    }
}
