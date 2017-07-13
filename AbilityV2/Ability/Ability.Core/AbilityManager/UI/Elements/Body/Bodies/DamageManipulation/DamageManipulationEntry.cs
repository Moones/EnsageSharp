namespace Ability.Core.AbilityManager.UI.Elements.Body.Bodies.DamageManipulation
{
    using System;
    using System.Collections.Generic;

    using Ability.Core.AbilityFactory.AbilityUnit;

    using Ensage;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    internal class DamageManipulationEntry : DrawObject
    {
        private IAbilityUnit unit;

        private ICollection<DamageManipulationValueEntry> values = new List<DamageManipulationValueEntry>();

        private Vector2 size;

        private Vector2 position;

        private DrawText unitNameText;

        internal DamageManipulationEntry(IAbilityUnit unit)
        {
            this.unit = unit;
            //if (this.unit == null)
            this.values.Add(
                new DamageManipulationValueEntry(
                    "DamageAmpli",
                    () => unit.DamageManipulation.DamageAmplification.GetValue(unit, unit.Health.Current).ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "DamageReduce",
                    () => unit.DamageManipulation.DamageReduction.GetValue(unit, unit.Health.Current).ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "DamageNegate",
                    () => unit.DamageManipulation.DamageNegation.GetValue(unit, unit.Health.Current).ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "DamageBlock",
                    () => unit.DamageManipulation.DamageBlock.ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "MagDmgAbsorb",
                    () => unit.DamageManipulation.MagicalDamageAbsorb.GetValue(unit, unit.Health.Current).ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "ManaShield",
                    () => unit.DamageManipulation.ManaShield.GetValue(unit, unit.Health.Current).ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "IceBlast",
                    () => unit.DamageManipulation.Aa?.GetSpecialValue(unit, unit.Health.Current).ToString() ?? 0.ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "MagicDmgShield",
                    () => unit.DamageManipulation.MagicalDamageShield.ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "PureDamageShield",
                    () => unit.DamageManipulation.PureDamageShield.ToString()));
            this.values.Add(
                new DamageManipulationValueEntry(
                    "PhysicalDamageShield",
                    () => unit.DamageManipulation.PhysicalDamageShield.ToString()));

            this.unitNameText = new DrawText { Color = Color.GreenYellow, Shadow = true, Text = Game.Localize(unit.Name) };
        }

        public override Vector2 Size
        {
            get
            {
                return this.size;
            }

            set
            {
                var textSize = value / 5;
                var entrySize = new Vector2(textSize.X / this.values.Count, textSize.Y / this.values.Count);
                this.size = new Vector2(0);
                foreach (var damageManipulationValueEntry in this.values)
                {
                    damageManipulationValueEntry.Size = entrySize * (float)0.9;
                    this.size = new Vector2(
                        Math.Max(this.size.X, damageManipulationValueEntry.Size.X),
                        this.size.Y + damageManipulationValueEntry.Size.Y);
                }

                this.unitNameText.TextSize = entrySize;
                this.size += new Vector2(0, this.unitNameText.Size.Y);
            }
        }

        public override Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
                var basePos = this.position;
                this.unitNameText.Position = basePos;
                basePos += new Vector2(0, this.unitNameText.Size.Y);
                foreach (var damageManipulationValueEntry in this.values)
                {
                    damageManipulationValueEntry.Position = basePos;
                    basePos += new Vector2(0, damageManipulationValueEntry.Size.Y);
                }
            }
        }

        public override void Draw()
        {
            this.unitNameText.Draw();
            foreach (var damageManipulationValueEntry in this.values)
            {
                damageManipulationValueEntry.Draw();
            }
        }
    }
}
