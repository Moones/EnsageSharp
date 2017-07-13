namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Slardar.Sprint
{
    using System;
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
    [AbilitySkillMetadata((uint)AbilityId.slardar_sprint)]
    internal class SprintSkillComposer : DefaultSkillComposer
    {
        internal SprintSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_slardar_sprint",
                                            modifier =>
                                                modifier.AssignModifierEffectApplier(
                                                    new ModifierEffectApplier(modifier)
                                                        {
                                                            Workers =
                                                                new List<IEffectApplierWorker>
                                                                    {
                                                                        new DamageAmplificationEffectApplierWorker(
                                                                            modifier,
                                                                            false,
                                                                            abilityModifier =>
                                                                                Math.Floor(abilityModifier.SourceSkill
                                                                                    .SourceAbility.GetAbilityData(
                                                                                        "bonus_damage",
                                                                                        1)) / 100)
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
