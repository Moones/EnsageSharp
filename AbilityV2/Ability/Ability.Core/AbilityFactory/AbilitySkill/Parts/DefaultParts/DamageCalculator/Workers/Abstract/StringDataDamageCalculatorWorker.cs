namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator.Workers.Abstract
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    using Ensage.Common.Extensions;

    internal class StringDataDamageCalculatorWorker : SkillRawDamageCalculatorWorker
    {
        internal StringDataDamageCalculatorWorker(IAbilitySkill skill, IAbilityUnit target, ISkillManipulatedDamageCalculatorWorker manipulatedDamageWorker)
            : base(skill, target, manipulatedDamageWorker)
        {
        }

        public override void UpdateRawDamage()
        {
            this.RawDamageValue = this.Skill.SourceAbility.GetAbilityData(
                this.Skill.AbilityInfo.DamageString,
                this.Skill.Level.Current);
        }
    }
}
