using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Composer
{
    public interface IAbilityUnitComposer
    {
        void Compose(IAbilityUnit unit);
    }
}
