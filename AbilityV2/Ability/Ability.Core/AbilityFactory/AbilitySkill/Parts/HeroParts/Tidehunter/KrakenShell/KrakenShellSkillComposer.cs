namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Tidehunter.KrakenShell
{
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
    [AbilitySkillMetadata((uint)AbilityId.tidehunter_kraken_shell)]
    internal class KrakenShellSkillComposer : DefaultSkillComposer
    {
        internal KrakenShellSkillComposer()
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
                                            true,
                                            abilitySkill =>
                                                abilitySkill.SourceAbility.GetAbilityData("damage_reduction"))
                                    }
                        });
        }
    }
}
