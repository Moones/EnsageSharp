namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.EmberSpirit.SkillBook
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;

    using Ensage;
    using Ensage.Common.Extensions;

    /// <summary>The ember spirit skill book.</summary>
    internal class EmberSpiritSkillBook : SkillBook<IAbilitySkill>
    {
        /// <summary>Initializes a new instance of the <see cref="EmberSpiritSkillBook"/> class.</summary>
        /// <param name="unit">The unit.</param>
        internal EmberSpiritSkillBook(IAbilityUnit unit)
            : base(unit)
        {
        }

        /// <summary>Gets or sets the flame guard.</summary>
        public IAbilitySkill FlameGuard { get; set; }

        /// <summary>The add skill.</summary>
        /// <param name="skill">The skill.</param>
        public override void AddSkill(IAbilitySkill skill)
        {
            base.AddSkill(skill);

            if (!skill.IsItem && skill.SourceAbility.Id.Equals(AbilityId.ember_spirit_flame_guard))
            {
                this.FlameGuard = skill;
            }
        }
    }
}
