using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DamageManipulation
{
    using Ability.Core.AbilityFactory.AbilityModifier;
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.Utilities;

    public interface IDamageManipulationValues
    {
        IAbilityUnit Unit { get; }

        Notifier ValueChanged { get; }

        void AddModifierValue(IAbilityModifier modifier, double value, bool willExpire = false);

        void AddSkillValue(IAbilitySkill skill, double value);

        void AddSpecialModifierValue(
            IAbilityModifier modifier,
            Func<IAbilityUnit, float, double> getValue,
            bool willExpire = false);

        void AddSpecialSkillValue(IAbilitySkill skill, Func<IAbilityUnit, float, double> getValue);

        double GetPredictedValue(IAbilityUnit source, float damageValue, float time);

        double GetValue(IAbilityUnit source, float damageValue);

        void RemoveModifierValue(IAbilityModifier modifier, double value);

        void RemoveSkillValue(IAbilitySkill skill, double value);

        void RemoveSpecialModifierValue(IAbilityModifier modifier);

        void RemoveSpecialSkillValue(IAbilitySkill skill);

        void UpdateSpecialModifierValue(IAbilityModifier modifier, Func<IAbilityUnit, float, double> newGetValue);

        void UpdateSpecialSkillValue(IAbilitySkill skill, Func<IAbilityUnit, float, double> newGetValue);

        void UpdateModifierValue(IAbilityModifier modifier, double newValue);

        void UpdateSkillValue(IAbilitySkill skill, double newValue);
    }
}
