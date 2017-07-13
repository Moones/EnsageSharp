namespace Ability.Core.AbilityFactory.AbilityTalent.Metadata
{
    using System.ComponentModel;

    public interface IAbilityTalentMetadata
    {
        /// <summary>Gets the ability ids.</summary>
        [DefaultValue(new uint[0])]
        uint[] AbilityIds { get; }
    }
}
