namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.AncientApparition.IceBlast
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Heroes.AncientApparition.IceBlast.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;

    using Ensage.Common.Enums;

    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata((uint)AbilityId.ancient_apparition_ice_blast)]
    internal class IceBlastSkillComposer : DefaultSkillComposer
    {
        internal IceBlastSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_ice_blast",
                                            modifier =>
                                                modifier.AssignModifierEffectApplier(
                                                    new IceBlastModifierEffectApplier(modifier)),
                                            true)
                                    }
                        });
        }
    }
}
