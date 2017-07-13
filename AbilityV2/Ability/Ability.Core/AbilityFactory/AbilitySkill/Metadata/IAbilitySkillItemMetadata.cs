namespace Ability.Core.AbilityFactory.AbilitySkill.Metadata
{
    using System.ComponentModel;

    using Ensage;

    public interface IAbilitySkillItemMetadata
    {
        /// <summary>Gets the skill ids.</summary>
        [DefaultValue(new uint[0])]
        uint[] AbilityIds { get; }

        /// <summary>Gets a value indicating whether owner.</summary>
        bool Owner { get; }

        /// <summary>Gets the owner class id.</summary>
        ClassId OwnerClassId { get; }
    }
}
