namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Heroes.Undying.FleshGolem.ModifierEffectApplier
{
    using System;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;

    using Ensage.Common.Extensions;

    public class FleshGolemModifierEffectApplier : ModifierEffectApplier
    {
        public FleshGolemModifierEffectApplier(IAbilityModifier modifier)
            : base(modifier)
        {
            var worker = new EffectApplierWorker(
                false,
                () =>
                    {
                        return unit =>
                            {
                                Console.WriteLine("applying undying");
                                unit.DamageManipulation.DamageAmplification.AddSpecialModifierValue(
                                    modifier,
                                    (abilityUnit, f) =>
                                        {
                                            var baseAmp = .05 * modifier.SourceSkill.Level.Current;
                                            var distance =
                                                modifier.SourceSkill.Owner.Position.Current.Distance2D(
                                                    modifier.AffectedUnit.Position.Current);
                                            if (distance <= 200)
                                            {
                                                return baseAmp + 0.15;
                                            }
                                            else if (distance > 750)
                                            {
                                                return 0.1;
                                            }
                                            else
                                            {
                                                return baseAmp + (750 - distance) * 0.03 / 110;
                                            }
                                        });
                            };
                    },
                () => unit =>
                    {
                        unit.DamageManipulation.DamageAmplification.RemoveSpecialModifierValue(modifier);
                    },
                null);
            this.Workers.Add(worker);
        }
    }
}
