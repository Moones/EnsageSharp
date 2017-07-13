namespace Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    public interface IAbilityRune : IDisposable
    {
        Rune SourceRune { get; }

        string Name { get; }

        string TypeName { get; }

        double Handle { get; }

        float PickUpRange { get; }

        Notifier RuneDisposed { get; }
    }
}
