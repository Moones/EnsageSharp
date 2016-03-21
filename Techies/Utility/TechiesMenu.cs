namespace Techies.Utility
{
    using System.Linq;

    using Ensage.Common.Menu;

    /// <summary>
    ///     The techies menu.
    /// </summary>
    internal class TechiesMenu
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TechiesMenu" /> class.
        /// </summary>
        public TechiesMenu()
        {
            var menu = new Menu("#TECHIES", "techies", true, "npc_dota_hero_techies", true);
            var optionsMenu = new Menu("Options", "options");
            var detonationMenu = new Menu("Auto Detonation", "autoDetonation");
            detonationMenu.AddItem(new MenuItem("autoDetonate", "Detonate on heroes").SetValue(true));
            detonationMenu.AddItem(new MenuItem("autoDetonateCreeps", "Detonate on creeps").SetValue(false));
            detonationMenu.AddItem(
                new MenuItem("autoDetonateProtection", "Detonate when enemy tries to destroy").SetValue(true)
                    .SetTooltip("Detonates mine when enemy tries to kill it with auto attack or item"));
            detonationMenu.AddItem(new MenuItem("detonateAllMines", "Detonate all mines in stack").SetValue(false))
                .SetTooltip(
                    "Instead of detonating only needed amount of mines, assembly will detonate all mines in stack");
            optionsMenu.AddSubMenu(detonationMenu);
            var forceStaffMenu = new Menu("Auto ForceStaff", "autoForceStaff");
            forceStaffMenu.AddItem(new MenuItem("useForceStaff", "Use ForceStaff").SetValue(true));
            forceStaffMenu.AddItem(new MenuItem("checkRotating", "Dont use on turning enemy").SetValue(false));
            forceStaffMenu.AddItem(
                new MenuItem("straightTime", "Minimum straight time (secs)").SetValue(new Slider(0, 0, 5))
                    .SetTooltip("Use force staff only on enemies who havent changed their direction X seconds"));
            optionsMenu.AddSubMenu(forceStaffMenu);
            var drawingMenu = new Menu("Drawings", "drawings");
            drawingMenu.AddItem(new MenuItem("drawTopPanel", "Draw TopPanel").SetValue(true));
            drawingMenu.AddItem(new MenuItem("drawSuicideKills", "Draw killability with Suicide").SetValue(true));
            drawingMenu.AddItem(new MenuItem("drawRemoteMineRange", "Draw range for remote mines").SetValue(true))
                .ValueChanged += RemoteMines_OnValueChanged;
            drawingMenu.AddItem(new MenuItem("drawLandMineRange", "Draw range for land mines").SetValue(true))
                .ValueChanged += LandMines_OnValueChanged;
            drawingMenu.AddItem(new MenuItem("drawStasisTrapRange", "Draw range for stasis traps").SetValue(true))
                .ValueChanged += StasisTraps_OnValueChanged;
            drawingMenu.AddItem(new MenuItem("drawStackOverlay", "Draw StackOverlay").SetValue(true));
            var suicideMenu = new Menu("Auto Suicide", "autoSuicide");
            suicideMenu.AddItem(new MenuItem("autoSuicide", "Auto Suicide").SetValue(true));
            suicideMenu.AddItem(
                new MenuItem("HPTreshold", "HP treshold percent").SetValue(new Slider(100, 1))
                    .SetTooltip("Use Suicide only if Your health percent goes below specified treshold"));
            menu.AddSubMenu(drawingMenu);
            optionsMenu.AddSubMenu(suicideMenu);
            menu.AddSubMenu(optionsMenu);
            menu.AddToMainMenu();
            this.DetonationMenu = detonationMenu;
            this.DrawingsMenu = drawingMenu;
            this.ForceStaffMenu = forceStaffMenu;
            this.SuicideMenu = suicideMenu;
            this.MainMenu = menu;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the detonation menu.
        /// </summary>
        public Menu DetonationMenu { get; set; }

        /// <summary>
        ///     Gets or sets the drawings menu.
        /// </summary>
        public Menu DrawingsMenu { get; set; }

        /// <summary>
        ///     Gets or sets the force staff menu.
        /// </summary>
        public Menu ForceStaffMenu { get; set; }

        /// <summary>
        ///     Gets or sets the main menu.
        /// </summary>
        public Menu MainMenu { get; set; }

        /// <summary>
        ///     Gets or sets the suicide menu.
        /// </summary>
        public Menu SuicideMenu { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     The land mines_ on value changed.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="onValueChangeEventArgs">
        ///     The on value change event args.
        /// </param>
        private static void LandMines_OnValueChanged(object sender, OnValueChangeEventArgs onValueChangeEventArgs)
        {
            if (onValueChangeEventArgs.GetNewValue<bool>())
            {
                foreach (var landMine in from landMine in Variables.LandMines
                                         let effect = landMine.RangeDisplay
                                         where effect == null || effect.IsDestroyed
                                         select landMine)
                {
                    landMine.CreateRangeDisplay();
                }
            }
            else
            {
                foreach (var effect in
                    Variables.LandMines.Select(remoteMine => remoteMine.RangeDisplay)
                        .Where(effect => effect != null && !effect.IsDestroyed))
                {
                    effect.Dispose();
                }
            }
        }

        /// <summary>
        ///     The remote mines_ on value changed.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="onValueChangeEventArgs">
        ///     The on value change event args.
        /// </param>
        private static void RemoteMines_OnValueChanged(object sender, OnValueChangeEventArgs onValueChangeEventArgs)
        {
            if (onValueChangeEventArgs.GetNewValue<bool>())
            {
                foreach (var remoteMine in from remoteMine in Variables.RemoteMines
                                           let effect = remoteMine.RangeDisplay
                                           where effect == null || effect.IsDestroyed
                                           select remoteMine)
                {
                    remoteMine.CreateRangeDisplay();
                }
            }
            else
            {
                foreach (var effect in
                    Variables.RemoteMines.Select(remoteMine => remoteMine.RangeDisplay)
                        .Where(effect => effect != null && !effect.IsDestroyed))
                {
                    effect.Dispose();
                }
            }
        }

        /// <summary>
        ///     The stasis traps_ on value changed.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="onValueChangeEventArgs">
        ///     The on value change event args.
        /// </param>
        private static void StasisTraps_OnValueChanged(object sender, OnValueChangeEventArgs onValueChangeEventArgs)
        {
            if (onValueChangeEventArgs.GetNewValue<bool>())
            {
                foreach (var stasisTrap in from stasisTrap in Variables.StasisTraps
                                           let effect = stasisTrap.RangeDisplay
                                           where effect == null || effect.IsDestroyed
                                           select stasisTrap)
                {
                    stasisTrap.CreateRangeDisplay();
                }
            }
            else
            {
                foreach (var effect in
                    Variables.StasisTraps.Select(remoteMine => remoteMine.RangeDisplay)
                        .Where(effect => effect != null && !effect.IsDestroyed))
                {
                    effect.Dispose();
                }
            }
        }

        #endregion
    }
}