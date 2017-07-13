namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.Vanguard
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.EffectApplier;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;

    using Ensage;
    using Ensage.Common.Extensions;

    [Export(typeof(IAbilitySkillItemComposer))]
    [AbilitySkillItemMetadata((uint)AbilityId.item_vanguard)]
    internal class VanguardSkillComposer : DefaultSkillComposer
    {
        internal VanguardSkillComposer()
        {
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
