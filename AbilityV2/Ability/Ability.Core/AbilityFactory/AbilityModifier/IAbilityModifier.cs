namespace Ability.Core.AbilityFactory.AbilityModifier
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit;

    using Ensage;

    /// <summary>The AbilityModifier interface.</summary>
    public interface IAbilityModifier : IDisposable
    {
        /// <summary>Gets or sets the source skill.</summary>
        IAbilitySkill SourceSkill { get; set; }

        /// <summary>Gets or sets the affected unit.</summary>
        IAbilityUnit AffectedUnit { get; set; }

        /// <summary>Gets or sets the source modifier.</summary>
        Modifier SourceModifier { get; set; }

        /// <summary>Gets or sets the modifier handle.</summary>
        double ModifierHandle { get; set; }

        /// <summary>Gets or sets the name.</summary>
        string Name { get; set; }

        IReadOnlyDictionary<Type, IAbilityModifierPart> Parts { get; }

        /// <summary>Gets or sets the modifier effect applier.</summary>
        IModifierEffectApplier ModifierEffectApplier { get; set; }


        /// <summary>The add part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        /// <param name="part">The part.</param>
        void AddPart<T>(T part) where T : IAbilityModifierPart;

        /// <summary>The get part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        /// <returns>The <see cref="T" />.</returns>
        T GetPart<T>() where T : IAbilityModifierPart;

        /// <summary>
        ///     The on draw.
        /// </summary>
        void OnDraw();

        /// <summary>The remove part.</summary>
        /// <typeparam name="T">The type of part</typeparam>
        void RemovePart<T>() where T : IAbilityModifierPart;

        /// <summary>The assign modifier effect applier.</summary>
        /// <param name="modifierEffectApplier">The modifier effect applier.</param>
        void AssignModifierEffectApplier(IModifierEffectApplier modifierEffectApplier);
    }
}
