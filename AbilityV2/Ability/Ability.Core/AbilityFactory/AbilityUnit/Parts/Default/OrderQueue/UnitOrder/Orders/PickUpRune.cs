using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders
{
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.OrderPriority;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage.Common.Extensions.SharpDX;

    public class PickUpRune : UnitOrderBase
    {
        private bool runeDisposed;

        private IAbilityUnit unit;

        private IAbilityRune rune;

        public PickUpRune(IAbilityUnit unit)
            : base(OrderType.PickUpFromGround, unit)
        {
            this.unit = unit;
        }

        public void AssignRune(IAbilityRune rune)
        {
            this.runeDisposed = false;
            this.rune = rune;
            rune.RuneDisposed.Subscribe(
                () =>
                {
                    this.runeDisposed = true;
                });
        }

        private void DoIt()
        {
            this.unit.SourceUnit.PickUpRune(this.rune.SourceRune);
        }

        public override bool CanExecute()
        {
            if (this.runeDisposed)
            {
                return false;
            }

            return true;
        }

        public override float Execute()
        {
            if (this.unit.Position.PredictedByLatency.Distance(this.rune.SourceRune.Position) <= this.rune.PickUpRange)
            {
                this.DoIt();
                this.DoIt();
                this.DoIt();
                this.DoIt();
                return 100;
            }

            return 0;
        }
    }
}
