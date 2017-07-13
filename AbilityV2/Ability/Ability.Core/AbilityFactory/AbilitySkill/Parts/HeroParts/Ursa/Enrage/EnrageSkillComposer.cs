namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Ursa.Enrage
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
    [AbilitySkillMetadata((uint)AbilityId.ursa_enrage)]
    internal class EnrageSkillComposer : DefaultSkillComposer
    {
        internal EnrageSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_ursa_enrage",
                                            modifier =>
                                                modifier.AssignModifierEffectApplier(
                                                    new ModifierEffectApplier(modifier)
                                                        {
                                                            Workers =
                                                                new List<IEffectApplierWorker>
                                                                    {
                                                                        new DamageReductionEffectApplierWorker(
                                                                            modifier,
                                                                            false,
                                                                            abilityModifier =>
                                                                                Math.Floor(abilityModifier.SourceSkill
                                                                                    .SourceAbility.GetAbilityData(
                                                                                        "damage_reduction")) / 100)
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
