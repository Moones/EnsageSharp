namespace BreakerSharp.Abilities
{
    using System.Linq;

    using Ensage;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

    using global::BreakerSharp.Utilities;

    /// <summary>
    ///     The nether strike.
    /// </summary>
    public class NetherStrike
    {
        #region Fields

        /// <summary>
        ///     The ability.
        /// </summary>
        private readonly Ability ability;

        /// <summary>
        ///     The sleeper.
        /// </summary>
        private readonly Sleeper sleeper;

        /// <summary>
        ///     The damage.
        /// </summary>
        private float damage;

        /// <summary>
        ///     The last level.
        /// </summary>
        private uint lastLevel;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NetherStrike" /> class.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        public NetherStrike(Ability ability)
        {
            this.ability = ability;
            this.damage = ability.GetAbilityData("damage");
            this.lastLevel = ability.Level;
            this.CastPoint = ability.FindCastPoint();
            this.sleeper = new Sleeper();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the cast point.
        /// </summary>
        public double CastPoint { get; private set; }

        /// <summary>
        ///     Gets the get damage.
        /// </summary>
        public float GetDamage
        {
            get
            {
                if (this.lastLevel == this.ability.Level)
                {
                    return this.damage;
                }

                this.lastLevel = this.ability.Level;
                this.damage = this.ability.GetAbilityData("damage");
                return this.damage;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The can use on.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool CanUseOn(Unit target)
        {
            return target != null && target.IsVisible
                   && target.Distance2D(Variables.Hero) <= this.ability.GetCastRange() + 100
                   && !target.IsLinkensProtected();
        }

        /// <summary>
        ///     The kill steal.
        /// </summary>
        /// <param name="minHealth">
        ///     The min Health.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool KillSteal(float minHealth)
        {
            foreach (var hero in
                Heroes.GetByTeam(Variables.EnemyTeam)
                    .Where(
                        hero =>
                        hero.IsValid && hero.IsVisible && hero.IsAlive && this.CanUseOn(hero) && hero.Health > minHealth
                        && AbilityDamage.CalculateDamage(this.ability, Variables.Hero, hero) >= hero.Health
                        && hero.CanDie()))
            {
                this.UseOn(hero);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     The use on.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool UseOn(Unit target)
        {
            if (this.sleeper.Sleeping || !this.CanUseOn(target))
            {
                return false;
            }

            if (!this.ability.CastStun(target))
            {
                return false;
            }

            this.sleeper.Sleep(500);
            return true;
        }

        #endregion
    }
}