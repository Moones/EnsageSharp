namespace Techies.Modules.SuicideSquadAttack
{
    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using global::Techies.Utility;

    /// <summary>
    ///     The auto suicide.
    /// </summary>
    internal class AutoSuicide : ITechiesModule
    {
        #region Fields

        /// <summary>
        ///     The suicide radius.
        /// </summary>
        public float SuicideRadius;

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

            if (Variables.Damage.GetSuicideDamage()[hero.ClassID] >= hero.Health)
            {
                var pos = hero.NetworkPosition;
                if (hero.NetworkActivity == NetworkActivity.Move)
                {
                    pos = Prediction.InFront(
                        hero, 
                        (float)((Game.Ping / 1000 + Variables.Techies.GetTurnTime(hero)) * hero.MovementSpeed));
                }

                if (pos.Distance2D(Variables.Techies) < hero.Distance2D(Variables.Techies))
                {
                    pos = hero.Position;
                }

                if (!(pos.Distance2D(Variables.Techies) < this.SuicideRadius))
                {
                    return false;
                }

                if (Variables.Techies.Distance2D(pos) > 100)
                {
                    pos = (pos - Variables.Techies.Position) * 99 / pos.Distance2D(Variables.Techies)
                          + Variables.Techies.Position;
                }

                Variables.SuicideAbility.UseAbility(pos);
                Utils.Sleep(500, "Techies.Suicide");
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
            Variables.SuicideAbility = Variables.Techies.Spellbook.SpellE;
            this.SuicideRadius = Variables.SuicideAbility.GetAbilityData("small_radius");
        }

        #endregion
    }
}