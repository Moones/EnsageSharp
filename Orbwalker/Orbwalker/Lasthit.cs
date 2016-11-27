namespace Orbwalker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

    /// <summary>
    ///     Preparation for last hit.
    /// </summary>
    public class Lasthit
    {
        #region Fields

        /// <summary>
        ///     The damage dictionary.
        /// </summary>
        private readonly Dictionary<string, float> damageDictionary;

        /// <summary>
        ///     The me.
        /// </summary>
        private readonly Hero me;

        /// <summary>
        ///     The creep.
        /// </summary>
        private Creep creep;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Lasthit" /> class.
        /// </summary>
        /// <param name="me">
        ///     The me.
        /// </param>
        public Lasthit(Hero me)
        {
            this.me = me;
            this.damageDictionary = new Dictionary<string, float>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The creep.
        /// </summary>
        /// <returns>
        ///     The <see cref="Creep" />.
        /// </returns>
        public Creep Creep()
        {
            return this.creep;
        }

        /// <summary>
        ///     The is being attacked.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public float IsBeingAttacked()
        {
            if (this.creep == null || !this.creep.IsValid)
            {
                return 0;
            }

            return
                Creeps.All.Count(
                    x =>
                    x.IsValid && !x.Equals(this.creep) && x.Team != this.creep.Team && x.Distance2D(this.me) < 1000
                    && x.Distance2D(this.creep) < x.AttackRange
                    && Math.Max(
                        Math.Abs(x.FindAngleR() - Utils.DegreeToRadian(x.FindAngleForTurnTime(this.creep.Position)))
                        - 20, 
                        0) <= 0.09 && x.IsAttacking())
                + Heroes.GetByTeam(this.creep.GetEnemyTeam())
                      .Count(
                          x =>
                          x.Distance2D(this.me) < 1000 && x.Distance2D(this.creep) < x.GetAttackRange()
                          && Math.Max(
                              Math.Abs(
                                  x.FindAngleR() - Utils.DegreeToRadian(x.FindAngleForTurnTime(this.creep.Position)))
                              - 20, 
                              0) <= 0.09 && x.IsAttacking())
                + ObjectManager.TrackingProjectiles.Count(x => x.Target != null && x.Target.Equals(this.creep));
        }

        /// <summary>
        ///     The is last hittable.
        /// </summary>
        /// <param name="damageAmount">
        ///     The damage amount.
        /// </param>
        /// <param name="damageType">
        ///     The damage type.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool IsLasthittable(float damageAmount, DamageType damageType)
        {
            if (this.creep == null || !this.creep.IsValid)
            {
                return false;
            }

            float damage;
            var name = this.creep.ClassID.ToString() + damageAmount + damageType;
            var contains = this.damageDictionary.TryGetValue(name, out damage);
            if (contains && !Utils.SleepCheck("Orbwalker.Lasthit.Damage." + name))
            {
                return damage >= this.creep.Health;
            }

            damage = this.creep.DamageTaken(damageAmount, damageType, this.me);
            if (!contains)
            {
                this.damageDictionary.Add(name, damage);
            }

            Utils.Sleep(500, "Orbwalker.Lasthit.Damage." + name);

            return damage >= this.creep.Health;
        }

        /// <summary>
        ///     The update creep.
        /// </summary>
        public void UpdateCreep()
        {
            this.creep =
                Creeps.All.Where(
                    x => x.IsValid && x.Team != this.me.Team && x.Distance2D(this.me) < this.me.GetAttackRange() + 150)
                    .MinOrDefault(x => x.Health + x.Distance2D(this.me));
        }

        #endregion
    }
}