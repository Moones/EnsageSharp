namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.DamageManipulation
{
    using System;

    internal class ValueHolder<T> : IValueHolder<T>
    {
        internal ValueHolder(T value, bool willExpire = false, Func<float> expireTime = null)
        {
            this.Value = value;
            this.WillExpire = willExpire;
            this.ExpireTime = expireTime;
        }

        internal ValueHolder(Func<IAbilityUnit, float, T> getValue, bool willExpire = false, Func<float> expireTime = null)
        {
            this.GetSpecialValue = getValue;
            this.WillExpire = willExpire;
            this.ExpireTime = expireTime;
        }

        public bool WillExpire { get; }

        public Func<float> ExpireTime { get; }

        public Func<IAbilityUnit, float, T> GetSpecialValue { get; set; }

        public T Value { get; set; }
    }
}
