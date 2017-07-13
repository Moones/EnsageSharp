using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.Cast.Generic
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    public class UnitTarget : CastFunctionBase
    {
        public UnitTarget(IAbilitySkill skill)
            : base(skill)
        {
        }

        public override bool Cast(IAbilityUnit target)
        {
            throw new NotImplementedException();
        }

        public override bool Cast(IAbilityUnit[] targets)
        {
            throw new NotImplementedException();
        }

        public override bool Cast()
        {
            throw new NotImplementedException();
        }
    }
}
