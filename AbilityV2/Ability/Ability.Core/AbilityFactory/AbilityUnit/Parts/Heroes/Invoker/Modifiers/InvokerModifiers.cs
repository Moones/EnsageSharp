// <copyright file="InvokerModifiers.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.Modifiers
{
    using System.Collections.Generic;
    using System.Linq;

    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Modifiers;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Heroes.Invoker.Modifiers.Orbs;

    using Ensage;

    /// <summary>
    ///     The invoker modifiers.
    /// </summary>
    public class InvokerModifiers : Modifiers
    {
        #region Fields

        private readonly Exort exort = new Exort();

        private readonly Quas quas = new Quas();

        private readonly Wex wex = new Wex();

        #endregion

        #region Constructors and Destructors

        internal InvokerModifiers(IAbilityUnit unit)
            : base(unit)
        {
            this.OrbsUpdate = new OrbsUpdate(this);
            foreach (var modifier in unit.SourceUnit.Modifiers)
            {
                if (modifier.Name == this.quas.Name)
                {
                    this.CurrentOrbs.Add(this.quas);
                    this.QuasCount++;
                }
                else if (modifier.Name == this.wex.Name)
                {
                    this.CurrentOrbs.Add(this.wex);
                    this.WexCount++;
                }
                else if (modifier.Name == this.exort.Name)
                {
                    this.CurrentOrbs.Add(this.exort);
                    this.ExortCount++;
                }
            }
        }

        #endregion

        #region Public Properties

        public List<IOrb> CurrentOrbs { get; set; } = new List<IOrb>(2);

        public uint ExortCount { get; set; }

        public OrbsUpdate OrbsUpdate { get; set; }

        public uint QuasCount { get; set; }

        public uint WexCount { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The new modifier received.
        /// </summary>
        /// <param name="modifier">
        ///     The modifier.
        /// </param>
        public override void AddModifier(Modifier modifier)
        {
            if (modifier.Name == this.quas.Name)
            {
                if (this.CurrentOrbs.Count >= 3)
                {
                    this.RemoveOrb(this.CurrentOrbs.First());
                }

                this.CurrentOrbs.Add(this.quas);
                this.QuasCount++;
            }
            else if (modifier.Name == this.wex.Name)
            {
                if (this.CurrentOrbs.Count >= 3)
                {
                    this.RemoveOrb(this.CurrentOrbs.First());
                }

                this.CurrentOrbs.Add(this.wex);
                this.WexCount++;
            }
            else if (modifier.Name == this.exort.Name)
            {
                if (this.CurrentOrbs.Count >= 3)
                {
                    this.RemoveOrb(this.CurrentOrbs.First());
                }

                this.CurrentOrbs.Add(this.exort);
                this.ExortCount++;
            }

            if (this.QuasCount + this.WexCount + this.ExortCount >= 3)
            {
                this.OrbsUpdate.Update();
            }

            base.AddModifier(modifier);
        }

        #endregion

        #region Methods

        private void RemoveOrb(IOrb orb)
        {
            if (orb is Quas)
            {
                this.QuasCount--;
            }
            else if (orb is Wex)
            {
                this.WexCount--;
            }
            else
            {
                this.ExortCount--;
            }

            this.CurrentOrbs.Remove(orb);
        }

        #endregion
    }
}