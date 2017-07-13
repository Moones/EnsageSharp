namespace Ability.Core.AbilityFactory.AbilityTalent.Parts.Default.TalentEffectApllier
{
    public interface ITalentEffectApplier : IAbilityTalentPart
    {
        bool EffectWasApplied { get; set; }

        /// <summary>The apply effect.</summary>
        void ApplyEffect();
    }
}
