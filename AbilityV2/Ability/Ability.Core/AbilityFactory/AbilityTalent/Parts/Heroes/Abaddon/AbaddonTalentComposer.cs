namespace Ability.Core.AbilityFactory.AbilityTalent.Parts.Heroes.Abaddon
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityTalent.Metadata;
    using Ability.Core.AbilityFactory.AbilityTalent.Parts.Default;

    using Ensage.Common.Enums;

    [Export(typeof(IAbilityTalentComposer))]
    [AbilityTalentMetadata((uint)AbilityId.special_bonus_unique_abaddon)]
    internal class AbaddonTalentComposer : AbilityTalentComposer
    {
        public override void Compose(IAbilityTalent modifier)
        {

        }
    }
}
