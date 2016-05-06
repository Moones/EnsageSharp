namespace Techies.Modules.ForceStaff
{
    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    using global::Techies.Utility;

    /// <summary>
    ///     The auto force staff.
    /// </summary>
    internal class AutoForceStaff : ITechiesModule
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The can be executed.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool CanBeExecuted()
        {
            var fs = Variables.ForceStaff;
            if (fs != null && fs.IsValid)
            {
                return Variables.Techies.IsAlive && Variables.Menu.ForceStaffMenu.Item("useForceStaff").GetValue<bool>()
                       && fs.CanBeCasted() && Utils.SleepCheck("Techies.ForceStaff");
            }

            if (Utils.SleepCheck("Techies.FindForceStaff"))
            {
                Variables.ForceStaff = Variables.Techies.FindItem("item_force_staff");
                Utils.Sleep(500, "Techies.FindForceStaff");
            }

            return false;
        }

        /// <summary>
        ///     The can draw.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool CanDraw()
        {
            return false;
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        public void Draw()
        {
        }

        /// <summary>
        ///     The execute.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool Execute(Hero hero)
        {
            var fs = Variables.ForceStaff;
            if (!fs.CanHit(hero) || !Utils.SleepCheck(hero.ClassID + "Techies.AutoDetonate"))
            {
                return false;
            }

            double rotSpeed;
            if (Prediction.RotSpeedDictionary.TryGetValue(hero.Handle, out rotSpeed) && rotSpeed > 0
                && Variables.Menu.ForceStaffMenu.Item("checkRotating").GetValue<bool>())
            {
                return false;
            }

            if (Prediction.StraightTime(hero) / 1000
                < Variables.Menu.ForceStaffMenu.Item("straightTime").GetValue<Slider>().Value)
            {
                return false;
            }

            var tempDamage = hero.GetStackDamage(610);
            if (tempDamage.Item1 >= hero.Health)
            {
                fs.UseAbility(hero);
                Utils.Sleep(250, "Techies.ForceStaff");
                return true;
            }

            return false;
        }

        /// <summary>
        ///     The is hero loop.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool IsHeroLoop()
        {
            return true;
        }

        /// <summary>
        ///     The on close.
        /// </summary>
        public void OnClose()
        {
        }

        /// <summary>
        ///     The on load.
        /// </summary>
        public void OnLoad()
        {
        }

        /// <summary>
        ///     The on wnd proc.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void OnWndProc(WndEventArgs args)
        {
        }

        #endregion
    }
}