namespace Techies.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

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
                                     + (VectorExtensions.FromPolarCoordinates(
                                         1f, 
                                         (float)(hero.NetworkRotationRad + rotSpeed)).ToVector3() * inFrontDistance)
                                   : hero.Position;
            var prediction = heroPosition;
            if (hero.NetworkActivity == NetworkActivity.Move)
            {
                prediction +=
                    VectorExtensions.FromPolarCoordinates(1f, (float)(hero.NetworkRotationRad + rotSpeed)).ToVector3()
                    * ((float)(Variables.Techies.GetTurnTime(hero) + (Game.Ping / 1000)) * hero.MovementSpeed);
            }

            var tempDamage = 0f;
            var nearestStack =
                Variables.Stacks.Where(x => x.AutoDetonate).MinOrDefault(x => x.Position.Distance(heroPosition));
            if (nearestStack == null || nearestStack.Position.Distance(heroPosition) > 1000)
            {
                return new Tuple<float, IEnumerable<RemoteMine>>(0, detonatableMines);
            }

            foreach (var landMine in
                nearestStack.LandMines.Where(
                    x => x.Distance(heroPosition) <= x.Radius && x.Distance(prediction) <= x.Radius))
            {
                if (tempDamage >= hero.Health)
                {
                    return new Tuple<float, IEnumerable<RemoteMine>>(tempDamage, detonatableMines);
                }

                tempDamage += Variables.Damage.GetLandMineDamage(landMine.Level, hero.ClassID);
            }

            foreach (var remoteMine in
                nearestStack.RemoteMines.Where(
                    x => x.Distance(heroPosition) <= x.Radius && x.Distance(prediction) <= x.Radius))
            {
                if (tempDamage >= hero.Health)
                {
                    if (hero is Hero && nearestStack.MinEnemiesKill > 1)
                    {
                        var count =
                            (from hero2 in
                                 Heroes.GetByTeam(Variables.EnemyTeam)
                                 .Where(
                                     x =>
                                     x.IsAlive && x.IsVisible && !x.Equals(hero)
                                     && Utils.SleepCheck(x.ClassID + "Techies.AutoDetonate")
                                     && x.Distance2D(nearestStack.Position) < 420)
                             let tempDamage2 =
                                 detonatableMines.Sum(
                                     x => Variables.Damage.GetRemoteMineDamage(x.Level, hero2.ClassID, hero2))
                             where tempDamage2 >= hero2.Health
                             select hero2).Count();
                        if (count + 1 >= nearestStack.MinEnemiesKill)
                        {
                            return new Tuple<float, IEnumerable<RemoteMine>>(tempDamage, detonatableMines);
                        }

                        detonatableMines.Add(remoteMine);
                        tempDamage += Variables.Damage.GetRemoteMineDamage(remoteMine.Level, hero.ClassID, hero);
                        continue;
                    }

                    return new Tuple<float, IEnumerable<RemoteMine>>(tempDamage, detonatableMines);
                }

                detonatableMines.Add(remoteMine);
                tempDamage += Variables.Damage.GetRemoteMineDamage(remoteMine.Level, hero.ClassID, hero);
            }

            if (tempDamage >= hero.Health)
            {
                if (hero is Hero && nearestStack.MinEnemiesKill > 1)
                {
                    var count =
                        (from hero2 in
                             Heroes.GetByTeam(Variables.EnemyTeam)
                             .Where(
                                 x =>
                                 x.IsAlive && x.IsVisible && !x.Equals(hero)
                                 && Utils.SleepCheck(x.ClassID + "Techies.AutoDetonate")
                                 && x.Distance2D(nearestStack.Position) < 420)
                         let tempDamage2 =
                             detonatableMines.Sum(
                                 x => Variables.Damage.GetRemoteMineDamage(x.Level, hero2.ClassID, hero2))
                         where tempDamage2 >= hero2.Health
                         select hero2).Count();
                    if (count + 1 >= nearestStack.MinEnemiesKill)
                    {
                        return new Tuple<float, IEnumerable<RemoteMine>>(tempDamage, detonatableMines);
                    }

                    return new Tuple<float, IEnumerable<RemoteMine>>(0, detonatableMines);
                }

                return new Tuple<float, IEnumerable<RemoteMine>>(tempDamage, detonatableMines);
            }

            return new Tuple<float, IEnumerable<RemoteMine>>(0, detonatableMines);
        }

        #endregion
    }
}