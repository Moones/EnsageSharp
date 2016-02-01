namespace Techies.Modules.RemoteMines
{
    using System.Linq;

    using Ensage;
    using Ensage.Common.Extensions;

    using global::Techies.Utility;

    /// <summary>
    ///     The protection.
    /// </summary>
    internal class AutoProtection : ITechiesModule
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
            return Variables.Menu.DetonationMenu.Item("autoDetonateProtection").GetValue<bool>();
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
            if (hero == null)
            {
                return false;
            }

            var item = hero.FindItem("item_tango_single")
                       ?? hero.FindItem("item_tango") ?? hero.FindItem("item_quelling_blade");
            var nearestStack = Variables.Stacks.MinOrDefault(x => x.Position.Distance(hero.Position));
            if (nearestStack == null || nearestStack.Position.Distance(hero.Position) > 1000)
            {
                return false;
            }

            foreach (var mine in
                nearestStack.RemoteMines.Where(
                    x => x.Entity.IsValid && x.Entity.IsAlive && x.Entity.IsVisible && x.Entity.IsVisibleToEnemies))
            {
                if (item != null && item.CanHit(mine.Entity) && hero.GetTurnTime(mine.Position) < 0.01)
                {
                    mine.Detonate();
                    return true;
                }

                if (!hero.IsAttacking() || !(hero.GetTurnTime(mine.Position) < 0.01)
                    || !(mine.Entity.DamageTaken(hero.MaximumDamage, DamageType.Physical, hero) >= mine.Entity.Health))
                {
                    continue;
                }

                mine.Detonate();
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

        #endregion
    }
}