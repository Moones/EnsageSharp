using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.CastRange
{
    public interface ICastRange : IAbilitySkillPart
    {
        bool IsTargetInRange { get; set; }

        float Value { get; set; }
    }
}
