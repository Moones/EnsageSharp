namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Heroes.Bloodseeker.Bloodrage.ModifierEffectApplier
{
    using System;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;

    using Ensage.Common.Extensions;

    public class BloodrageModifierEffectApplier : ModifierEffectApplier
    {
        private double value;

        private double realValue;

        private EffectApplierWorker ampApplier;

        public BloodrageModifierEffectApplier(IAbilityModifier modifier)
            : base(modifier)
        {
            this.ampApplier = new EffectApplierWorker(
                true,
                () =>
                    {
                        return unit =>
                        {
                            this.realValue = Math.Floor(this.Modifier.SourceSkill.SourceAbility.GetAbilityData("damage_increase_pct")) / 100;
                            this.value = this.realValue;
                            unit.DamageManipulation.DamageAmplification.AddSpecialModifierValue(
                                    modifier,
                                    (abilityUnit, damageValue) =>
                                        {
                                            if (
                                                modifier.AffectedUnit.Position.Current.Distance2D(
                                                    abilityUnit.Position.Current) < 2200)
                                            {
                                                return this.value;
                                            }
                                            else
                                            {
                                                return this.value / 2;
                                            }
                                        },
                                    true);
                                unit.DamageManipulation.AmpFromMe.AddSpecialModifierValue(
                                    modifier,
                                    (abilityUnit, damageValue) =>
                                        {
                                            if (
                                                modifier.AffectedUnit.Position.Current.Distance2D(
                                                    abilityUnit.Position.Current) < 2200)
                                            {
                                                return this.value;
                                            }
                                            else
                                            {
                                                return this.value / 2;
                                            }
                                        },
                                    true);
                            };
                    },
                () =>
                    {
                        return unit =>
                            {
                                unit.DamageManipulation.DamageAmplification.RemoveSpecialModifierValue(modifier);
                                unit.DamageManipulation.AmpFromMe.RemoveSpecialModifierValue(modifier);
                            };
                    },
                () =>
                    {
                        return unit =>
                        {
                            this.realValue = Math.Floor(this.Modifier.SourceSkill.SourceAbility.GetAbilityData("damage_increase_pct")) / 100;
                            this.value = this.realValue;
                            unit.DamageManipulation.DamageAmplification.UpdateSpecialModifierValue(
                                    modifier,
                                    (abilityUnit, damageValue) =>
                                        {
                                            if (
                                                modifier.AffectedUnit.Position.Current.Distance2D(
                                                    abilityUnit.Position.Current) < 2200)
                                            {
                                                return this.value;
                                            }
                                            else
                                            {
                                                return this.value / 2;
                                            }
                                        });
                                unit.DamageManipulation.AmpFromMe.UpdateSpecialModifierValue(
                                    modifier,
                                    (abilityUnit, damageValue) =>
                                        {
                                            if (
                                                modifier.AffectedUnit.Position.Current.Distance2D(
                                                    abilityUnit.Position.Current) < 2200)
                                            {
                                                return this.value;
                                            }
                                            else
                                            {
                                                return this.value / 2;
                                            }
                                        });
                            };
                    });

            this.Workers.Add(this.ampApplier);
        }
    }
}
