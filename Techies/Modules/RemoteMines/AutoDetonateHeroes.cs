namespace Techies.Modules.RemoteMines
{
    using System.Collections.Generic;

    using Ensage;
    using Ensage.Common;

    using global::Techies.Classes;
    using global::Techies.Utility;

    /// <summary>
    ///     The auto detonate heroes.
    /// </summary>
    internal class AutoDetonateHeroes : ITechiesModule
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
            return Variables.Menu.DetonationMenu.Item("autoDetonate").GetValue<bool>();
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
            if (Variables.Instance.Techies.EnabledHeroes.ContainsKey(hero.ClassID)
                && !Variables.Instance.Techies.EnabledHeroes[hero.ClassID])
            {
                return false;
            }

            if (Utils.SleepCheck(hero.ClassID + "Techies.AutoDetonate"))
            {
                var tempDamage = hero.GetStackDamage();
                if (tempDamage.Item1 >= hero.Health)
                {
                    Detonate(tempDamage.Item2);
                    Utils.Sleep(500, hero.ClassID + "Techies.AutoDetonate");
                    return true;
                }
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