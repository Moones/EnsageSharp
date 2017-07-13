using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderIssuer.Custom
{
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Runes.AbilityRune.Types;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.OrderQueue.UnitOrder.Orders;
    using Ability.Core.AbilityFactory.Utilities;

    using PlaySharp.Toolkit.Injector;

    public class RunePicker : IOrderIssuer
    {

        private DataObserver<BountyRune> newBountyRuneObserver = new DataObserver<BountyRune>();

        private DataObserver<PowerUpRune> newPowerUpRuneObserver = new DataObserver<PowerUpRune>();

        private PickUpRune pickUpRune;

        public RunePicker(IAbilityUnit unit)
        {
            this.Unit = unit;
        }


        public void Dispose()
        {
        }

        public void AutomaticRunePicking(bool enable)
        {
            if (enable)
            {
                this.pickUpRune = new PickUpRune(this.Unit);
                this.newBountyRuneObserver = new DataObserver<BountyRune>(
                    rune =>
                    {
                        var distance =
                        Ensage.Common.Extensions.VectorExtensions.Distance(
                            this.Unit.Position.PredictedByLatency,
                            rune.SourceRune.Position);
                        if (distance < 1000)
                        {
                            this.pickUpRune.AssignRune(rune);
                            //this.Unit.OrderQueue.EnqueueOrder(this.pickUpRune);
                        }
                    });

                this.newPowerUpRuneObserver = new DataObserver<PowerUpRune>(
                    rune =>
                    {
                        var distance =
                        Ensage.Common.Extensions.VectorExtensions.Distance(
                            this.Unit.Position.PredictedByLatency,
                            rune.SourceRune.Position);
                        if (distance < 1000)
                        {
                            this.pickUpRune.AssignRune(rune);
                            //this.Unit.OrderQueue.EnqueueOrder(this.pickUpRune);
                        }
                    });
                //this.newPowerUpRuneObserver.Subscribe(this.MapData.PowerUpRuneSpawner.NewRuneProvider);
                //Console.WriteLine(this.MapData);
                //this.newBountyRuneObserver.Subscribe(this.MapData.BountyRuneSpawner.NewRuneProvider);
            }
        }

        public IAbilityUnit Unit { get; set; }

        public void Initialize()
        {
        }

        public bool Enabled { get; set; }

        public uint Id { get; set; }

        public bool Issue()
        {
            throw new NotImplementedException();
        }

        public bool PreciseIssue()
        {
            throw new NotImplementedException();
        }
    }
}
