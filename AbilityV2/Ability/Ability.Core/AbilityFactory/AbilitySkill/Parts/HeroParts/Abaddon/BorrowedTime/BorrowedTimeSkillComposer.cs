namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Abaddon.BorrowedTime
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;

    using Ensage.Common.Enums;

    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata((uint)AbilityId.abaddon_borrowed_time)]
    internal class BorrowedTimeSkillComposer : DefaultSkillComposer
    {
        internal BorrowedTimeSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_abaddon_borrowed_time_damage_redirect",
                                            modifier =>
                                                modifier.AssignModifierEffectApplier(
                                                    new ModifierEffectApplier(modifier)
                                                        {
                                                            Workers =
                                                                new List<IEffectApplierWorker>
                                                                    {
                                                                        new ReduceOtherEffectApplierWorker(
                                                                            modifier,
                                                                            false,
                                                                            abilityModifier => 0.5)
                                                                    }
                                                        }),
                                            false,
                                            true),
                                        new ModifierGeneratorWorker(
                                            "modifier_abaddon_borrowed_time",
                                            modifier =>
                                                modifier.AssignModifierEffectApplier(
                                                    new ModifierEffectApplier(modifier)
                                                        {
                                                            Workers =
                                                                new List<IEffectApplierWorker>
                                                                    {
                                                                        new DamageShieldEffectApplierWorker(
                                                                            modifier,
                                                                            false,
                                                                            true,
                                                                            true,
                                                                            true)
                                                                    }
                                                        }),
                                            false,
                                            false,
                                            true)
                                    }
                        });
        }
    }
}
