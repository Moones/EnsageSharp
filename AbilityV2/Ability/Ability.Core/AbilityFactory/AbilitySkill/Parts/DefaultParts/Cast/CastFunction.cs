using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.Cast
{
    using Ability.Core.AbilityFactory.AbilityUnit;

    public abstract class CastFunctionBase : ICastFunction
    {
        protected CastFunctionBase(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        public void Dispose()
        {
        }

        public IAbilitySkill Skill { get; set; }

        public void Initialize()
        {
        }

        public abstract bool Cast(IAbilityUnit target);

        public abstract bool Cast();

        public abstract bool Cast(IAbilityUnit[] targets);
    }
}
