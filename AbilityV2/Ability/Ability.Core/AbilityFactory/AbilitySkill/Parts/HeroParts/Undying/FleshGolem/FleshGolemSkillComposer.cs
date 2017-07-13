namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Undying.FleshGolem
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Heroes.Undying.FleshGolem.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;

    using Ensage.Common.Enums;

    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata((uint)AbilityId.undying_flesh_golem)]
    internal class FleshGolemSkillComposer : DefaultSkillComposer
    {
        internal FleshGolemSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_undying_flesh_golem_plague_aura",
                                            modifier =>
                                                modifier.AssignModifierEffectApplier(
                                                    new FleshGolemModifierEffectApplier(modifier)),
                                            true)
                                    }
                        });
        }
    }
}
