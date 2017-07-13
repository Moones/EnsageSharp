using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.Cast
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    public interface ICastFunction : IAbilitySkillPart
    {
        bool Cast(IAbilityUnit target);
        bool Cast();
        bool Cast(IAbilityUnit[] targets);
    }
}
