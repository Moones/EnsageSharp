namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillModifiers
{
    using Ability.Core.AbilityFactory.AbilityModifier;

    public interface ISkillModifiers : IAbilitySkillPart
    {
        void AddModifier(IAbilityModifier modifier);

        void RemoveModifier(IAbilityModifier modifier);
    }
}
