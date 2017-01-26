using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.Metadata
{
    using Ensage;

    /// <summary>The AbilityUnitMetadata interface.</summary>
    public interface IAbilityUnitMetadata
    {
        /// <summary>Gets the class ids.</summary>
        ICollection<ClassID> ClassIds { get; }
    }
}
