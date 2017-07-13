namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DamageManipulation
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.Utilities;

    /// <summary>The DamageReduction interface.</summary>
    public interface IDamageManipulation : IAbilityUnitPart
    {
        Notifier MagicalDamageReductionChanged { get; }

        Notifier PhysicalDamageReductionChanged { get; }

        Notifier PureDamageReductionChanged { get; }

        Notifier BecameInvulnerableNotifier { get; }

        Notifier BecameMagicImmuneNotifier { get; }

        double ReduceOther { get; set; }

        Dictionary<double, Tuple<float, double>> ReduceOtherMinusEvents { get; }
        Dictionary<double, Tuple<float, double>> ReduceOtherPlusEvents { get; }

        IDamageManipulationValues MagicalDamageAbsorb { get; set; }

        IDamageManipulationValues DamageAmplification { get; set; }

        IDamageManipulationValues AmpFromMe { get; set; }

        IDamageManipulationValues DamageReduction { get; set; }

        float ReduceStatic { get; set; }
        Dictionary<double, Tuple<float, float>> ReduceStaticMinusEvents { get; }
        Dictionary<double, Tuple<float, float>> ReduceStaticPlusEvents { get; }

        float DamageBlock { get; set; }

        IDamageManipulationValues DamageNegation { get; }

        IValueHolder<float> Aa { get; set; }
        Dictionary<double, Tuple<float, float>> AaMinusEvents { get; }
        Dictionary<double, Tuple<float, float>> AaPlusEvents { get; }

        IDamageManipulationValues ManaShield { get; set; }

        bool IsMagicImmune { get; set; }

        bool IsAttackImmune { get; set; }

        bool IsInvul { get; set; }


        bool PhysicalDamageShield { get; }
        bool PureDamageShield { get; }
        bool MagicalDamageShield { get; }

        void AddDamageShield(double handle, bool magical, bool physical, bool pure);

        void RemoveDamageShield(double handle, bool magical, bool physical, bool pure);

        /// <summary>Gets the damage blocks.</summary>
        Dictionary<double, float> DamageBlocks { get; }

        void AddDamageBlock(double handle, float value);

        void RemoveDamageBlock(double handle);

        void UpdateDamageBlock(double handle, float newValue);

        /// <summary>The reduce magical damage.</summary>
        /// <param name="source">The source.</param>
        /// <param name="damageValue">The damage value.</param>
        /// <param name="damageAmplification">The damage Amplification.</param>
        /// <param name="minusMagicResistancePerc">The minus Magic Resistance Perc.</param>
        /// <returns>The <see cref="float"/>.</returns>
        float ManipulateIncomingMagicalDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusMagicResistancePerc,
            float time);
        float ManipulateIncomingMagicalDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusMagicResistancePerc);

        /// <summary>The reduce physical damage.</summary>
        /// <param name="damageValue">The damage value.</param>
        /// <param name="damageAmplification">The damage Amplification.</param>
        /// <returns>The <see cref="float"/>.</returns>
        float ManipulateIncomingPhysicalDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusDamageResistancePerc,
            float minusArmor,
            float time);
        float ManipulateIncomingPhysicalDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusDamageResistancePerc,
            float minusArmor);

        /// <summary>The reduce auto attack damage.</summary>
        /// <param name="damageValue">The damage value.</param>
        /// <param name="damageAmplification">The damage amplification.</param>
        /// <param name="minusArmor">The minus armor.</param>
        /// <returns>The <see cref="float"/>.</returns>
        float ManipulateIncomingAutoAttackDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusArmor,
            float time);


        float ManipulateIncomingAutoAttackDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusArmor);

        /// <summary>The reduce pure damage.</summary>
        /// <param name="damageValue">The damage value.</param>
        /// <param name="damageAmplification">The damage Amplification.</param>
        /// <returns>The <see cref="float"/>.</returns>
        float ManipulateIncomingPureDamage(IAbilityUnit source, float damageValue, double damageAmplification,
            float time);
        float ManipulateIncomingPureDamage(IAbilityUnit source, float damageValue, double damageAmplification);
    }
}
