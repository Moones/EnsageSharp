namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Tidehunter.SkillBook
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;

    using Ensage;

    internal class TidehunterSkillbook : SkillBook<IAbilitySkill>
    {
        internal TidehunterSkillbook(IAbilityUnit unit)
            : base(unit)
        {
        }

        public IAbilitySkill KrakenShell { get; set; }

        public override void AddSkill(IAbilitySkill skill)
        {
            base.AddSkill(skill);

            if (!skill.IsItem && skill.SourceAbility.Id == AbilityId.tidehunter_kraken_shell)
            {
                this.KrakenShell = skill;
            }
        }
    }
}
