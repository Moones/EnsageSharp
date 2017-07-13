namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Spectre.Dispersion
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.EffectApplier;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;

    using Ensage.Common.Enums;
    using Ensage.Common.Extensions;

    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata((uint)AbilityId.spectre_dispersion)]
    internal class DispersionSkillComposer : DefaultSkillComposer
    {
        internal DispersionSkillComposer()
        {
            this.AssignPart<IEffectApplier>(
                skill =>
                    new EffectApplier(skill)
                        {
                            Workers =
                                new List<IEffectApplierWorker>
                                    {
                                        new DamageReductionEffectApplierWorker(
                                            skill,
                                            true,
                                            abilitySkill =>
                                                Math.Floor(
                                                    abilitySkill.SourceAbility.GetAbilityData("damage_reflection_pct"))
                                                / 100)
                                    }
                        });
        }
    }
}
