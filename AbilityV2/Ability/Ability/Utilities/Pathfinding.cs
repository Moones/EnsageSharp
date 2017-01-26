// <copyright file="Pathfinding.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Utilities
{
    using System.Collections.ObjectModel;

    using Ability.Core.AbilityFactory.AbilityUnit;

    using Ensage;
    using Ensage.Common.Extensions.SharpDX;

    using SharpDX;

    /// <summary>
    ///     The pathfinding.
    /// </summary>
    public static class Pathfinding
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The get positions with distance and angle.
        /// </summary>
        /// <param name="startPosition">
        ///     The start position.
        /// </param>
        /// <param name="direction">
        ///     The direction.
        /// </param>
        /// <param name="cellSize">
        ///     The cell size.
        /// </param>
        /// <param name="distance">
        ///     The distance.
        /// </param>
        /// <returns>
        ///     The <see cref="Collection" />.
        /// </returns>
        public static Collection<Vector3> GetPositionsWithDistanceAndAngle(
            Vector3 startPosition,
            Vector3 direction,
            float cellSize,
            int distance)
        {
            var result = new Collection<Vector3>();

            for (var ang = 0; ang < 360; ang += 90)
            {
                var currentDirection = direction.Rotated(MathUtil.DegreesToRadians(ang));
                for (var i = 0; i <= distance; ++i)
                {
                    var currentTarget = startPosition + currentDirection * (i * cellSize);
                    result.Add(currentTarget);
                }
            }

            return result;
        }

        /// <summary>
        ///     The get valid move position.
        /// </summary>
        /// <param name="currentTarget">
        ///     The current target.
        /// </param>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="pathfinding">
        ///     The pathfinding.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector3" />.
        /// </returns>
        public static Vector3 GetValidMovePosition(
            Vector3 currentTarget,
            IAbilityUnit unit,
            NavMeshPathfinding pathfinding)
        {
            var dir = currentTarget - unit.SourceUnit.Position;
            dir.Normalize();

            // try to walk further if position is not valid
            foreach (var possibleTarget in GetPositionsWithDistanceAndAngle(currentTarget, dir, pathfinding.CellSize, 4)
            )
            {
                var flags = pathfinding.GetCellFlags(possibleTarget);
                if (IsWalkableCell(flags))
                {
                    return possibleTarget;
                }
            }

            // var perpendicular = dir.Perpendicular();
            // perpendicular.Normalize();
            // var newTarget = unit.SourceUnit.Position + (perpendicular * 50);
            // foreach (var possibleTarget in GetPositionsWithDistanceAndAngle(newTarget, perpendicular, pathfinding.CellSize, 4))
            // {
            // var flags = pathfinding.GetCellFlags(possibleTarget);
            // if (IsWalkableCell(flags))
            // {
            // return possibleTarget;
            // }
            // }

            // newTarget = unit.SourceUnit.Position + (perpendicular * -50);
            // foreach (var possibleTarget in GetPositionsWithDistanceAndAngle(newTarget, perpendicular, pathfinding.CellSize, 4))
            // {
            // var flags = pathfinding.GetCellFlags(possibleTarget);
            // if (IsWalkableCell(flags))
            // {
            // return possibleTarget;
            // }
            // }
            var newTarget = unit.SourceUnit.Position + dir * -50;

            // no valid position found for our old movement target, so try to find a new one
            foreach (var possibleTarget in GetPositionsWithDistanceAndAngle(newTarget, dir, pathfinding.CellSize, 4))
            {
                var flags = pathfinding.GetCellFlags(possibleTarget);
                if (IsWalkableCell(flags))
                {
                    return possibleTarget;
                }
            }

            // well no luck?
            return newTarget;
        }

        /// <summary>
        ///     The is walkable cell.
        /// </summary>
        /// <param name="flags">
        ///     The flags.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsWalkableCell(NavMeshCellFlags flags)
        {
            return !flags.HasFlag(NavMeshCellFlags.GridFlagObstacle) && flags.HasFlag(NavMeshCellFlags.Walkable)
                   && !flags.HasFlag(NavMeshCellFlags.Tree);
        }

        #endregion
    }
}