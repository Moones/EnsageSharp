using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.LoneDruid.Rabid
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilitySkill.Metadata;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillCastData;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.SkillComposer;
    using Ability.Core.AbilityFactory.AbilitySkill.Parts.HeroParts.LoneDruid.Rabid.CastData;

    using Ensage.Common.Enums;

    //[Export(typeof(IAbilitySkillComposer))]
    //[AbilitySkillMetadata((uint)AbilityId.lone_druid_rabid)]
    internal class RabidSkillComposer : DefaultSkillComposer
    {
        public RabidSkillComposer()
        {
            this.AssignPart<ISkillCastData>(skill => new RabidCastData(skill));
        }
    }
}
