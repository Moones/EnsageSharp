namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;

    public class AbilityModifierComposer : IAbilityModifierComposer
    {
        public AbilityModifierComposer()
        {
            this.AssignPart<IModifierEffectApplier>(
                modifier => new ModifierEffectApplier.ModifierEffectApplier(modifier));
        }

        public IDictionary<Type, Action<IAbilityModifier>> Assignments { get; } =
            new Dictionary<Type, Action<IAbilityModifier>>();

        public void AssignPart<T>(Func<IAbilityModifier, T> factory) where T : IAbilityModifierPart
        {
            //var type = typeof(T);
            //this.Assignments[type] = modifier => modifier.AddPart(factory);
        }

        public void Compose(IAbilityModifier modifier)
        {
            foreach (var keyValuePair in this.Assignments)
            {
                keyValuePair.Value.Invoke(modifier);
            }

            foreach (var keyValuePair in modifier.Parts)
            {
                keyValuePair.Value.Initialize();
            }
        }
    }
}
