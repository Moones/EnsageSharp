namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Bloodseeker.Bloodrage
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityModifier.Parts.Heroes.Bloodseeker.Bloodrage.ModifierEffectApplier;
    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.ModifierGenerator;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;

    using Ensage.Common.Enums;

    /// <summary>The bloodrage skill composer.</summary>
    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata((int)AbilityId.bloodseeker_bloodrage)]
    internal class BloodrageSkillComposer : DefaultSkillComposer
    {

        private float value;

        internal BloodrageSkillComposer()
        {
            this.AssignPart<IModifierGenerator>(
                skill =>
                    new ModifierGenerator(skill)
                        {
                            Workers =
                                new List<ModifierGeneratorWorker>
                                    {
                                        new ModifierGeneratorWorker(
                                            "modifier_bloodseeker_bloodrage",
                                            modifier =>
                                                {
                                                    modifier.AssignModifierEffectApplier(
                                                        new BloodrageModifierEffectApplier(modifier));
                                                },
                                            true,
                                            true,
                                            true,
                                            true)
                                    }
                        });
        }
    }
}
