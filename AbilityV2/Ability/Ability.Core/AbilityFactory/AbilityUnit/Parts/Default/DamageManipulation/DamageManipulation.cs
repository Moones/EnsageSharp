// <copyright file="DamageManipulation.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DamageManipulation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Modifiers;
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    internal class DamageManipulation : IDamageManipulation
    {
        #region Fields

        private IValueHolder<float> aa;

        private bool iceblasted;

        private bool isAttackImmune;

        private bool isInvul;

        private bool isMagicImmune;

        private ICollection<double> magicalDamageBlocks = new List<double>();

        private DataObserver<Modifier> modifierAddObserver;

        private DataObserver<Modifier> modifierRemoveObserver;

        private ICollection<double> physicalDamageBlocks = new List<double>();

        private ICollection<double> pureDamageBlocks = new List<double>();

        private float reduceBlock;

        private float reduceStatic;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="DamageManipulation" /> class.</summary>
        /// <param name="unit">The unit.</param>
        internal DamageManipulation(IAbilityUnit unit)
        {
            this.Unit = unit;
            Action reacter = () =>
                {
                    this.MagicalDamageReductionChanged.Notify();
                    this.PhysicalDamageReductionChanged.Notify();
                    this.PureDamageReductionChanged.Notify();
                };
            this.DamageNegation = new DamageManipulationValues(unit);
            this.DamageNegation.ValueChanged.Subscribe(reacter);

            this.DamageAmplification = new DamageManipulationValues(unit);
            this.DamageAmplification.ValueChanged.Subscribe(reacter);

            this.DamageReduction = new DamageManipulationValues(unit);
            this.DamageReduction.ValueChanged.Subscribe(reacter);

            this.MagicalDamageAbsorb = new DamageManipulationValues(unit);
            this.MagicalDamageAbsorb.ValueChanged.Subscribe(() => this.MagicalDamageReductionChanged.Notify());

            this.AmpFromMe = new DamageManipulationValues(unit);

            this.ManaShield = new DamageManipulationValues(unit);
            this.ManaShield.ValueChanged.Subscribe(reacter);
        }

        #endregion

        #region Public Properties

        public IValueHolder<float> Aa
        {
            get
            {
                return this.aa;
            }

            set
            {
                this.aa = value;
                this.iceblasted = this.aa != null;
                this.MagicalDamageReductionChanged.Notify();
                this.PhysicalDamageReductionChanged.Notify();
                this.PureDamageReductionChanged.Notify();
            }
        }

        public Dictionary<double, Tuple<float, float>> AaMinusEvents { get; }

        public Dictionary<double, Tuple<float, float>> AaPlusEvents { get; }

        public IDamageManipulationValues AmpFromMe { get; set; }

        public Notifier BecameInvulnerableNotifier { get; } = new Notifier();

        public Notifier BecameMagicImmuneNotifier { get; } = new Notifier();

        public IDamageManipulationValues DamageAmplification { get; set; }

        public float DamageBlock
        {
            get
            {
                return this.reduceBlock;
            }

            set
            {
                this.reduceBlock = value;
                this.MagicalDamageReductionChanged.Notify();
                this.PhysicalDamageReductionChanged.Notify();
                this.PureDamageReductionChanged.Notify();
            }
        }

        public Dictionary<double, float> DamageBlocks { get; } = new Dictionary<double, float>();

        public IDamageManipulationValues DamageNegation { get; }

        public IDamageManipulationValues DamageReduction { get; set; }

        public bool IsAttackImmune
        {
            get
            {
                return this.isAttackImmune;
            }

            set
            {
                this.isAttackImmune = value;
                this.PhysicalDamageReductionChanged.Notify();
            }
        }

        public bool IsInvul
        {
            get
            {
                return this.isInvul;
            }

            set
            {
                this.isInvul = value;
                this.MagicalDamageReductionChanged.Notify();
                this.PhysicalDamageReductionChanged.Notify();
                this.PureDamageReductionChanged.Notify();
                this.BecameInvulnerableNotifier.Notify();
            }
        }

        public bool IsMagicImmune
        {
            get
            {
                return this.isMagicImmune;
            }

            set
            {
                this.isMagicImmune = value;
                this.BecameMagicImmuneNotifier.Notify();
            }
        }

        public IDamageManipulationValues MagicalDamageAbsorb { get; set; }

        public Notifier MagicalDamageReductionChanged { get; } = new Notifier();

        public bool MagicalDamageShield { get; private set; }

        public IDamageManipulationValues ManaShield { get; set; }

        public Notifier PhysicalDamageReductionChanged { get; } = new Notifier();

        public bool PhysicalDamageShield { get; private set; }

        public Notifier PureDamageReductionChanged { get; } = new Notifier();

        public bool PureDamageShield { get; private set; }

        /// <summary>Gets or sets the reduce other.</summary>
        public double ReduceOther { get; set; }

        public Dictionary<double, Tuple<float, double>> ReduceOtherMinusEvents { get; }

        public Dictionary<double, Tuple<float, double>> ReduceOtherPlusEvents { get; }

        public float ReduceStatic
        {
            get
            {
                return this.reduceStatic;
            }

            set
            {
                this.reduceStatic = value;
                this.MagicalDamageReductionChanged.Notify();
                this.PhysicalDamageReductionChanged.Notify();
                this.PureDamageReductionChanged.Notify();
            }
        }

        public Dictionary<double, Tuple<float, float>> ReduceStaticMinusEvents { get; }

        public Dictionary<double, Tuple<float, float>> ReduceStaticPlusEvents { get; }

        public IAbilityUnit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        public void AddDamageBlock(double handle, float value)
        {
            this.DamageBlocks.Add(handle, value);
            Console.WriteLine("added damage block " + value);
            this.DamageBlock = this.DamageBlocks.MaxOrDefault(x => x.Value).Value;
        }

        public void AddDamageShield(double handle, bool magical, bool physical, bool pure)
        {
            if (magical)
            {
                this.magicalDamageBlocks.Add(handle);
                this.MagicalDamageShield = true;
            }

            if (physical)
            {
                this.physicalDamageBlocks.Add(handle);
                this.PhysicalDamageShield = true;
            }

            if (pure)
            {
                this.pureDamageBlocks.Add(handle);
                this.PureDamageShield = true;
            }
        }

        public void Dispose()
        {
            this.modifierAddObserver.Dispose();
            this.modifierRemoveObserver.Dispose();
        }

        public virtual void Initialize()
        {
            this.modifierAddObserver = new DataObserver<Modifier>(modifier => this.ModifierAdded(modifier));
            this.modifierAddObserver.Subscribe(this.Unit.Modifiers.ModifierAdded);

            this.modifierRemoveObserver =
                new DataObserver<Modifier>(modifier => this.ModifierRemoved(modifier));
            this.modifierRemoveObserver.Subscribe(this.Unit.Modifiers.ModifierRemoved);

            foreach (var sourceUnitModifier in this.Unit.SourceUnit.Modifiers)
            {
                this.ModifierAdded(sourceUnitModifier);
            }
        }

        public float ManipulateIncomingAutoAttackDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusArmor,
            float time)
        {
            if (this.IsAttackImmune || this.PhysicalDamageShield)
            {
                return 0;
            }

            return
                (float)
                Math.Max(
                    0,
                    (damageValue * (1 - this.ManaShield.GetValue(source, damageValue)) * (1 - this.ReduceOther)
                     - this.DamageBlock - this.DamageNegation.GetPredictedValue(source, damageValue, time))
                    * (1 + this.DamageAmplification.GetPredictedValue(source, damageValue, time)
                       - this.DamageReduction.GetPredictedValue(source, damageValue, time)) * (1 + damageAmplification)
                    * (1 - this.Unit.SourceUnit.DamageResist) + 0.06 * minusArmor / (1 + 0.06 * Math.Abs(minusArmor))
                    - this.ReduceStatic + (this.iceblasted ? this.Aa.GetSpecialValue(source, damageValue) : 0));
        }

        public float ManipulateIncomingAutoAttackDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusArmor)
        {
            if (this.IsAttackImmune || this.PhysicalDamageShield)
            {
                return 0;
            }

            return
                (float)
                Math.Max(
                    0,
                    (damageValue * (1 - this.ManaShield.GetValue(source, damageValue)) * (1 - this.ReduceOther)
                     - this.DamageBlock - this.DamageNegation.GetValue(source, damageValue))
                    * (1 + this.DamageAmplification.GetValue(source, damageValue)
                       - this.DamageReduction.GetValue(source, damageValue)) * (1 + damageAmplification)
                    * (1 - this.Unit.SourceUnit.DamageResist) + 0.06 * minusArmor / (1 + 0.06 * Math.Abs(minusArmor))
                    - this.ReduceStatic + (this.iceblasted ? this.Aa.GetSpecialValue(source, damageValue) : 0));
        }

        /// <summary>The reduce magical damage.</summary>
        /// <param name="damageValue">The damage value.</param>
        /// <param name="damageAmplification">The damage amplification.</param>
        /// <param name="minusMagicResistancePerc">The minus magic resistance perc.</param>
        /// <returns>The <see cref="float" />.</returns>
        public virtual float ManipulateIncomingMagicalDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusMagicResistancePerc)
        {
            if (this.MagicalDamageShield)
            {
                return 0;
            }

            var resist = 1 - (1 - this.Unit.SourceUnit.MagicDamageResist) * (1 + minusMagicResistancePerc / 100);
            return
                (float)
                Math.Max(
                    0,
                    (damageValue * (1 - this.ManaShield.GetValue(source, damageValue)) * (1 - this.ReduceOther)
                     - this.MagicalDamageAbsorb.GetValue(source, damageValue)
                     - this.DamageNegation.GetValue(source, damageValue))
                    * (1 + this.DamageAmplification.GetValue(source, damageValue)
                       - this.DamageReduction.GetValue(source, damageValue)) * (1 + damageAmplification) * (1 - resist)
                    - this.ReduceStatic + (this.iceblasted ? this.Aa.GetSpecialValue(source, damageValue) : 0));
        }

        public virtual float ManipulateIncomingMagicalDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusMagicResistancePerc,
            float time)
        {
            if (this.MagicalDamageShield)
            {
                return 0;
            }

            var resist = 1 - (1 - this.Unit.SourceUnit.MagicDamageResist) * (1 + minusMagicResistancePerc / 100);
            return
                (float)
                Math.Max(
                    0,
                    (damageValue * (1 - this.ManaShield.GetValue(source, damageValue)) * (1 - this.ReduceOther)
                     - this.MagicalDamageAbsorb.GetPredictedValue(source, damageValue, time)
                     - this.DamageNegation.GetPredictedValue(source, damageValue, time))
                    * (1 + this.DamageAmplification.GetPredictedValue(source, damageValue, time)
                       - this.DamageReduction.GetPredictedValue(source, damageValue, time)) * (1 + damageAmplification)
                    * (1 - resist) - this.ReduceStatic
                    + (this.iceblasted ? this.Aa.GetSpecialValue(source, damageValue) : 0));
        }

        /// <summary>The reduce physical damage.</summary>
        /// <param name="damageValue">The damage value.</param>
        /// <param name="damageAmplification">The damage amplification.</param>
        /// <param name="minusDamageResistancePerc">The minus damage resistance perc.</param>
        /// <param name="minusArmor">The minus armor.</param>
        /// <returns>The <see cref="float" />.</returns>
        public float ManipulateIncomingPhysicalDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusDamageResistancePerc,
            float minusArmor)
        {
            if (this.IsAttackImmune || this.PhysicalDamageShield)
            {
                return 0;
            }

            return
                (float)
                Math.Max(
                    0,
                    (damageValue * (1 - this.ManaShield.GetValue(source, damageValue)) * (1 - this.ReduceOther)
                     - this.DamageNegation.GetValue(source, damageValue))
                    * (1 + this.DamageAmplification.GetValue(source, damageValue)
                       - this.DamageReduction.GetValue(source, damageValue)) * (1 + damageAmplification)
                    * (1 - this.Unit.SourceUnit.DamageResist * (1 - minusDamageResistancePerc / 100))
                    + 0.06 * minusArmor / (1 + 0.06 * Math.Abs(minusArmor)) - this.ReduceStatic
                    + (this.iceblasted ? this.Aa.GetSpecialValue(source, damageValue) : 0));
        }

        public float ManipulateIncomingPhysicalDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float minusDamageResistancePerc,
            float minusArmor,
            float time)
        {
            if (this.IsAttackImmune || this.PhysicalDamageShield)
            {
                return 0;
            }

            return
                (float)
                Math.Max(
                    0,
                    (damageValue * (1 - this.ManaShield.GetValue(source, damageValue)) * (1 - this.ReduceOther)
                     - this.DamageNegation.GetPredictedValue(source, damageValue, time))
                    * (1 + this.DamageAmplification.GetPredictedValue(source, damageValue, time)
                       - this.DamageReduction.GetPredictedValue(source, damageValue, time)) * (1 + damageAmplification)
                    * (1 - this.Unit.SourceUnit.DamageResist * (1 - minusDamageResistancePerc / 100))
                    + 0.06 * minusArmor / (1 + 0.06 * Math.Abs(minusArmor)) - this.ReduceStatic
                    + (this.iceblasted ? this.Aa.GetSpecialValue(source, damageValue) : 0));
        }

        public float ManipulateIncomingPureDamage(IAbilityUnit source, float damageValue, double damageAmplification)
        {
            if (this.PureDamageShield)
            {
                return 0;
            }

            return
                (float)
                Math.Max(
                    0,
                    (damageValue * (1 - this.ManaShield.GetValue(source, damageValue)) * (1 - this.ReduceOther)
                     - this.DamageNegation.GetValue(source, damageValue))
                    * (1 + this.DamageAmplification.GetValue(source, damageValue)
                       - this.DamageReduction.GetValue(source, damageValue)) * (1 + damageAmplification)
                    - this.reduceStatic + (this.iceblasted ? this.Aa.GetSpecialValue(source, damageValue) : 0));
        }

        public float ManipulateIncomingPureDamage(
            IAbilityUnit source,
            float damageValue,
            double damageAmplification,
            float time)
        {
            if (this.PureDamageShield)
            {
                return 0;
            }

            var tempDamage =
                (float)
                Math.Max(
                    0,
                    (damageValue * (1 - this.ManaShield.GetValue(source, damageValue)) * (1 - this.ReduceOther)
                     - this.DamageNegation.GetPredictedValue(source, damageValue, time))
                    * (1 + this.DamageAmplification.GetPredictedValue(source, damageValue, time)
                       - this.DamageReduction.GetPredictedValue(source, damageValue, time)) * (1 + damageAmplification)
                    - this.reduceStatic);
            return tempDamage + (this.iceblasted ? this.Aa.GetSpecialValue(source, tempDamage) : 0);
        }

        public virtual void ModifierAdded(Modifier modifier)
        {
            this.IsMagicImmune = this.Unit.SourceUnit.IsMagicImmune();
            this.IsAttackImmune = this.Unit.SourceUnit.IsAttackImmune();
            this.IsInvul = this.Unit.SourceUnit.IsInvul();
        }

        public virtual void ModifierRemoved(Modifier modifier)
        {
            DelayAction.Add(
                1,
                () =>
                    {
                        this.IsMagicImmune = this.Unit.SourceUnit.IsMagicImmune();
                        this.IsAttackImmune = this.Unit.SourceUnit.IsAttackImmune();
                        this.IsInvul = this.Unit.SourceUnit.IsInvul();
                    });
        }

        public void RemoveDamageBlock(double handle)
        {
            this.DamageBlocks.Remove(handle);
            this.DamageBlock = this.DamageBlocks.MaxOrDefault(x => x.Value).Value;
        }

        public void RemoveDamageShield(double handle, bool magical, bool physical, bool pure)
        {
            if (magical)
            {
                this.magicalDamageBlocks.Remove(handle);
                this.MagicalDamageShield = this.magicalDamageBlocks.Any();
            }

            if (physical)
            {
                this.physicalDamageBlocks.Remove(handle);
                this.PhysicalDamageShield = this.physicalDamageBlocks.Any();
            }

            if (pure)
            {
                this.pureDamageBlocks.Remove(handle);
                this.PureDamageShield = this.pureDamageBlocks.Any();
            }
        }

        public void UpdateDamageBlock(double handle, float newValue)
        {
            this.DamageBlocks[handle] = newValue;
            this.DamageBlock = this.DamageBlocks.MaxOrDefault(x => x.Value).Value;
        }

        #endregion
    }
}