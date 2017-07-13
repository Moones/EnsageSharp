namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Wisp.Skillbook
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;

    using Ensage;

    internal class WispSkillbook : SkillBook<IAbilitySkill>
    {
        internal WispSkillbook(IAbilityUnit unit)
            : base(unit)
        {
        }

        public IAbilitySkill Overcharge { get; set; }

        public override void AddSkill(IAbilitySkill skill)
        {
            base.AddSkill(skill);

            if (!skill.IsItem && skill.SourceAbility.Id == AbilityId.wisp_overcharge)
            {
                this.Overcharge = skill;
            }
        }
    }
}
