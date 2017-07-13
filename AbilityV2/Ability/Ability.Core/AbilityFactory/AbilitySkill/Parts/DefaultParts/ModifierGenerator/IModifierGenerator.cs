namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator
{
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityModifier;

    public interface IModifierGenerator : IAbilitySkillPart
    {
        ICollection<ModifierGeneratorWorker> Workers { get; set; }

        bool TryGenerateModifier(IAbilityModifier modifier);
    }
}
