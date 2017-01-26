using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Weaver
{
    using System.ComponentModel.Composition;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Composer;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.PositionTracker.Types;
    using Ability.Core.AbilityFactory.Metadata;

    using Ensage;

    /// <summary>The weaver unit composer.</summary>
    [Export(typeof(IAbilityUnitComposer))]
    [AbilityUnitMetadata(ClassID.CDOTA_Unit_Hero_Weaver)]
    internal class WeaverUnitComposer : AbilityUnitComposer
    {
        /// <summary>Initializes a new instance of the <see cref="WeaverUnitComposer"/> class.</summary>
        internal WeaverUnitComposer()
        {
            this.AssignPart<IPositionTracker>(unit => new DotaBasePositionTracker(unit, 100));
        }
    }
}
