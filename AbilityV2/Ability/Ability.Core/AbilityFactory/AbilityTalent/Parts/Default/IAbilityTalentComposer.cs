namespace Ability.Core.AbilityFactory.AbilityTalent.Parts.Default
{
    public interface IAbilityTalentComposer
    {
        /// <summary>The compose.</summary>
        /// <param name="modifier">The modifier.</param>
        void Compose(IAbilityTalent modifier);
    }
}
