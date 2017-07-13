using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.TargetSelector
{
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Position;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common.Extensions;

    public class UnitTargetSelector : IUnitTargetSelector
    {
        private IAbilityUnit target;

        public UnitTargetSelector(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        public Notifier TargetDistanceChanged { get; } = new Notifier();

        public float LastDistanceToTarget { get; set; }

        public void Initialize()
        {
            this.Unit.Position.Subscribe(
                new DataObserver<IPosition>(
                    position =>
                        {
                            if (this.TargetIsSet)
                            {
                                this.UpdateDistance();
                            }
                        }));
        }

        private int lastZeroHealthId;

        private IDisposable positionUnsubscriber;

        public bool TargetIsSet { get; set; }

        public IAbilityUnit Target
        {
            get
            {
                return this.target;
            }

            set
            {
                this.target?.Health.ZeroHealth.Unsubscribe(this.lastZeroHealthId);
                this.positionUnsubscriber?.Dispose();
                this.target = value;
                if (this.target != null)
                {
                    this.TargetIsSet = true;
                    this.lastZeroHealthId = this.target.Health.ZeroHealth.Subscribe(this.TargetDied);
                    this.positionUnsubscriber = this.Target.Position.Subscribe(
                        new DataObserver<IPosition>(
                            position =>
                                {
                                    this.UpdateDistance();
                                }));
                }
                else
                {
                    this.TargetIsSet = false;
                }

                this.TargetChanged.Notify();
            }
        }

        public void ResetTarget()
        {
            this.target?.Health.ZeroHealth.Unsubscribe(this.lastZeroHealthId);
            this.positionUnsubscriber?.Dispose();
        }

        public Notifier TargetChanged { get; } = new Notifier();

        public virtual IAbilityUnit GetTarget()
        {
            var mouseDistance = 9999999f;
            var mousePosition = Game.MousePosition;
            IAbilityUnit result = null;
            foreach (var teamOtherTeam in this.Unit.Team.OtherTeams)
            {
                foreach (var unitManagerUnit in teamOtherTeam.UnitManager.Units)
                {
                    if (!unitManagerUnit.Value.SourceUnit.IsAlive || !unitManagerUnit.Value.Visibility.Visible
                        || unitManagerUnit.Value.Health.Current <= 0)
                    {
                        continue;
                    }

                    var distance = unitManagerUnit.Value.Position.Current.Distance2D(mousePosition);
                    if (distance < mouseDistance)
                    {
                        mouseDistance = distance;
                        result = unitManagerUnit.Value;
                    }
                }
            }

            if (result != null)
            {
                Console.WriteLine("setting target " + result.Name);
            }

            this.Target = result;
            return result;
        }

        public IAbilityUnit[] GetTargets()
        {
            return new IAbilityUnit[0];
        }

        private void TargetDied()
        {
            this.GetTarget();
        }

        private void UpdateDistance()
        {
            if (!this.TargetIsSet || this.Target == null)
            {
                return;
            }

            this.LastDistanceToTarget =
                this.Unit.Position.PredictedByLatency.Distance2D(this.Target.Position.PredictedByLatency);
            this.TargetDistanceChanged.Notify();
        }
    }
}
