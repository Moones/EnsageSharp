namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Slardar.SkillBook
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;

    using Ensage;

    internal class SlardarSkillbook : SkillBook<IAbilitySkill>
    {
        internal SlardarSkillbook(IAbilityUnit unit)
            : base(unit)
        {
        }

        public IAbilitySkill GuardianSprint { get; private set; }

        public override void AddSkill(IAbilitySkill skill)
        {
            base.AddSkill(skill);

            if (!skill.IsItem && skill.SourceAbility.Id == AbilityId.slardar_sprint)
            {
                this.GuardianSprint = skill;
            }
        }
    }
}
