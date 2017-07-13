using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.TrollWarlord.SkillBook
{
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.SkillBook;

    using Ensage;

    public class TrollWarlordSkillBook : SkillBook<IAbilitySkill>
    {
        public TrollWarlordSkillBook(IAbilityUnit unit)
            : base(unit)
        {
        }


        public IAbilitySkill BattleTrance { get; set; }

        public IAbilitySkill BerserkersRage { get; set; }

    }
}
