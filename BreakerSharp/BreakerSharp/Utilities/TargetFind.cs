namespace BreakerSharp.Utilities
{
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

    using SharpDX;

    /// <summary>
    ///     The target find.
    /// </summary>
    public class TargetFind
    {
        #region Fields

        /// <summary>
        ///     The hero icon.
        /// </summary>
        private readonly DotaTexture heroIcon;

        /// <summary>
        ///     The sleeper.
        /// </summary>
        private readonly Sleeper sleeper;

        /// <summary>
        ///     The icon size.
        /// </summary>
        private Vector2 iconSize;

        /// <summary>
        ///     The locked.
        /// </summary>
        private bool locked;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TargetFind" /> class.
        /// </summary>
        public TargetFind()
        {
            this.sleeper = new Sleeper();
            this.heroIcon = Drawing.GetTexture("materials/ensage_ui/miniheroes/spirit_breaker");
            this.iconSize = new Vector2(HUDInfo.GetHpBarSizeY() * 2);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the get target.
        /// </summary>
        public Hero Target { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw target.
        /// </summary>
        public void DrawTarget()
        {
            if (this.Target == null || !this.Target.IsVisible || !this.Target.IsAlive)
            {
                return;
            }

            Vector2 screenPosition;
            if (
                !Drawing.WorldToScreen(
                    this.Target.Position + new Vector3(0, 0, this.Target.HealthBarOffset / 3), 
                    out screenPosition))
            {
                return;
            }

            screenPosition += new Vector2(-this.iconSize.X, 0);
            Drawing.DrawRect(screenPosition, this.iconSize, this.heroIcon);

            if (this.locked)
            {
                Drawing.DrawText(
                    "LOCKED", 
                    screenPosition + new Vector2(this.iconSize.X + 2, 1), 
                    new Vector2((float)(this.iconSize.X * 0.85)), 
                    new Color(255, 150, 110), 
                    FontFlags.AntiAlias | FontFlags.Additive);
            }
        }

        /// <summary>
        ///     The find.
        /// </summary>
        public void Find()
        {
            if (this.sleeper.Sleeping)
            {
                return;
            }

            if (this.locked && this.Target != null && this.Target.IsAlive)
            {
                return;
            }

            this.UnlockTarget();
            this.Target =
                Heroes.GetByTeam(Variables.EnemyTeam)
                    .Where(
                        x =>
                        x.IsValid && x.IsAlive && !x.IsIllusion && x.IsVisible
                        && x.Distance2D(Game.MousePosition) < 2000)
                    .MinOrDefault(x => x.Distance2D(Game.MousePosition));
            this.sleeper.Sleep(100);
        }

        /// <summary>
        ///     The lock target.
        /// </summary>
        public void LockTarget()
        {
            this.locked = true;
        }

        /// <summary>
        ///     The unlock target.
        /// </summary>
        public void UnlockTarget()
        {
            this.locked = false;
        }

        #endregion
    }
}