namespace Techies.Modules.SuicideSquadAttack
{
    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Extensions.SharpDX;

    using global::Techies.Utility;

    /// <summary>
    ///     The auto suicide.
    /// </summary>
    internal class AutoSuicide : ITechiesModule
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the suicide radius.
        /// </summary>
        public float SuicideRadius { get; set; }

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
            return Variables.Techies.IsAlive && Variables.Menu.SuicideMenu.Item("autoSuicide").GetValue<bool>()
                   && Variables.SuicideAbility.CanBeCasted() && Utils.SleepCheck("Techies.Suicide");
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
            if (!Variables.Damage.GetSuicideDamage().ContainsKey(hero.ClassID))
            {
                return false;
            }

            if (!Utils.SleepCheck(hero.ClassID + "Techies.AutoDetonate"))
            {
                return false;
            }

            if (!(Variables.Damage.GetSuicideDamage()[hero.ClassID] >= hero.Health))
            {
                return false;
            }

            var pos = hero.NetworkPosition;
            if (hero.NetworkActivity == NetworkActivity.Move)
            {
                pos = Prediction.InFront(
                    hero, 
                    (float)(((Game.Ping / 1000) + Variables.Techies.GetTurnTime(hero)) * hero.MovementSpeed));
            }

            if (pos.Distance2D(Variables.Techies) < hero.Distance2D(Variables.Techies))
            {
                pos = Prediction.InFront(hero, Game.Ping / 1000);
            }

            if (
                !(pos.Distance2D(Variables.Techies)
                  < this.SuicideRadius + hero.HullRadius + Variables.Techies.HullRadius))
            {
                return false;
            }

            if (Variables.Techies.Distance2D(pos) > 100)
            {
                pos = Variables.Techies.Position.Extend(pos, 99);
            }

            Variables.SuicideAbility.UseAbility(pos);
            Utils.Sleep(500, "Techies.Suicide");
            return true;
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
            Variables.SuicideAbility = Variables.Techies.Spellbook.SpellE;
            this.SuicideRadius = Variables.SuicideAbility.GetAbilityData("small_radius");
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