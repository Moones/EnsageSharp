namespace Ability.Core.AbilityFactory.AbilityTalent.Parts.Default
{
    public abstract class AbilityTalentComposer : IAbilityTalentComposer
    {
        public abstract void Compose(IAbilityTalent modifier);
    }
}
