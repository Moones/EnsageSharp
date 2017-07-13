namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer
{
    using System;

    public interface IAbilitySkillItemComposer
    {
        void AssignPart<T>(Func<IAbilitySkill, T> factory) where T : IAbilitySkillPart;

        /// <summary>
        ///     The compose.
        /// </summary>
        /// <param name="skill">
        ///     The skill.
        /// </param>
        void Compose(IAbilitySkill skill);
    }
}
