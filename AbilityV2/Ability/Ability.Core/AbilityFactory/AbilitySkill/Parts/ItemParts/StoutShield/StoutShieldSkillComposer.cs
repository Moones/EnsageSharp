namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.ItemParts.StoutShield
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

    /// <summary>The stout shield skill composer.</summary>
    [Export(typeof(IAbilitySkillItemComposer))]
    [AbilitySkillItemMetadata((uint)AbilityId.item_stout_shield)]
    internal class StoutShieldSkillComposer : DefaultSkillComposer
    {
        internal StoutShieldSkillComposer()
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
                                                        ? "damage_block_ranged"
                                                        : "damage_block_melee"))
                                    }
                        });
        }
    }
}
