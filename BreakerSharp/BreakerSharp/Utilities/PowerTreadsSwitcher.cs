namespace BreakerSharp.Utilities
{
    using Ensage;
    using Ensage.Items;

    /// <summary>
    ///     The power treads switcher.
    /// </summary>
    public class PowerTreadsSwitcher
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PowerTreadsSwitcher" /> class.
        /// </summary>
        /// <param name="powerTreads">
        ///     The power treads.
        /// </param>
        public PowerTreadsSwitcher(PowerTreads powerTreads)
        {
            this.PowerTreads = powerTreads;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.PowerTreads != null && this.PowerTreads.IsValid;
            }
        }

        /// <summary>
        ///     Gets or sets the power treads.
        /// </summary>
        public PowerTreads PowerTreads { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The switch to.
        /// </summary>
        /// <param name="attribute">
        ///     The attribute.
        /// </param>
        /// <param name="currentAttribute">
        ///     The current Attribute.
        /// </param>
        /// <param name="queue">
        ///     The queue.
        /// </param>
        public void SwitchTo(Attribute attribute, Attribute currentAttribute, bool queue)
        {
            if (attribute == Attribute.Agility)
            {
                if (currentAttribute == Attribute.Strength)
                {
                    this.PowerTreads.UseAbility(queue);
                    this.PowerTreads.UseAbility(queue);
                }
                else if (currentAttribute == Attribute.Intelligence)
                {
                    this.PowerTreads.UseAbility(queue);
                }
            }
            else if (attribute == Attribute.Strength)
            {
                if (currentAttribute == Attribute.Intelligence)
                {
                    this.PowerTreads.UseAbility(queue);
                    this.PowerTreads.UseAbility(queue);
                }
                else if (currentAttribute == Attribute.Agility)
                {
                    this.PowerTreads.UseAbility(queue);
                }
            }
            else if (attribute == Attribute.Intelligence)
            {
                if (currentAttribute == Attribute.Agility)
                {
                    this.PowerTreads.UseAbility(queue);
                    this.PowerTreads.UseAbility(queue);
                }
                else if (currentAttribute == Attribute.Strength)
                {
                    this.PowerTreads.UseAbility(queue);
                }
            }
        }

        #endregion
    }
}