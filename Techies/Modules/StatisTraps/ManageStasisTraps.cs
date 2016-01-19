namespace Techies.Modules.StatisTraps
{
    using System;
    using System.Linq;

    using Ensage;
    using Ensage.Common;

    using global::Techies.Classes;
    using global::Techies.Utility;

    /// <summary>
    ///     The manage statis traps.
    /// </summary>
    internal class ManageStasisTraps : ITechiesModule
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
            return false;
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
            ObjectMgr.OnAddEntity -= ObjectMgr_OnAddEntity;
            ObjectMgr.OnRemoveEntity -= ObjectMgr_OnRemoveEntity;
        }

        /// <summary>
        ///     The on load.
        /// </summary>
        public void OnLoad()
        {
            ObjectMgr.OnAddEntity += ObjectMgr_OnAddEntity;
            ObjectMgr.OnRemoveEntity += ObjectMgr_OnRemoveEntity;
            foreach (var bomb in
                ObjectMgr.GetEntities<Unit>()
                    .Where(
                        x =>
                        x.ClassID == ClassID.CDOTA_NPC_TechiesMines && x.Name == "npc_dota_techies_stasis_trap"
                        && x.IsAlive))
            {
                Variables.StasisTraps.Add(new StasisTrap(bomb));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The object manager on add entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void ObjectMgr_OnAddEntity(EntityEventArgs args)
        {
            var e = args.Entity as Unit;
            if (e == null || e.ClassID != ClassID.CDOTA_NPC_TechiesMines)
            {
                return;
            }

            DelayAction.Add(
                Math.Max(1500 - Game.Ping, 100), 
                delegate
                    {
                        if (e.Name != null && e.Name != "npc_dota_techies_stasis_trap")
                        {
                            return;
                        }

                        Variables.StasisTraps.Add(new StasisTrap(e));
                    });
        }

        /// <summary>
        ///     The object manager on remove entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void ObjectMgr_OnRemoveEntity(EntityEventArgs args)
        {
            var e = args.Entity as Unit;
            if (e == null || e.Name != "npc_dota_techies_stasis_trap")
            {
                return;
            }

            var statisTrapsList = Variables.StasisTraps;
            var mine = statisTrapsList.FirstOrDefault(x => x.Handle.Equals(e.Handle));
            if (mine == null)
            {
                return;
            }

            mine.Delete();
        }

        #endregion
    }
}