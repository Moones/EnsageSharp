using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DamageManipulation
{
    public interface IValueHolder<T>
    {
        bool WillExpire { get; }

        Func<float> ExpireTime { get; }

        Func<IAbilityUnit, float, T> GetSpecialValue { get; set; }

        T Value { get; set; }
    }
}
