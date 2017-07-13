namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Bristleback.Bristleback.EffectApplier
{
    using System;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.EffectApplier;

    using Ensage.Common.Extensions;

    public class BristlebackEffectApplier : EffectApplier
    {
        public BristlebackEffectApplier(IAbilitySkill skill)
            : base(skill)
        {
            var worker = new EffectApplierWorker(
                false,
                () =>
                    {
                        return unit =>
                            {
                                unit.DamageManipulation.DamageReduction.AddSpecialSkillValue(
                                    skill,
                                    (abilityUnit, damageValue) =>
                                        {
                                            var angle = unit.SourceUnit.FindRelativeAngle(abilityUnit.Position.Current)
                                                        % (2 * Math.PI * 180) / Math.PI;
                                            if (angle >= 110 && angle <= 250)
                                            {
                                                return (1 + skill.Level.Current) * 0.08;
                                            }
                                            else if (angle >= 70 && angle <= 290)
                                            {
                                                return (1 + skill.Level.Current) * 0.04;
                                            }

                                            return 0;
                                        });

                            };
                    },
                () => unit =>
                    {
                        unit.DamageManipulation.DamageReduction.RemoveSpecialSkillValue(skill);
                    },
                null);
            this.Workers.Add(worker);
        }
    }
}
