namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Default
{
    using System;
    using System.Collections.Generic;

    public interface IAbilityModifierComposer
    {
        /// <summary>Gets the assignments.</summary>
        IDictionary<Type, Action<IAbilityModifier>> Assignments { get; }

        /// <summary>
        ///     The compose.
        /// </summary>
        /// <param name="modifier">
        ///     The modifier.
        /// </param>
        void Compose(IAbilityModifier modifier);
    }
}
