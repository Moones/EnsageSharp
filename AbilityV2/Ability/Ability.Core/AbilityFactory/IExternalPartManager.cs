using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts;
    using Ability.Core.AbilityFactory.AbilityUnit;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts;

    using Ensage;
    using Ensage.Common.Enums;

    using AbilityId = Ensage.AbilityId;

    /// <summary>The ExternalPartManager interface.</summary>
    public interface IExternalPartManager
    {
        /// <summary>The add unit part.</summary>
        /// <param name="unitClassId">The unit class id.</param>
        /// <param name="factory">The factory.</param>
        /// <typeparam name="T">Part type</typeparam>
        void AddUnitPart<T>(HeroId unitClassId, Func<IAbilityUnit, T> factory) where T : IAbilityUnitPart;

        /// <summary>The add unit part.</summary>
        /// <param name="factory">The factory.</param>
        /// <typeparam name="T">Part type</typeparam>
        void AddUnitPart<T>(Func<IAbilityUnit, T> factory) where T : IAbilityUnitPart;


        void AddSkillPart<T>(Func<IAbilitySkill, T> factory) where T : IAbilitySkillPart;

        void AddSkillPart<T>(Func<IAbilitySkill, T> factory, params uint[] abilityIds) where T : IAbilitySkillPart;


        void AddSkillItemPart<T>(Func<IAbilitySkill, T> factory, params uint[] abilityIds) where T : IAbilitySkillPart;
    }
}
