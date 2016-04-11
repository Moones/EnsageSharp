namespace BreakerSharp.Utilities
{
    using System.Linq;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

    /// <summary>
    ///     The armlet toggler.
    /// </summary>
    public class ArmletToggler
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArmletToggler" /> class.
        /// </summary>
        /// <param name="armlet">
        ///     The armlet.
        /// </param>
        public ArmletToggler(Item armlet)
        {
            this.Armlet = armlet;
            this.Sleeper = new Sleeper();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the armlet.
        /// </summary>
        public Item Armlet { get; set; }

        /// <summary>
        ///     Gets a value indicating whether can toggle.
        /// </summary>
        public bool CanToggle
        {
            get
            {
                return this.Armlet.IsValid && !this.Sleeper.Sleeping
                       && Variables.Hero.Health <= Variables.MenuManager.ArmletHpTreshold;
            }
        }

        /// <summary>
        ///     Gets the sleeper.
        /// </summary>
        public Sleeper Sleeper { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The toggle.
        /// </summary>
        public void Toggle()
        {
            if (!Variables.Hero.CanUseItems())
            {
                return;
            }

            if (
                !Heroes.GetByTeam(Variables.EnemyTeam)
                     .Any(
                         x =>
                         x.IsValid && x.IsAlive && x.IsVisible
                         && x.Distance2D(Variables.Hero) < x.GetAttackRange() + 200)
                && !Variables.Hero.HasModifiers(
                    new[]
                        {
                            "modifier_axe_battle_hunger", "modifier_queenofpain_shadow_strike", 
                            "modifier_phoenix_fire_spirit_burn", "modifier_venomancer_poison_nova", 
                            "modifier_venomancer_venomous_gale", "modifier_huskar_burning_spear_debuff", 
                            "modifier_item_urn_damage"
                        }, 
                    false))
            {
                return;
            }

            if (Variables.Hero.HasModifier("modifier_item_armlet_unholy_strength") || this.Armlet.IsToggled)
            {
                this.Armlet.ToggleAbility();
                this.Armlet.ToggleAbility();
            }
            else
            {
                this.Armlet.ToggleAbility();
            }

            this.Sleeper.Sleep(Variables.MenuManager.ArmletToggleInterval);
        }

        #endregion
    }
}