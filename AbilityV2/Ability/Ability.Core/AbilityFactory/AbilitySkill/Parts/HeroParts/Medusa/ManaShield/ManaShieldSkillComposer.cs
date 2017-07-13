namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Medusa.ManaShield
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;

    using Ensage.Common.Enums;
    using Ensage.Common.Extensions;

    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata((uint)AbilityId.medusa_mana_shield)]
    internal class ManaShieldSkillComposer : DefaultSkillComposer
    {
        internal ManaShieldSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_medusa_mana_shield",
                                            modifier =>
                                                modifier.AssignModifierEffectApplier(
                                                    new ModifierEffectApplier(modifier)
                                                        {
                                                            Workers =
                                                                new List<IEffectApplierWorker>
                                                                    {
                                                                        new ManaShieldEffectApplierWorker(
                                                                            modifier,
                                                                            true,
                                                                            abilityModifier =>
                                                                                abilityModifier.SourceSkill
                                                                                    .SourceAbility.GetAbilityData(
                                                                                        "damage_per_mana"))
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
