// <copyright file="ControllableUnit.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Types
{
    using Ensage;

    /// <summary>
    ///     The ability unit.
    /// </summary>
    public abstract class ControllableUnit : AbilityUnit, IControllableUnit
    {
        #region Constructors and Destructors

        protected ControllableUnit(Unit unit)
            : base(unit)
        {
            this.IsEnemy = false;
        }

        #endregion

        #region Public Methods and Operators

        public override void OnDraw()
        {
            // if (!this.SourceUnit.IsValid)
            // {
            // return;
            // }
            base.OnDraw();

            // if (this.Path != null && this.Path.Any())
            // {
            // var previous = this.Path[0];
            // Drawing.DrawLine(
            // Drawing.WorldToScreen(this.Position.Current.SetZ(this.SourceUnit.Position.Z)), 
            // Drawing.WorldToScreen(previous.SetZ(this.SourceUnit.Position.Z)), 
            // Color.Azure);
            // for (var i = 1; i < Math.Min(this.Path.Count, 3); ++i)
            // {
            // var pos = this.Path[i];
            // Drawing.DrawLine(
            // Drawing.WorldToScreen(pos.SetZ(this.SourceUnit.Position.Z)), 
            // Drawing.WorldToScreen(previous.SetZ(this.SourceUnit.Position.Z)), 
            // Color.Azure);
            // previous = pos;
            // }
            // }

            // var heroPos = this.SourceUnit.Position;
            // var heroScreenpos =
            // Drawing.WorldToScreen(
            // new Vector3(
            // this.SourceUnit.Position.X, 
            // this.SourceUnit.Position.Y, 
            // this.SourceUnit.Position.Z + this.SourceUnit.HealthBarOffset)) + new Vector2(-60, -200);

            //// Drawing the navmesh grid
            // const int CellCount = 25;
            // for (var i = 0; i < CellCount; ++i)
            // {
            // for (var j = 0; j < CellCount; ++j)
            // {
            // Vector2 p;
            // p.X = this.Pathfinding.CellSize * (i - CellCount / 2) + heroPos.X;
            // p.Y = this.Pathfinding.CellSize * (j - CellCount / 2) + heroPos.Y;

            // Color c;
            // if (i == CellCount / 2 && j == CellCount / 2) c = Color.Blue - new Color(0, 0, 0, 70);
            // else
            // {
            // var isFlying = this.SourceUnit.MoveCapability == MoveCapability.Fly
            // || this.SourceUnit.IsUnitState(UnitState.Flying);
            // var flag = this.Pathfinding.GetCellFlags(p);
            // if (!isFlying && flag.HasFlag(NavMeshCellFlags.Walkable))
            // {
            // c = flag.HasFlag(NavMeshCellFlags.Tree)
            // ? Color.Purple - new Color(0, 0, 0, 70)
            // : Color.Green - new Color(0, 0, 0, 70);
            // if (flag.HasFlag(NavMeshCellFlags.GridFlagObstacle)) c = Color.BlanchedAlmond - new Color(0, 0, 0, 70);
            // }
            // else if (isFlying && !flag.HasFlag(NavMeshCellFlags.MovementBlocker)) c = Color.Green - new Color(0, 0, 0, 70);
            // else c = Color.Red - new Color(0, 0, 0, 70);
            // }

            // Drawing.DrawRect(
            // heroScreenpos + new Vector2(i * 5, 30 + (CellCount - j - 1) * 5), 
            // new Vector2(5, 5), 
            // c, 
            // false);
            // }
            // }
        }

        #endregion
    }
}