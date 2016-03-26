namespace BreakerSharp.Utilities
{
    using System.Collections.Generic;

    using Ensage.Common.Menu;

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
            keyMenu.AddItem(this.chargeAwayKey);
            this.comboKey = new MenuItem("BreakerSharp.ComboKey", "Combo").SetValue(new KeyBind(32, KeyBindType.Press));
            keyMenu.AddItem(this.comboKey);
            this.Menu.AddSubMenu(keyMenu);
            var comboMenu = new Menu("Combo", "BreakerSharp.Combo");
            this.abilityComboToggler =
                new MenuItem("BreakerSharp.AbilityComboToggler", "Abilities in combo").SetValue(
                    new AbilityToggler(
                        new Dictionary<string, bool>
                            {
                                { "item_rod_of_atos", true }, { "item_sheepstick", true }, { "item_orchid", true }, 
                                { "item_shivas_guard", true }, { "item_abyssal_blade", true }, 
                                { "item_mask_of_madness", true }, { "item_urn_of_shadows", true }, 
                                { "item_solar_crest", true }, { "item_medallion_of_courage", true }, 
                                { "item_heavens_halberd", true }, { "spirit_breaker_nether_strike", true }, 
                                { "spirit_breaker_charge_of_darkness", true }
                            }));
            comboMenu.AddItem(this.abilityComboToggler);
            this.moveMode =
                new MenuItem("BreakerSharp.MoveMode", "Move mode").SetValue(
                    new StringList(new[] { "Move to Mouse", "Follow enemy" }));
            comboMenu.AddItem(this.moveMode);
            this.Menu.AddSubMenu(comboMenu);
            var drawingMenu = new Menu("Drawing", "BreakerSharp.Drawing");
            this.drawBashChance =
                new MenuItem("BreakerSharp.DrawBashChance", "Draw BashChance").SetValue(true)
                    .SetTooltip("Calculates current bash proc chance based on pseudo random distribution");
            drawingMenu.AddItem(this.drawBashChance);
            this.drawTarget = new MenuItem("BreakerSharp.DrawTarget", "Draw TargetIndicator").SetValue(true);
            drawingMenu.AddItem(this.drawTarget);
            this.drawTimeToHit =
                new MenuItem("BreakerSharp.DrawTimeToHit", "Draw TimeToHit with Charge").SetValue(true)
                    .SetTooltip("Calculates hit time based on current charge speed");
            drawingMenu.AddItem(this.drawTimeToHit);
            this.killSteal = new MenuItem("BreakerSharp.KillSteal", "KillSteal with Ulti").SetValue(false);
            this.Menu.AddItem(this.killSteal);
            this.Menu.AddSubMenu(drawingMenu);
        }

        #endregion

        #region Public Properties

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
        ///     Gets the move mode.
        /// </summary>
        public int MoveMode
        {
            get
            {
                return this.moveMode.GetValue<StringList>().SelectedIndex;
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