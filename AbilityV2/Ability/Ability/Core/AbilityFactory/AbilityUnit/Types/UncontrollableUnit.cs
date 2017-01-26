// <copyright file="UncontrollableUnit.cs" company="EnsageSharp">
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
    ///     The enemy.
    /// </summary>
    public abstract class UncontrollableUnit : AbilityUnit, IUncontrollableUnit
    {
        #region Constructors and Destructors

        protected UncontrollableUnit(Unit unit)
            : base(unit)
        {
            // foreach (var hero in Heroes.GetByTeam(Variables.EnemyTeam))
            // {
            // this.DamageDealtDictionary.Add(hero.Handle, 0);
            // }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The drawing_ on draw.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        public override void OnDraw()
        {
            // if (!this.SourceUnit.IsValid)
            // {
            // return;
            // }
            base.OnDraw();

            // if (this.Path == null || !this.Path.Any())
            // {
            // return;
            // }

            // var previous = this.Path[0];
            // Drawing.DrawLine(
            // Drawing.WorldToScreen(this.Position.Current.SetZ(this.SourceUnit.Position.Z)),
            // Drawing.WorldToScreen(previous.SetZ(this.SourceUnit.Position.Z)),
            // Color.Azure);
            // for (var i = 1; i < this.Path.Count; ++i)
            // {
            // var pos = this.Path[i];
            // Drawing.DrawLine(
            // Drawing.WorldToScreen(pos.SetZ(this.SourceUnit.Position.Z)),
            // Drawing.WorldToScreen(previous.SetZ(this.SourceUnit.Position.Z)),
            // Color.Azure);
            // previous = pos;
            // }
        }

        #endregion
    }
}