namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.CentaurWarrunner.Stampede
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Default.ModifierEffectApplier.Workers.DamageManipulation;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata((uint)AbilityId.centaur_stampede)]
    internal class StampedeSkillComposer : DefaultSkillComposer
    {
        /// <summary>The compose.</summary>
        /// <param name="skill">The skill.</param>
        public override void Compose(IAbilitySkill skill)
        {
            base.Compose(skill);

            var skillAddedObserver = new DataObserver<IAbilitySkill>(
                add =>
                    {
                        if (add.IsItem && add.SourceItem.Id == AbilityId.item_ultimate_scepter)
                        {
                            skill.AddPart<IModifierGenerator>(
                                abilitySkill =>
                                    new ModifierGenerator(skill)
                                        {
                                            Workers =
                                                new List<ModifierGeneratorWorker>
                                                    {
                                                        new ModifierGeneratorWorker(
                                                            "modifier_centaur_stampede",
                                                            modifier =>
                                                                modifier.AssignModifierEffectApplier(
                                                                    new ModifierEffectApplier(modifier)
                                                                        {
                                                                            Workers =
                                                                                new List<IEffectApplierWorker>
                                                                                    {
                                                                                        new DamageReductionEffectApplierWorker
                                                                                        (
                                                                                            modifier,
                                                                                            false,
                                                                                            abilityModifier => 0.6)
                                                                                    }
                                                                        }),
                                                            false,
                                                            true,
                                                            true)
                                                    }
                                        });
                        }
                    });
            skillAddedObserver.Subscribe(skill.Owner.SkillBook.SkillAdded);
            skill.DisposeNotifier.Subscribe(() => skillAddedObserver.Dispose());
        }
    }
}
