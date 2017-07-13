namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.DamageCalculator.Workers.Abstract
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    using Ensage.Common.Extensions;

    internal class StringDataAghaDamageCalculatorWorker : SkillRawDamageCalculatorWorker
    {
        internal StringDataAghaDamageCalculatorWorker(IAbilitySkill skill, IAbilityUnit target, ISkillManipulatedDamageCalculatorWorker manipulatedDamageWorker)
            : base(skill, target, manipulatedDamageWorker)
        {
        }


        public override void UpdateRawDamage()
        {
            this.RawDamageValue = this.Skill.SourceAbility.GetAbilityData(
                this.Skill.AbilityInfo.DamageScepterString,
                this.Skill.Level.Current);
        }
    }
}
