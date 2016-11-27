namespace BreakerSharp.Abilities
{
    using System;

    using Ensage;
    using Ensage.Common;

    using SharpDX;

    /// <summary>
    ///     The greater bash.
    /// </summary>
    public class GreaterBash
    {
        #region Constants

        /// <summary>
        ///     The c.
        /// </summary>
        private const double C = 0.04069;

        #endregion

        #region Fields

        /// <summary>
        ///     The ability.
        /// </summary>
        private readonly Ability ability;

        /// <summary>
        ///     The ability icon.
        /// </summary>
        private readonly DotaTexture abilityIcon;

        /// <summary>
        ///     The attacked.
        /// </summary>
        private bool attacked;

        /// <summary>
        ///     The icon size.
        /// </summary>
        private Vector2 iconSize;

        /// <summary>
        ///     The unsuccessful attack count.
        /// </summary>
        private uint unsuccessfulAttackCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GreaterBash" /> class.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        public GreaterBash(Ability ability)
        {
            this.ability = ability;
            this.unsuccessfulAttackCount = 1;
            this.abilityIcon = Drawing.GetTexture("materials/ensage_ui/spellicons/spirit_breaker_greater_bash");
            this.iconSize = new Vector2(HUDInfo.GetHpBarSizeY() * 2);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the get chance.
        /// </summary>
        public double GetChance
        {
            get
            {
                return C * this.unsuccessfulAttackCount;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The check proc.
        /// </summary>
        public void CheckProc()
        {
            if (this.ability.Level <= 0)
            {
                return;
            }

            var onCooldown = Orbwalking.AttackOnCooldown();
            if (this.attacked && !onCooldown)
            {
                this.attacked = false;
            }

            var canCancel = Orbwalking.CanCancelAnimation();
            if (!canCancel || !onCooldown || this.attacked)
            {
                return;
            }

            DelayAction.Add(
                Game.Ping, 
                () =>
                    {
                        if (this.ability.Cooldown > 0)
                        {
                            this.unsuccessfulAttackCount = 1;
                        }
                        else
                        {
                            this.unsuccessfulAttackCount += 1;
                        }
                    });
            this.attacked = true;
        }

        /// <summary>
        ///     The draw chance.
        /// </summary>
        public void DrawChance()
        {
            if (this.ability.Level <= 0)
            {
                return;
            }

            Vector2 screenPosition;
            if (
                !Drawing.WorldToScreen(
                    Variables.Hero.Position + new Vector3(0, 0, Variables.Hero.HealthBarOffset / 3), 
                    out screenPosition))
            {
                return;
            }

            screenPosition += new Vector2((float)(-this.iconSize.X * 0.2), this.iconSize.Y * 2);
            Drawing.DrawRect(screenPosition, this.iconSize, this.abilityIcon);
            var chance = Math.Floor(this.GetChance * 100) + " %";
            Drawing.DrawText(
                chance, 
                screenPosition + new Vector2(this.iconSize.X + 2, 2), 
                new Vector2((float)(this.iconSize.X * 0.85)), 
                Color.White, 
                FontFlags.AntiAlias | FontFlags.Additive);
        }

        #endregion
    }
}