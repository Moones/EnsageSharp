namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Kunkka.GhostShip
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
    [AbilitySkillMetadata((uint)AbilityId.kunkka_ghostship)]
    internal class GhostShipSkillComposer : DefaultSkillComposer
    {
        internal GhostShipSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_kunkka_ghost_ship_damage_absorb",
                                            modifier =>
                                                modifier.AssignModifierEffectApplier(
                                                    new ModifierEffectApplier(modifier)
                                                        {
                                                            Workers =
                                                                new List<IEffectApplierWorker>
                                                                    {
                                                                        new DamageReductionEffectApplierWorker(
                                                                            modifier,
                                                                            true,
                                                                            abilityModifier =>
                                                                                Math.Floor(abilityModifier.SourceSkill
                                                                                    .SourceAbility.GetAbilityData(
                                                                                        "ghostship_absorb")) / 100)
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
