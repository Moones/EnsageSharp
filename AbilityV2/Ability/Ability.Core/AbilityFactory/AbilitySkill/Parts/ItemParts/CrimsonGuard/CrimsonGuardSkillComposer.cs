namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.CrimsonGuard
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.EffectApplier;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;
    
    using Ensage.Common.Extensions;

    using AbilityId = Ensage.AbilityId;

    [Export(typeof(IAbilitySkillItemComposer))]
    [AbilitySkillItemMetadata((uint)AbilityId.item_crimson_guard)]
    internal class CrimsonGuardSkillComposer :DefaultSkillComposer
    {
        internal CrimsonGuardSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_item_crimson_guard_extra",
                                            modifier =>
                                                modifier.AssignModifierEffectApplier(
                                                    new ModifierEffectApplier(modifier)
                                                        {
                                                            Workers =
                                                                new List<IEffectApplierWorker>
                                                                    {
                                                                        new DamageBlockEffectApplierWorker(
                                                                            modifier,
                                                                            false,
                                                                            abilityModifier =>
                                                                                abilityModifier.SourceSkill.SourceItem
                                                                                    .GetAbilityData(
                                                                                        abilityModifier.AffectedUnit
                                                                                            .SourceUnit.IsRanged
                                                                                            ? "block_damage_ranged_active"
                                                                                            : "block_damage_melee_active"))
                                                                    }
                                                        }),
                                            false,
                                            true,
                                            true)
                                    }
                        });
            this.AssignPart<IEffectApplier>(
                skill =>
                    new EffectApplier(skill)
                        {
                            Workers =
                                new List<IEffectApplierWorker>
                                    {
                                        new DamageBlockEffectApplierWorker(
                                            skill,
                                            false,
                                            abilitySkill =>
                                                abilitySkill.SourceItem.GetAbilityData(
                                                    abilitySkill.Owner.SourceUnit.IsRanged
                                                        ? "block_damage_ranged"
                                                        : "block_damage_melee"))
                                    }
                        });
        }
    }
}
