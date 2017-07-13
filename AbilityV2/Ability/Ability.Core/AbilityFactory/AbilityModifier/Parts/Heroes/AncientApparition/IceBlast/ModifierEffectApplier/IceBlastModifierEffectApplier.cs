namespace Ability.Core.AbilityFactory.AbilityModifier.Parts.Heroes.AncientApparition.IceBlast.ModifierEffectApplier
{
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DamageManipulation;

    using Ensage.Common.Extensions;

    public class IceBlastModifierEffectApplier : ModifierEffectApplier
    {
        public IceBlastModifierEffectApplier(IAbilityModifier modifier)
            : base(modifier)
        {
            var value = 0d;
            var worker = new EffectApplierWorker(
                true,
                () =>
                    {
                        return unit =>
                            {
                                value = modifier.SourceSkill.SourceAbility.GetAbilityData("kill_pct") / 100;
                                unit.DamageManipulation.Aa = new ValueHolder<float>(
                                    (abilityUnit, f) =>
                                        {
                                            var percent = modifier.AffectedUnit.Health.Maximum * value;
                                            if (modifier.AffectedUnit.Health.Current - f <= percent)
                                            {
                                                return (float)percent;
                                            }

                                            return 0;
                                        },
                                    true,
                                    () => modifier.SourceModifier.DieTime);
                            };
                    },
                () => unit => unit.DamageManipulation.Aa = null,
                () =>
                    {
                        return unit =>
                            {
                                value = modifier.SourceSkill.SourceAbility.GetAbilityData("kill_pct") / 100;
                                unit.DamageManipulation.Aa = new ValueHolder<float>(
                                    (abilityUnit, f) =>
                                        {
                                            var percent = modifier.AffectedUnit.Health.Maximum * value;
                                            if (modifier.AffectedUnit.Health.Current - f <= percent)
                                            {
                                                return (float)percent;
                                            }

                                            return 0;
                                        },
                                    true,
                                    () => modifier.SourceModifier.DieTime);
                            };
                    });
            this.Workers.Add(worker);
        }
    }
}
