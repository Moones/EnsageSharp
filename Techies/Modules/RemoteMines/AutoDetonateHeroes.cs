namespace Techies.Modules.RemoteMines
{
    using System.Collections.Generic;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

    using global::Techies.Classes;
    using global::Techies.Utility;

    using SharpDX;

    /// <summary>
    ///     The auto detonate heroes.
    /// </summary>
    internal class AutoDetonateHeroes : ITechiesModule
    {
        #region Fields

        /// <summary>
        ///     The notification.
        /// </summary>
        private Notification notification;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The can be executed.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool CanBeExecuted()
        {
            return Variables.Menu.DetonationMenu.Item("autoDetonate").GetValue<bool>();
        }

        /// <summary>
        ///     The can draw.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool CanDraw()
        {
            return true;
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        public void Draw()
        {
            if (this.notification != null)
            {
                this.notification.Draw();
            }
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
            if ((Variables.Instance.Techies.EnabledHeroes.ContainsKey(hero.ClassID)
                 && !Variables.Instance.Techies.EnabledHeroes[hero.ClassID]) || hero.IsInvul() || hero.IsMagicImmune()
                || hero.HasModifiers(
                    new[]
                        {
                            "modifier_juggernaut_blade_fury", 
                            "modifier_ember_spirit_sleight_of_fist_caster_invulnerability"
                        }, 
                    false) || !hero.CanDie())
            {
                return false;
            }

            if (!Utils.SleepCheck(hero.ClassID + "Techies.AutoDetonate"))
            {
                return false;
            }

            var tempDamage = hero.GetStackDamage();
            if (!(tempDamage.Item1 >= hero.Health))
            {
                return false;
            }

            if (tempDamage.Item3 != null && tempDamage.Item3.AutoDetonate)
            {
                Detonate(tempDamage.Item2);
                Utils.Sleep(500, hero.ClassID + "Techies.AutoDetonate");
                return true;
            }

            if (tempDamage.Item3 == null || tempDamage.Item3.AutoDetonate
                || !Variables.Menu.DrawingsMenu.Item("Techies.ShowNotification").GetValue<bool>())
            {
                return false;
            }

            if (!Utils.SleepCheck("Techies.Notification." + hero.StoredName()))
            {
                return false;
            }

            Utils.Sleep(15000, "Techies.Notification." + hero.StoredName());
            if (this.notification == null)
            {
                this.notification = new Notification(
                    5000, 
                    new Vector2(HUDInfo.ScreenSizeX(), (float)(HUDInfo.ScreenSizeY() / 2.3)), 
                    new Vector2(HUDInfo.ScreenSizeX() / 18, HUDInfo.ScreenSizeX() / 30));
            }

            this.notification.RemoteMines = tempDamage.Item2;
            this.notification.PopUp(hero);
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
            if (args.Msg == (ulong)Utils.WindowsMessages.WM_LBUTTONDOWN && this.notification != null)
            {
                this.notification.Click(Game.MouseScreenPosition);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The detonate.
        /// </summary>
        /// <param name="remoteMines">
        ///     The remote mines.
        /// </param>
        private static void Detonate(IEnumerable<RemoteMine> remoteMines)
        {
            foreach (var remoteMine in remoteMines)
            {
                remoteMine.Detonate();
            }
        }

        #endregion
    }
}