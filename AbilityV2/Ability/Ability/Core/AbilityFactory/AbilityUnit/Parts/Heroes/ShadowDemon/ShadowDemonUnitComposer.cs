using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.ShadowDemon
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker.Types;
    using Ability.Core.AbilityFactory.Metadata;

    using Ensage;

    /// <summary>The shadow demon unit composer.</summary>
    [Export(typeof(IAbilityUnitComposer))]
    [AbilityUnitMetadata(ClassID.CDOTA_Unit_Hero_Shadow_Demon)]
    internal class ShadowDemonUnitComposer : AbilityUnitComposer
    {
        /// <summary>Initializes a new instance of the <see cref="ShadowDemonUnitComposer"/> class.</summary>
        public ShadowDemonUnitComposer()
        {
            this.AssignPart<IPositionTracker>(unit => new DotaBasePositionTracker(unit, 400));
        }
    }
}
