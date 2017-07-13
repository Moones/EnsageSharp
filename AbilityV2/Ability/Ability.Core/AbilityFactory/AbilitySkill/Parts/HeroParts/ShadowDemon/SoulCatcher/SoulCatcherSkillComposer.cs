namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.ShadowDemon.SoulCatcher
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

    /// <summary>The soul catcher skill composer.</summary>
    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata((uint)AbilityId.shadow_demon_soul_catcher)]
    internal class SoulCatcherSkillComposer : DefaultSkillComposer
    {
        internal SoulCatcherSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_shadow_demon_soul_catcher",
                                            modifier =>
                                                {
                                                    modifier.AssignModifierEffectApplier(
                                                        new ModifierEffectApplier(modifier)
                                                            {
                                                                Workers =
                                                                    new List<IEffectApplierWorker>
                                                                        {
                                                                            new DamageAmplificationEffectApplierWorker(
                                                                                modifier,
                                                                                true,
                                                                                abilityModifier =>
                                                                                    Math.Floor(abilityModifier.SourceSkill
                                                                                        .SourceAbility.GetAbilityData(
                                                                                            "bonus_damage_taken")) / 100)
                                                                        }
                                                            });
                                                },
                                            true)
                                    }
                        });
        }
    }
}
