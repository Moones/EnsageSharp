// <copyright file="DamageManipulationValues.cs" company="EnsageSharp">
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

    using Ability.Core.AbilityFactory.AbilityModifier;
    using Ability.Core.AbilityFactory.AbilitySkill;
    using Ability.Core.AbilityFactory.Utilities;

    internal class DamageManipulationValues : IDamageManipulationValues
    {
        #region Fields

        private Dictionary<double, ValueHolder<double>> modifierValues = new Dictionary<double, ValueHolder<double>>();

        private Dictionary<double, ValueHolder<double>> skillValues = new Dictionary<double, ValueHolder<double>>();

        private Dictionary<double, ValueHolder<double>> specialModifierValues =
            new Dictionary<double, ValueHolder<double>>();

        private bool specialModifierValuesAny;

        private Dictionary<double, ValueHolder<double>> specialSkillValues =
            new Dictionary<double, ValueHolder<double>>();

        private bool specialSkillValuesAny;

        private double value;

        #endregion

        #region Constructors and Destructors

        internal DamageManipulationValues(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        public IAbilityUnit Unit { get; }

        public Notifier ValueChanged { get; } = new Notifier();

        #endregion

        #region Public Methods and Operators

        public void AddModifierValue(IAbilityModifier modifier, double value, bool willExpire = false)
        {
            this.modifierValues.Add(
                modifier.ModifierHandle,
                new ValueHolder<double>(value, willExpire, () => modifier.SourceModifier.DieTime));
            this.value += value;
            this.ValueChanged.Notify();
        }

        public void AddSkillValue(IAbilitySkill skill, double value)
        {
            this.skillValues.Add(skill.SkillHandle, new ValueHolder<double>(value));
            this.value += value;
            this.ValueChanged.Notify();
        }

        public void AddSpecialModifierValue(
            IAbilityModifier modifier,
            Func<IAbilityUnit, float, double> getValue,
            bool willExpire = false)
        {
            this.specialModifierValues.Add(
                modifier.ModifierHandle,
                new ValueHolder<double>(getValue, willExpire, () => modifier.SourceModifier.DieTime));
            this.specialModifierValuesAny = true;
            this.ValueChanged.Notify();
        }

        public void AddSpecialSkillValue(IAbilitySkill skill, Func<IAbilityUnit, float, double> getValue)
        {
            this.specialSkillValues.Add(skill.SkillHandle, new ValueHolder<double>(getValue));
            this.specialSkillValuesAny = true;
            this.ValueChanged.Notify();
        }

        public virtual double GetPredictedValue(IAbilityUnit source, float damageValue, float time)
        {
            var tempValue = this.value;
            foreach (var modifierValue in this.modifierValues)
            {
                if (modifierValue.Value.WillExpire && time >= modifierValue.Value.ExpireTime.Invoke())
                {
                    tempValue -= modifierValue.Value.Value;
                }
            }

            if (this.specialSkillValuesAny)
            {
                foreach (var specialSkillValue in this.specialSkillValues)
                {
                    tempValue += specialSkillValue.Value.GetSpecialValue(source, damageValue);
                }
            }

            if (this.specialModifierValuesAny)
            {
                foreach (var specialModifierValue in this.specialModifierValues)
                {
                    if (!specialModifierValue.Value.WillExpire || time < specialModifierValue.Value.ExpireTime.Invoke())
                    {
                        tempValue += specialModifierValue.Value.GetSpecialValue(source, damageValue);
                    }
                }
            }

            return tempValue;
        }

        public virtual double GetValue(IAbilityUnit source, float damageValue)
        {
            var tempValue = this.value;
            if (this.specialModifierValuesAny)
            {
                foreach (var specialModifierValue in this.specialModifierValues)
                {
                    tempValue += specialModifierValue.Value.GetSpecialValue(source, damageValue);
                }
            }

            if (this.specialSkillValuesAny)
            {
                foreach (var specialSkillValue in this.specialSkillValues)
                {
                    tempValue += specialSkillValue.Value.GetSpecialValue(source, damageValue);
                }
            }

            return tempValue;
        }

        public void RemoveModifierValue(IAbilityModifier modifier, double value)
        {
            this.modifierValues.Remove(modifier.ModifierHandle);
            this.value -= value;
            this.ValueChanged.Notify();
        }

        public void RemoveSkillValue(IAbilitySkill skill, double value)
        {
            this.skillValues.Remove(skill.SkillHandle);
            this.value -= value;
            this.ValueChanged.Notify();
        }

        public void RemoveSpecialModifierValue(IAbilityModifier modifier)
        {
            this.specialModifierValues.Remove(modifier.ModifierHandle);
            this.specialModifierValuesAny = this.specialModifierValues.Any();
            this.ValueChanged.Notify();
        }

        public void RemoveSpecialSkillValue(IAbilitySkill skill)
        {
            this.specialSkillValues.Remove(skill.SkillHandle);
            this.specialSkillValuesAny = this.specialSkillValues.Any();
            this.ValueChanged.Notify();
        }

        public void UpdateSpecialModifierValue(IAbilityModifier modifier, Func<IAbilityUnit, float, double> newGetValue)
        {
            var valueHolder = this.specialModifierValues[modifier.ModifierHandle];
            valueHolder.GetSpecialValue = newGetValue;
            this.ValueChanged.Notify();
        }

        public void UpdateSpecialSkillValue(IAbilitySkill skill, Func<IAbilityUnit, float, double> newGetValue)
        {
            var valueHolder = this.specialSkillValues[skill.SkillHandle];
            valueHolder.GetSpecialValue = newGetValue;
            this.ValueChanged.Notify();
        }

        public void UpdateModifierValue(IAbilityModifier modifier, double newValue)
        {
            var valueHolder = this.modifierValues[modifier.ModifierHandle];
            this.value -= valueHolder.Value;
            valueHolder.Value = newValue;
            this.value += newValue;
            this.ValueChanged.Notify();
        }

        public void UpdateSkillValue(IAbilitySkill skill, double newValue)
        {
            var valueHolder = this.skillValues[skill.SkillHandle];
            this.value -= valueHolder.Value;
            valueHolder.Value = newValue;
            this.value += newValue;
            this.ValueChanged.Notify();
        }

        #endregion
    }
}