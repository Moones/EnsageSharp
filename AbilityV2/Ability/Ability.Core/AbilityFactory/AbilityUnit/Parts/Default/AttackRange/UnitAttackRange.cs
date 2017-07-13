using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.AttackRange
{
    using Ensage;
    using Ensage.Common.Extensions;

    public class UnitAttackRange : IUnitAttackRange
    {
        public UnitAttackRange(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        public virtual void Initialize()
        {
            this.Value = this.Unit.SourceUnit.AttackRange;
        }

        public float Value { get; set; }

        public bool IsInAttackRange(IAbilityUnit target)
        {
            return
                target.Position.Predict(
                        (float)(Game.Ping + this.Unit.SourceUnit.GetTurnTime(target.SourceUnit) * 1000f))
                    .Distance2D(this.Unit.Position.Current)
                <= this.Value + target.SourceUnit.HullRadius + this.Unit.SourceUnit.HullRadius;
        }
    }
}
