namespace Ability.Core.AbilityFactory.AbilityModifier.Parts
{
    using System;

    public interface IAbilityModifierPart : IDisposable
    {
        IAbilityModifier Modifier { get; set; }

        void Initialize();
    }
}
