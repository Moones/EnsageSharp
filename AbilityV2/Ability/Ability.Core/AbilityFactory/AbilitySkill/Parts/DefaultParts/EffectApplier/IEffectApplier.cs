namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.EffectApplier
{
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;

    public interface IEffectApplier : IAbilitySkillPart
    {
        ICollection<IEffectApplierWorker> Workers { get; set; }

        void ApplyEffect();

        void UpdateEffect();
    }
}
