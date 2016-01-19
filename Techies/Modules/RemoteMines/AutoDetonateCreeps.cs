namespace Techies.Modules.RemoteMines
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using global::Techies.Classes;
    using global::Techies.Utility;

    /// <summary>
    ///     The auto detonate creeps.
    /// </summary>
    internal class AutoDetonateCreeps : ITechiesModule
    {
        #region Fields

        /// <summary>
        ///     The creeps.
        /// </summary>
        private List<Creep> creeps;

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
            return Variables.Menu.DetonationMenu.Item("autoDetonateCreeps").GetValue<bool>();
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
        public bool Execute(Hero hero = null)
        {
            if (Utils.SleepCheck("Techies.GetCreeps") || this.creeps == null)
            {
                this.creeps =
                    ObjectMgr.GetEntities<Creep>()
                        .Where(
                            x =>
                            (x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Lane
                             || x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Siege) && x.IsAlive && x.IsVisible
                            && x.IsSpawned && x.Team == Variables.EnemyTeam)
                        .ToList();
                Utils.Sleep(500, "Techies.GetCreeps");
            }

            foreach (var creep in
                this.creeps.Where(x => Utils.SleepCheck(x.Handle + "Techies.AutoDetonate")))
            {
                var tempDamage = creep.GetStackDamage();
                if (tempDamage.Item1 >= creep.Health)
                {
                    var creep1 = creep;
                    var count =
                        (from creep2 in
                             this.creeps.Where(
                                 x =>
                                 !x.Equals(creep1) && Utils.SleepCheck(x.Handle + "Techies.AutoDetonate")
                                 && x.Distance2D(creep1) < 500)
                         let tempDamage2 = creep2.GetStackDamage()
                         where tempDamage2.Item1 >= creep2.Health
                         select creep2).Count();
                    if (count < 3)
                    {
                        return false;
                    }

                    Detonate(tempDamage.Item2);
                    Utils.Sleep(500, creep.Handle + "Techies.AutoDetonate");
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
            return false;
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