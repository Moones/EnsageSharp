namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.TurnRate
{
    using System;

    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    using SharpDX;

    public class UnitTurnRate : IUnitTurnRate
    {
        private float value;

        public UnitTurnRate(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        private float turnRate;

        public void Initialize()
        {
            try
            {
                this.turnRate =
                    Game.FindKeyValues(
                        $"{this.Unit.Name}/MovementTurnRate",
                        this.Unit.IsHero ? KeyValueSource.Hero : KeyValueSource.Unit).FloatValue;
            }
            catch (KeyValuesNotFoundException)
            {
                this.turnRate = 0.5f;
            }

            this.UpdateValue();

            this.Unit.Modifiers.ModifierAdded.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                        {
                            if (modifier.Name == "modifier_medusa_stone_gaze_slow")
                            {
                                this.MedusaStoneGaze = true;
                                this.UpdateValue();
                            }
                            else if (modifier.Name == "modifier_batrider_sticky_napalm")
                            {
                                this.BatriderStickNapalm = true;
                                this.UpdateValue();
                            }
                        }));

            this.Unit.Modifiers.ModifierRemoved.Subscribe(
                new DataObserver<Modifier>(
                    modifier =>
                    {
                        if (modifier.Name == "modifier_medusa_stone_gaze_slow")
                        {
                            this.MedusaStoneGaze = false;
                            this.UpdateValue();
                        }
                        else if (modifier.Name == "modifier_batrider_sticky_napalm")
                        {
                            this.BatriderStickNapalm = false;
                            this.UpdateValue();
                        }
                    }));
        }

        public bool MedusaStoneGaze { get; set; }

        public bool BatriderStickNapalm { get; set; }

        public float Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }

        private void UpdateValue()
        {
            var tempValue = this.turnRate;

            if (this.MedusaStoneGaze)
            {
                tempValue *= 0.65f;
            }

            if (this.BatriderStickNapalm)
            {
                tempValue *= 0.3f;
            }

            this.Value = tempValue;
        }

        public double GetTurnTime(Vector3 position)
        {
            var num =
                Math.Abs(
                    Math.Atan2(
                        position.Y - this.Unit.Position.PredictedByLatency.Y,
                        position.X - this.Unit.Position.PredictedByLatency.X) - this.Unit.SourceUnit.RotationRad);
            if (num > Math.PI)
            {
                num = (2.0 * Math.PI) - num;
            }

            var angle = (float)num;
            if (this.Unit.SourceUnit.ClassId == ClassId.CDOTA_Unit_Hero_Wisp)
            {
                return 0;
            }

            if (angle <= 0.5f)
            {
                return 0;
            }

            return (0.03f / this.Value) * angle;
        }

        public double GetTurnTime(IAbilityUnit target)
        {
            return this.GetTurnTime(target.Position.PredictedByLatency);
        }
    }
}
