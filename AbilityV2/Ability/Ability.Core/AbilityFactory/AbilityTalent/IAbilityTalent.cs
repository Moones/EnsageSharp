namespace Ability.Core.AbilityFactory.AbilityTalent
{
    using System;

    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    public interface IAbilityTalent : IDisposable
    {
        /// <summary>Gets or sets the owner.</summary>
        IAbilityUnit Owner { get; set; }

        /// <summary>Gets or sets the source ability.</summary>
        Ability SourceAbility { get; set; }

        Notifier TalentLeveledNotifier { get; }
    }
}
