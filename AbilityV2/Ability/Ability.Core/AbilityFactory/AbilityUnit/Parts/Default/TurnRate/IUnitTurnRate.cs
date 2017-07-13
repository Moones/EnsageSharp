using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.TurnRate
{
    using SharpDX;

    public interface IUnitTurnRate : IAbilityUnitPart
    {
        float Value { get; set; }

        double GetTurnTime(Vector3 position);

        double GetTurnTime(IAbilityUnit target);
    }
}
