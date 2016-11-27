namespace BreakerSharp.Utilities
{
    using System.Collections.Generic;

    using Ensage.Common.Menu;

    using SharpDX;

    /// <summary>
    ///     The menu manager.
    /// </summary>
    public class MenuManager
    {
        #region Fields

        /// <summary>
        ///     The ability combo toggler.
        /// </summary>
        private readonly MenuItem abilityComboToggler;

        /// <summary>
        ///     The armlet hp treshold.
        /// </summary>
        private readonly MenuItem armletHpTreshold;

        /// <summary>
        ///     The armlet toggle.
        /// </summary>
        private readonly MenuItem armletToggle;

        /// <summary>
        ///     The armlet toggle interval.
        /// </summary>
        private readonly MenuItem armletToggleInterval;

        /// <summary>
        ///     The charge away key.
        /// </summary>
        private readonly MenuItem chargeAwayKey;

        /// <summary>
        ///     The combo key.
        /// </summary>
        private readonly MenuItem comboKey;

        /// <summary>
        ///     The draw bash chance.
        /// </summary>
        private readonly MenuItem drawBashChance;

        /// <summary>
        ///     The draw notification.
        /// </summary>
        private readonly MenuItem drawNotification;

        /// <summary>
        ///     The draw notification health.
        /// </summary>
        private readonly MenuItem drawNotificationHealth;

        /// <summary>
        ///     The draw target.
        /// </summary>
        private readonly MenuItem drawTarget;

        /// <summary>
        ///     The draw time to hit.
        /// </summary>
        private readonly MenuItem drawTimeToHit;

        /// <summary>
        ///     The kill steal.
        /// </summary>
        private readonly MenuItem killSteal;

        /// <summary>
        ///     The min hp killsteal.
        /// </summary>
        private readonly MenuItem minHpKillsteal;

        /// <summary>
        ///     The move mode.
        /// </summary>
        private readonly MenuItem moveMode;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MenuManager" /> class.
        /// </summary>
        /// <param name="heroName">
        ///     The hero name.
        /// </param>
        public MenuManager(string heroName)
        {
            this.Menu = new Menu(Constants.AssemblyName, Constants.AssemblyName, true, heroName, true);

            var keyMenu = new Menu("Keys", "BreakerSharp.Keys");
            this.chargeAwayKey =
                new MenuItem("BreakerSharp.ChargeAwayKey", "Charge Away").SetValue(new KeyBind('B', KeyBindType.Press));
            this.comboKey = new MenuItem("BreakerSharp.ComboKey", "Combo").SetValue(new KeyBind(32, KeyBindType.Press));

            keyMenu.AddItem(this.chargeAwayKey);
            keyMenu.AddItem(this.comboKey);

            this.Menu.AddSubMenu(keyMenu);

            var comboMenu = new Menu("Combo", "BreakerSharp.Combo");
            this.abilityComboToggler =
                new MenuItem("BreakerSharp.AbilityComboToggler", "Abilities in combo").SetValue(
                    new AbilityToggler(
                        new Dictionary<string, bool>
                            {
                                { "item_invis_sword", true }, { "item_silver_edge", true }, { "item_rod_of_atos", true }, 
                                { "item_sheepstick", true }, { "item_orchid", true }, { "item_shivas_guard", true }, 
                                { "item_abyssal_blade", true }, { "item_armlet", true }, { "item_mask_of_madness", true }, 
                                { "item_urn_of_shadows", true }, { "item_solar_crest", true }, 
                                { "item_medallion_of_courage", true }, { "item_heavens_halberd", true }, 
                                { "spirit_breaker_empowering_haste", false }, { "spirit_breaker_nether_strike", true }, 
                                { "spirit_breaker_charge_of_darkness", true }
                            }));
            this.moveMode =
                new MenuItem("BreakerSharp.MoveMode", "Move mode").SetValue(
                    new StringList(new[] { "Move to Mouse", "Follow enemy" }));

            comboMenu.AddItem(this.abilityComboToggler);
            comboMenu.AddItem(this.moveMode);

            this.Menu.AddSubMenu(comboMenu);

            var drawingMenu = new Menu("Drawing", "BreakerSharp.Drawing");
            this.drawBashChance =
                new MenuItem("BreakerSharp.DrawBashChance", "Draw BashChance").SetValue(true)
                    .SetTooltip("Calculates current bash proc chance based on pseudo random distribution");
            this.drawTarget = new MenuItem("BreakerSharp.DrawTarget", "Draw TargetIndicator").SetValue(true);
            this.drawTimeToHit =
                new MenuItem("BreakerSharp.Draw", "Visible TimeToHit with Charge").SetValue(true)
                    .SetTooltip("Calculates hit time based on current charge speed");
            this.drawNotification =
                new MenuItem("BreakerSharp.DrawNotification", "Draw notification").SetValue(true)
                    .SetTooltip("Notification when enemy health goes below specified value");
            this.drawNotificationHealth =
                new MenuItem("BreakerSharp.DrawNotificationHealth", "Notification alert health").SetValue(
                    new Slider(200, 100, 500));

            drawingMenu.AddItem(this.drawBashChance);
            drawingMenu.AddItem(this.drawTarget);
            drawingMenu.AddItem(this.drawTimeToHit);
            drawingMenu.AddItem(this.drawNotification);
            drawingMenu.AddItem(this.drawNotificationHealth);

            this.Menu.AddSubMenu(drawingMenu);

            var autoUsageMenu = new Menu("AutoUsage", "BreakerSharp.AutoUsageMenu");
            this.minHpKillsteal =
                new MenuItem("BreakerSharp.MinHPKill", "Min enemy HP for killsteal").SetValue(new Slider(200, 0, 500));
            this.killSteal = new MenuItem("BreakerSharp.KillStealEnable", "KillSteal with Ulti").SetValue(false);
            this.armletToggle = new MenuItem("BreakerSharp.ArmletToggle", "Enable auto armlet toggle").SetValue(true);
            this.armletHpTreshold =
                new MenuItem("BreakerSharp.ArmletHPTreshold", "HP treshold").SetValue(new Slider(300, 100, 450));
            this.armletToggleInterval =
                new MenuItem("BreakerSharp.ArmletToggleInterval", "Armlet toggle interval").SetValue(
                    new Slider(1400, 500, 2500));
            autoUsageMenu.AddItem(
                new MenuItem("BreakerSharp.KillStealSign", "KillSteal").SetFontStyle(fontColor: Color.LightSkyBlue));

            autoUsageMenu.AddItem(this.killSteal);
            autoUsageMenu.AddItem(this.minHpKillsteal);
            autoUsageMenu.AddItem(
                new MenuItem("BreakerSharp.ArmletToggleSign", "ArmletToggle").SetFontStyle(
                    fontColor: Color.LightSkyBlue));
            autoUsageMenu.AddItem(this.armletToggle);
            autoUsageMenu.AddItem(this.armletHpTreshold);
            autoUsageMenu.AddItem(this.armletToggleInterval);

            this.Menu.AddSubMenu(autoUsageMenu);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the armlet hp treshold.
        /// </summary>
        public float ArmletHpTreshold
        {
            get
            {
                return this.armletHpTreshold.GetValue<Slider>().Value;
            }
        }

        /// <summary>
        ///     Gets the armlet toggle interval.
        /// </summary>
        public float ArmletToggleInterval
        {
            get
            {
                return this.armletToggleInterval.GetValue<Slider>().Value;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether charge away key pressed.
        /// </summary>
        public bool ChargeAwayKeyPressed
        {
            get
            {
                return this.chargeAwayKey.GetValue<KeyBind>().Active;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether combo key pressed.
        /// </summary>
        public bool ComboKeyPressed
        {
            get
            {
                return this.comboKey.GetValue<KeyBind>().Active;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether draw chance.
        /// </summary>
        public bool DrawChance
        {
            get
            {
                return this.drawBashChance.GetValue<bool>();
            }
        }

        /// <summary>
        ///     Gets a value indicating whether draw notification.
        /// </summary>
        public bool DrawNotification
        {
            get
            {
                return this.drawNotification.GetValue<bool>();
            }
        }

        /// <summary>
        ///     Gets a value indicating whether draw target.
        /// </summary>
        public bool DrawTarget
        {
            get
            {
                return this.drawTarget.GetValue<bool>();
            }
        }

        /// <summary>
        ///     Gets a value indicating whether draw time to hit.
        /// </summary>
        public bool DrawTimeToHit
        {
            get
            {
                return this.drawTimeToHit.GetValue<bool>();
            }
        }

        /// <summary>
        ///     Gets a value indicating whether enable armlet toggle.
        /// </summary>
        public bool EnableArmletToggle
        {
            get
            {
                return this.armletToggle.GetValue<bool>();
            }
        }

        /// <summary>
        ///     Gets a value indicating whether kill steal.
        /// </summary>
        public bool KillSteal
        {
            get
            {
                return this.killSteal.GetValue<bool>();
            }
        }

        /// <summary>
        ///     Gets the menu.
        /// </summary>
        public Menu Menu { get; private set; }

        /// <summary>
        ///     Gets the min health kill steal.
        /// </summary>
        public float MinHpKillsteal
        {
            get
            {
                return this.minHpKillsteal.GetValue<Slider>().Value;
            }
        }

        /// <summary>
        ///     Gets the move mode.
        /// </summary>
        public int MoveMode
        {
            get
            {
                return this.moveMode.GetValue<StringList>().SelectedIndex;
            }
        }

        /// <summary>
        ///     Gets the notification health.
        /// </summary>
        public float NotificationHealth
        {
            get
            {
                return this.drawNotificationHealth.GetValue<Slider>().Value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The item enabled.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool AbilityEnabled(string name)
        {
            return this.abilityComboToggler.GetValue<AbilityToggler>().IsEnabled(name);
        }

        #endregion
    }
}