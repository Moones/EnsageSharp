namespace BreakerSharp.Abilities
{
    using System;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

    using global::BreakerSharp.Abilities.ChargeNotification;
    using global::BreakerSharp.Utilities;

    using SharpDX;

    using Attribute = Ensage.Attribute;

    /// <summary>
    ///     The charge of darkness.
    /// </summary>
    public class ChargeOfDarkness
    {
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
        ///     The notification.
        /// </summary>
        private readonly Notification notification;

        /// <summary>
        ///     The sleeper.
        /// </summary>
        private readonly Sleeper sleeper;

        /// <summary>
        ///     The icon size.
        /// </summary>
        private Vector2 iconSize;

        /// <summary>
        ///     The last level.
        /// </summary>
        private uint lastLevel;

        /// <summary>
        ///     The last target.
        /// </summary>
        private Unit lastTarget;

        /// <summary>
        ///     The speed.
        /// </summary>
        private float speed;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChargeOfDarkness" /> class.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        public ChargeOfDarkness(Ability ability)
        {
            this.ability = ability;
            this.speed = ability.GetProjectileSpeed();
            this.sleeper = new Sleeper();
            this.lastLevel = ability.Level;
            this.abilityIcon = Drawing.GetTexture("materials/ensage_ui/spellicons/spirit_breaker_charge_of_darkness");
            this.iconSize = new Vector2(HUDInfo.GetHpBarSizeY() * 2);
            this.CastPoint = this.ability.FindCastPoint();
            this.notification = new Notification(
                5000,
                new Vector2(HUDInfo.ScreenSizeX(), (float)(HUDInfo.ScreenSizeY() / 2.3)), 
                new Vector2(HUDInfo.ScreenSizeX() / 11, HUDInfo.ScreenSizeX() / 30));
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether can charge.
        /// </summary>
        public bool CanCharge
        {
            get
            {
                return this.ability.CanBeCasted() && !this.sleeper.Sleeping && !this.IsCharging;
            }
        }

        /// <summary>
        ///     Gets the cast point.
        /// </summary>
        public double CastPoint { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether is charging.
        /// </summary>
        public bool IsCharging
        {
            get
            {
                return this.ability.IsInAbilityPhase
                       || Variables.Hero.HasModifier("modifier_spirit_breaker_charge_of_darkness")
                       || this.sleeper.Sleeping;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the speed.
        /// </summary>
        private float Speed
        {
            get
            {
                if (this.lastLevel == this.ability.Level)
                {
                    return this.speed;
                }

                this.speed = this.ability.GetProjectileSpeed();
                this.lastLevel = this.ability.Level;
                return this.speed;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The charge away.
        /// </summary>
        public void ChargeAway()
        {
            if (!this.CanCharge)
            {
                return;
            }

            var optimalCreep =
                Creeps.All.Where(
                    x =>
                    x.IsValid && x.IsAlive && x.IsSpawned && x.Team == Variables.EnemyTeam
                    && x.Distance2D(Variables.Hero) > 2000).MinOrDefault(x => Variables.Hero.GetTurnTime(x.Position));

            if (optimalCreep == null)
            {
                return;
            }

            this.ChargeTo(optimalCreep);
        }

        /// <summary>
        ///     The charge to.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool ChargeTo(Unit target)
        {
            if (target == null || !target.IsValid)
            {
                return false;
            }

            if (!this.CanCharge)
            {
                return false;
            }

            if (target.IsMagicImmune() || target.IsInvul())
            {
                return false;
            }

            this.lastTarget = target;
            var lastAttribute = Attribute.Strength;
            if (Variables.PowerTreadsSwitcher != null && Variables.PowerTreadsSwitcher.IsValid
                && Variables.Hero.Health > 300)
            {
                lastAttribute = Variables.PowerTreadsSwitcher.PowerTreads.ActiveAttribute;
                Variables.PowerTreadsSwitcher.SwitchTo(
                    Attribute.Intelligence, 
                    Variables.PowerTreadsSwitcher.PowerTreads.ActiveAttribute, 
                    false);
            }

            this.ability.UseAbility(target);
            if (Variables.PowerTreadsSwitcher != null && Variables.PowerTreadsSwitcher.IsValid
                && Variables.Hero.Health > 300)
            {
                DelayAction.Add(
                    (float)((this.CastPoint * 1000) + (Variables.Hero.GetTurnTime(target) * 1000) + Game.Ping + 200), 
                    () => { Variables.PowerTreadsSwitcher.SwitchTo(lastAttribute, Attribute.Intelligence, false); });
            }

            this.sleeper.Sleep(
                (float)((this.CastPoint * 1000) + Game.Ping + (Variables.Hero.GetTurnTime(target) * 1000)));
            return true;
        }

        /// <summary>
        ///     The check health and alert.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <param name="healthToAlert">
        ///     The health to alert.
        /// </param>
        public void CheckHpAndAlert(Unit hero, float healthToAlert)
        {
            if (!(hero.Health <= healthToAlert) || !Utils.SleepCheck("BreakerSharp.Notification." + hero.StoredName()))
            {
                return;
            }

            this.notification.PopUp(hero);
            Utils.Sleep(15000, "BreakerSharp.Notification." + hero.StoredName());
        }

        /// <summary>
        ///     The click.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public void Click(Vector2 mousePosition)
        {
            this.notification.Click(mousePosition);
        }

        /// <summary>
        ///     The draw time to hit.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        public void Draw(Unit target)
        {
            if (Variables.MenuManager.DrawNotification)
            {
                this.notification.Draw();
            }

            if (target == null || !target.IsVisible || !target.IsAlive)
            {
                return;
            }

            if ((!this.CanCharge && !this.IsCharging) || !Variables.MenuManager.DrawTimeToHit)
            {
                return;
            }

            var position = target.Position;

            if (this.IsCharging && this.lastTarget != null && this.lastTarget.IsValid)
            {
                position = this.lastTarget.Position;
                target = this.lastTarget;
            }

            Vector2 screenPosition;
            if (!Drawing.WorldToScreen(position + new Vector3(0, 0, target.HealthBarOffset / 3), out screenPosition))
            {
                return;
            }

            if (this.IsCharging)
            {
                if (screenPosition.X > HUDInfo.ScreenSizeX() * 0.921875)
                {
                    screenPosition = new Vector2((float)(HUDInfo.ScreenSizeX() * 0.921875), screenPosition.Y);
                }

                if (screenPosition.X < 50)
                {
                    screenPosition = new Vector2(50, screenPosition.Y);
                }

                if (screenPosition.Y > HUDInfo.ScreenSizeY() * 0.7685185185185185)
                {
                    screenPosition = new Vector2(screenPosition.X, (float)(HUDInfo.ScreenSizeY() * 0.7685185185185185));
                }

                if (screenPosition.Y < 50)
                {
                    screenPosition = new Vector2(screenPosition.X, 50);
                }
            }

            screenPosition += new Vector2(-this.iconSize.X, this.iconSize.Y + 2);
            Drawing.DrawRect(screenPosition, this.iconSize, this.abilityIcon);
            var time = Math.Max(Math.Floor(this.TimeToHit(target) * 10) / 10, 0) + "s to impact";
            Drawing.DrawText(
                time, 
                screenPosition + new Vector2(this.iconSize.X + 2, 1), 
                new Vector2((float)(this.iconSize.X * 0.85)), 
                Color.LightSkyBlue, 
                FontFlags.AntiAlias | FontFlags.Additive);
        }

        /// <summary>
        ///     The time to hit.
        /// </summary>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public double TimeToHit()
        {
            return (Variables.Hero.Distance2D(this.lastTarget) - Variables.Hero.HullRadius - this.lastTarget.HullRadius
                    - 150) / this.Speed;
        }

        /// <summary>
        ///     The time to hit.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public double TimeToHit(Unit target)
        {
            if (this.IsCharging && this.lastTarget != null && this.lastTarget.IsValid)
            {
                return this.TimeToHit();
            }

            if (target == null)
            {
                return 0;
            }

            return (Variables.Hero.Distance2D(target) - Variables.Hero.HullRadius - target.HullRadius - 150)
                   / this.Speed;
        }

        #endregion
    }
}