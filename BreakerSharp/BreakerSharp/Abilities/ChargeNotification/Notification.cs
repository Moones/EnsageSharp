namespace BreakerSharp.Abilities.ChargeNotification
{
    using System;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu.Transitions;
    using Ensage.Common.Objects;

    using global::BreakerSharp.Utilities;

    using SharpDX;

    /// <summary>
    ///     The notification.
    /// </summary>
    public class Notification
    {
        #region Fields

        /// <summary>
        ///     The allies nearby sleeper.
        /// </summary>
        private readonly Sleeper alliesNearbySleeper;

        /// <summary>
        ///     The allies text.
        /// </summary>
        private readonly DrawText alliesText;

        /// <summary>
        ///     The charge text.
        /// </summary>
        private readonly DrawText chargeText;

        /// <summary>
        ///     The health text.
        /// </summary>
        private readonly DrawText healthText;

        /// <summary>
        ///     The move camera text.
        /// </summary>
        private readonly DrawText moveCameraText;

        /// <summary>
        ///     The allies nearby.
        /// </summary>
        private int alliesNearby;

        /// <summary>
        ///     The hero icon position.
        /// </summary>
        private Vector2 heroIconPosition;

        /// <summary>
        /// The transition.
        /// </summary>
        private readonly Transition transition;

        /// <summary>
        /// The position on.
        /// </summary>
        private readonly Vector2 positionOn;

        /// <summary>
        /// The position off.
        /// </summary>
        private readonly Vector2 positionOff;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Notification" /> class.
        /// </summary>
        /// <param name="duration">
        ///     The duration.
        /// </param>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <param name="size">
        ///     The size.
        /// </param>
        public Notification(float duration, Vector2 position, Vector2 size)
        {
            this.Duration = duration;
            this.alliesNearbySleeper = new Sleeper();
            this.Position = position;
            this.positionOn = position - new Vector2(size.X + 1, 0);
            this.positionOff = position + new Vector2(1, 0);
            this.Size = size;
            this.Visible = true;
            this.HeroIconSize = new Vector2((float)(size.X / 2.2), (float)(size.Y / 1.5));
            this.chargeText = new DrawText
                                  {
                                      Text = "CHARGE", Color = Color.White, FontFlags = FontFlags.AntiAlias, 
                                      Position = new Vector2(), TextSize = new Vector2((float)(this.HeroIconSize.Y / 2.1))
                                  };
            this.healthText = new DrawText
                                  {
                                      Text = string.Empty, Color = Color.White, FontFlags = FontFlags.AntiAlias, 
                                      Position = new Vector2(), TextSize = new Vector2((float)(this.HeroIconSize.Y / 3.1))
                                  };
            this.alliesText = new DrawText
                                  {
                                      Text = string.Empty, Color = Color.White, FontFlags = FontFlags.AntiAlias, 
                                      Position = new Vector2(), TextSize = new Vector2((float)(this.HeroIconSize.Y / 2.5))
                                  };
            this.moveCameraText = new DrawText
                                      {
                                          Text = "MOVE CAMERA", Color = Color.White, FontFlags = FontFlags.AntiAlias, 
                                          Position = new Vector2(), 
                                          TextSize = new Vector2((float)(this.HeroIconSize.Y / 3.4))
                                      };
            this.transition = new QuadEaseInOut(2);
            this.transition.Start(this.positionOn, this.positionOff);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the allies nearby.
        /// </summary>
        public int AlliesNearby
        {
            get
            {
                if (this.alliesNearbySleeper.Sleeping)
                {
                    return this.alliesNearby;
                }

                this.alliesNearby =
                    Heroes.GetByTeam(Variables.Hero.Team)
                        .Count(
                            x => x.IsValid && x.IsAlive && !x.Equals(Variables.Hero) && x.Distance2D(this.Hero) < 700);
                this.alliesNearbySleeper.Sleep(500);
                return this.alliesNearby;
            }
        }

        /// <summary>
        ///     Gets the display time.
        /// </summary>
        public float DisplayTime
        {
            get
            {
                return Variables.TickCount - this.StartTime;
            }
        }

        /// <summary>
        ///     Gets or sets the duration.
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        ///     Gets or sets the hero.
        /// </summary>
        public Unit Hero { get; set; }

        /// <summary>
        ///     Gets or sets the hero icon size.
        /// </summary>
        public Vector2 HeroIconSize { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether hide.
        /// </summary>
        public bool Hide { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is hidden.
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        ///     Gets or sets the start time.
        /// </summary>
        public float StartTime { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether draw.
        /// </summary>
        public bool Visible { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The click.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public void Click(Vector2 mousePosition)
        {
            if (!this.Visible)
            {
                return;
            }

            if (Utils.IsUnderRectangle(
                Game.MouseScreenPosition, 
                this.heroIconPosition.X, 
                this.heroIconPosition.Y, 
                this.HeroIconSize.X, 
                this.HeroIconSize.Y / 2))
            {
                Variables.ChargeOfDarkness.ChargeTo(this.Hero);
            }
            else if (Utils.IsUnderRectangle(
                Game.MouseScreenPosition, 
                this.heroIconPosition.X, 
                this.heroIconPosition.Y + (this.HeroIconSize.Y / 2), 
                this.HeroIconSize.X, 
                this.HeroIconSize.Y / 2))
            {
                DotaCommands.MoveCamera(this.Hero.Position);
            }
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        public void Draw()
        {
            if (!this.Visible || this.Hero == null || !this.Hero.IsValid)
            {
                return;
            }

            var hover = Utils.IsUnderRectangle(
                Game.MouseScreenPosition, 
                this.Position.X, 
                this.Position.Y, 
                this.Size.X, 
                this.Size.Y);

            if (!hover && this.DisplayTime > this.Duration && !this.Hide)
            {
                this.transition.Start(this.Position, this.positionOff);
                this.Hide = true;
                this.IsHidden = false;
            }

            if (this.Hide && !this.IsHidden)
            {
                if (hover)
                {
                    this.Hide = false;
                    this.transition.Start(this.Position, this.positionOn);
                }
                else
                {
                    if (this.Position.X > HUDInfo.ScreenSizeX())
                    {
                        this.IsHidden = true;
                    }
                    else
                    {
                        this.Position = this.transition.GetPosition();
                    }
                }
            }

            if (!this.Hide && (this.IsHidden || hover))
            {
                if (this.Position.X < HUDInfo.ScreenSizeX() - this.Size.X)
                {
                    this.IsHidden = false;
                }
                else
                {
                    this.Position = this.transition.GetPosition();
                }
            }

            Drawing.DrawRect(this.Position, this.Size, new Color(0, 0, 0, 150));
            Drawing.DrawRect(this.Position, this.Size, Color.Black, true);
            this.heroIconPosition = this.Position
                                    + new Vector2(
                                          this.HeroIconSize.X / 10, 
                                          (this.Size.Y / 2) - (((this.HeroIconSize.Y / 4) + this.HeroIconSize.Y) / 2));
            Drawing.DrawRect(
                this.heroIconPosition, 
                this.HeroIconSize, 
                Textures.GetTexture(
                    "materials/ensage_ui/heroes_horizontal/" + this.Hero.StoredName().Substring("npc_dota_hero_".Length)));
            Drawing.DrawRect(this.heroIconPosition, this.HeroIconSize, Color.Black, true);
            var perc = (float)this.Hero.Health / this.Hero.MaximumHealth;
            Drawing.DrawRect(
                this.heroIconPosition + new Vector2(0, this.HeroIconSize.Y), 
                new Vector2(this.HeroIconSize.X, this.HeroIconSize.Y / 4), 
                Color.Black);
            Drawing.DrawRect(
                this.heroIconPosition + new Vector2(0, this.HeroIconSize.Y), 
                new Vector2(this.HeroIconSize.X * perc, this.HeroIconSize.Y / 4), 
                Color.Red);
            Drawing.DrawRect(
                this.heroIconPosition + new Vector2(0, this.HeroIconSize.Y), 
                new Vector2(this.HeroIconSize.X, this.HeroIconSize.Y / 4), 
                Color.Black, 
                true);

            this.healthText.Text = this.Hero.Health + "/" + this.Hero.MaximumHealth;
            this.healthText.Position = this.heroIconPosition
                                       + new Vector2(
                                             (this.HeroIconSize.X / 2) - (this.healthText.Size.X / 2), 
                                             this.HeroIconSize.Y + (this.HeroIconSize.Y / 8)
                                             - (this.healthText.Size.Y / 2));
            this.healthText.Draw();

            hover = Utils.IsUnderRectangle(
                Game.MouseScreenPosition, 
                this.heroIconPosition.X, 
                this.heroIconPosition.Y, 
                this.HeroIconSize.X, 
                this.HeroIconSize.Y / 2);
            var a = hover ? 255 : 165;
            this.chargeText.Color = new Color(a, a, a, a);
            this.chargeText.Position = this.heroIconPosition
                                       + new Vector2((this.HeroIconSize.X / 2) - (this.chargeText.Size.X / 2), 1);

            Drawing.DrawRect(
                this.heroIconPosition, 
                new Vector2(this.HeroIconSize.X, this.HeroIconSize.Y / 2), 
                new Color(45, 45, 45, hover ? 210 : 80));

            this.chargeText.Draw();
            hover = Utils.IsUnderRectangle(
                Game.MouseScreenPosition, 
                this.heroIconPosition.X, 
                this.heroIconPosition.Y + (this.HeroIconSize.Y / 2), 
                this.HeroIconSize.X, 
                this.HeroIconSize.Y / 2);
            a = hover ? 255 : 190;
            this.moveCameraText.Color = new Color(a, a, a, a);
            this.moveCameraText.Position = this.heroIconPosition
                                           + new Vector2(
                                                 (this.HeroIconSize.X / 2) - (this.moveCameraText.Size.X / 2), 
                                                 (this.HeroIconSize.Y / 2)
                                                 + ((this.HeroIconSize.Y / 4) - (this.moveCameraText.Size.Y / 2)));
            Drawing.DrawRect(
                this.heroIconPosition + new Vector2(0, this.HeroIconSize.Y / 2), 
                new Vector2(this.HeroIconSize.X, this.HeroIconSize.Y / 2), 
                new Color(45, 45, 45, hover ? 210 : 80));

            this.moveCameraText.Draw();

            this.alliesText.Text = this.AlliesNearby > 1 || this.AlliesNearby == 0
                                       ? this.AlliesNearby + " allies near"
                                       : this.AlliesNearby + " ally near";
            this.alliesText.Position = this.heroIconPosition
                                       + new Vector2(
                                             this.HeroIconSize.X + 1, 
                                             (this.HeroIconSize.Y / 2) - (this.alliesText.Size.Y / 3));
            this.alliesText.Draw();
        }

        /// <summary>
        ///     The pop up.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        public void PopUp(Unit hero)
        {
            if (hero == null || !hero.IsValid)
            {
                return;
            }

            this.Hero = hero;
            this.Visible = true;
            this.Hide = false;
            this.IsHidden = true;
            this.StartTime = Variables.TickCount;
            this.transition.Start(this.positionOff, this.positionOn);
        }

        #endregion
    }
}