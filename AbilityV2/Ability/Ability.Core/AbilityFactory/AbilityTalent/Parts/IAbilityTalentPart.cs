namespace Ability.Core.AbilityFactory.AbilityTalent.Parts
{
    using System;

    public interface IAbilityTalentPart : IDisposable
    {
        IAbilityTalent Talent { get; set; }

        void Initialize();
    }
}
