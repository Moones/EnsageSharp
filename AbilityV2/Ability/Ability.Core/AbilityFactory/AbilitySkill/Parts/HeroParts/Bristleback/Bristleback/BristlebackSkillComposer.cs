namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Bristleback.Bristleback
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.EffectApplier;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.Bristleback.Bristleback.EffectApplier;

    using Ensage.Common.Enums;

    /// <summary>The bristleback skill composer.</summary>
    [Export(typeof(IAbilitySkillComposer))]
    [AbilitySkillMetadata((uint)AbilityId.bristleback_bristleback)]
    internal class BristlebackSkillComposer : DefaultSkillComposer
    {
        internal BristlebackSkillComposer()
        {
            this.AssignPart<IEffectApplier>(skill => new BristlebackEffectApplier(skill));
        }
    }
}
