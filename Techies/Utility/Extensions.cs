namespace Techies.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using global::Techies.Classes;

    /// <summary>
    ///     The extensions.
    /// </summary>
    internal static class Extensions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The get stack damage.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <param name="inFrontDistance">
        ///     The in front distance.
        /// </param>
        /// <returns>
        ///     The <see cref="Tuple" />.
        /// </returns>
        public static Tuple<float, IEnumerable<RemoteMine>> GetStackDamage(this Unit hero, float inFrontDistance = 0)
        {
            var detonatableMines = new List<RemoteMine>();
            if (Variables.Stacks == null || !Variables.Stacks.Any())
            {
                return new Tuple<float, IEnumerable<RemoteMine>>(0, detonatableMines);
            }

            var rotSpeed = Prediction.RotSpeedDictionary.ContainsKey(hero.Handle)
                               ? Prediction.RotSpeedDictionary[hero.Handle]
                               : 0;
            var heroPosition = inFrontDistance > 0
                                   ? hero.Position
                                     + VectorExtensions.FromPolarCoordinates(
                                         1f, 
                                         (float)(hero.NetworkRotationRad + rotSpeed)).ToVector3() * inFrontDistance
                                   : hero.Position;
            if (hero.NetworkActivity == NetworkActivity.Move)
            {
                heroPosition +=
                    VectorExtensions.FromPolarCoordinates(1f, (float)(hero.NetworkRotationRad + rotSpeed)).ToVector3()
                    * (float)(Variables.Techies.GetTurnTime(hero) + Game.Ping / 1000) * hero.MovementSpeed;
            }

            var tempDamage = 0f;
            var nearestStack = Variables.Stacks.MinOrDefault(x => x.Position.Distance(heroPosition));
            if (nearestStack == null || nearestStack.Position.Distance(heroPosition) > 1000)
            {
                return new Tuple<float, IEnumerable<RemoteMine>>(0, detonatableMines);
            }

            foreach (var landMine in nearestStack.LandMines.Where(x => x.Distance(heroPosition) <= x.Radius))
            {
                if (tempDamage >= hero.Health)
                {
                    return new Tuple<float, IEnumerable<RemoteMine>>(tempDamage, detonatableMines);
                }

                tempDamage += Variables.Damage.GetLandMineDamage(landMine.Level, hero.ClassID);
            }

            foreach (var remoteMine in nearestStack.RemoteMines.Where(x => x.Distance(heroPosition) <= x.Radius))
            {
                if (tempDamage >= hero.Health)
                {
                    break;
                }

                detonatableMines.Add(remoteMine);
                tempDamage += Variables.Damage.GetRemoteMineDamage(remoteMine.Level, hero.ClassID, hero);
            }

            return new Tuple<float, IEnumerable<RemoteMine>>(tempDamage, detonatableMines);
        }

        #endregion
    }
}