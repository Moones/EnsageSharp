namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.WinterWyvern.ColdEmbrace
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
    [AbilitySkillMetadata((uint)AbilityId.winter_wyvern_cold_embrace)]
    internal class ColdEmbraceSkillComposer : DefaultSkillComposer
    {
        internal ColdEmbraceSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_winter_wyvern_cold_embrace",
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
                                                                            false,
                                                                            true,
                                                                            false)
                                                                    }
                                                        }),
                                            false,
                                            true,
                                            true)
                                    }
                        });
        }
    }
}
