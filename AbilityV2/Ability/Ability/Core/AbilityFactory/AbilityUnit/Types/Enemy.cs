﻿// <copyright file="Enemy.cs" company="EnsageSharp">
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
    public class Enemy : UncontrollableUnit
    {
        #region Constructors and Destructors

        public Enemy(Unit unit)
            : base(unit)
        {
            this.IsEnemy = true;

            // foreach (var hero in Heroes.GetByTeam(Variables.EnemyTeam))
            // {
            // this.DamageDealtDictionary.Add(hero.Handle, 0);
            // }
        }

        #endregion
    }
}