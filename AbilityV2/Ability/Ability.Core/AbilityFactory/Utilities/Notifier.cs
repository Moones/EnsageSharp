// <copyright file="Notifier.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    ///     The notifier.
    /// </summary>
    public class Notifier : IDisposable
    {
        private int count;

        #region Public Properties

        public Dictionary<int, Action> Reacters { get; set; }= new Dictionary<int, Action>();

        #endregion

        #region Public Methods and Operators

        public void Dispose()
        {
            this.Reacters.Clear();
        }

        public void Notify()
        {
            foreach (var reacter in this.Reacters)
            {
                reacter.Value.Invoke();
            }
        }

        public virtual int Subscribe(Action reacter)
        {
            this.count++;
            this.Reacters.Add(this.count, reacter);
            return this.count;
        }

        public void Unsubscribe(int id)
        {
            var temp = new Dictionary<int, Action>(this.Reacters);
            temp.Remove(id);
            this.Reacters = temp;
        }

        #endregion
    }
}