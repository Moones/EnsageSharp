namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.ShadowDemon.SkillBook
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;

    using Ensage;

    internal class ShadowDemonSkillBook : SkillBook<IAbilitySkill>
    {
        internal ShadowDemonSkillBook(IAbilityUnit unit)
            : base(unit)
        {
        }

        public IAbilitySkill SoulCatcher { get; set; }

        public override void AddSkill(IAbilitySkill skill)
        {
            base.AddSkill(skill);

            if (!skill.IsItem && skill.SourceAbility.Id == AbilityId.shadow_demon_soul_catcher)
            {
                this.SoulCatcher = skill;
            }
        }
    }
}
