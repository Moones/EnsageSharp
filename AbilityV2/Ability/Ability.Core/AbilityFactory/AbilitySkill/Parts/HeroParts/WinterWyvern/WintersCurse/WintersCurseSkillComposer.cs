namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.WinterWyvern.WintersCurse
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
    [AbilitySkillMetadata((uint)AbilityId.winter_wyvern_winters_curse)]
    internal class WintersCurseSkillComposer : DefaultSkillComposer
    {
        internal WintersCurseSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_winter_wyvern_winters_curse_aura",
                                            modifier =>
                                                {
                                                    modifier.AssignModifierEffectApplier(
                                                        new ModifierEffectApplier(modifier)
                                                            {
                                                                Workers =
                                                                    new List<IEffectApplierWorker>
                                                                        {
                                                                            new DamageReductionEffectApplierWorker(
                                                                                modifier,
                                                                                false,
                                                                                abilityModifier => 1)
                                                                        }
                                                            });
                                                },
                                            true)
                                    }
                        });
        }
    }
}
