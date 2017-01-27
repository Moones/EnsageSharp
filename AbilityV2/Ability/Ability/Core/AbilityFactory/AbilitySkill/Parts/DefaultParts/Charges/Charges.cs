// <copyright file="Charges.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilitySkill.Parts.DefaultParts.Charges
{
    using Ability.Core.AbilityFactory.Utilities;

    public class Charges : ICharges
    {
        #region Fields

        private uint primary;

        private uint secondary;

        #endregion

        #region Constructors and Destructors

        public Charges(IAbilitySkill skill)
        {
            this.Skill = skill;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether has secondary.
        /// </summary>
        public bool HasSecondary { get; set; }

        /// <summary>
        ///     Gets or sets the primary.
        /// </summary>
        public uint Primary
        {
            get
            {
                return this.primary;
            }

            set
            {
                if (this.primary != value)
                {
                    this.PrimaryProvider.Next(value);
                }

                this.primary = value;
            }
        }

        /// <summary>
        ///     Gets the primary provider.
        /// </summary>
        public DataProvider<uint> PrimaryProvider { get; } = new DataProvider<uint>();

        /// <summary>
        ///     Gets or sets the secondary.
        /// </summary>
        public uint Secondary
        {
            get
            {
                return this.secondary;
            }

            set
            {
                if (value > 0)
                {
                    this.HasSecondary = true;
                }

                if (this.secondary != value)
                {
                    this.SecondaryProvider.Next(value);
                }

                this.secondary = value;
            }
        }

        /// <summary>
        ///     Gets the secondary provider.
        /// </summary>
        public DataProvider<uint> SecondaryProvider { get; } = new DataProvider<uint>();

        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        public IAbilitySkill Skill { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
        }

        public virtual void Initialize()
        {
            this.Skill.DataReceiver.Updates.Add(
                () =>
                    {
                        this.Primary = this.Skill.SourceItem.CurrentCharges;
                        this.Secondary = this.Skill.SourceItem.SecondaryCharges;
                    });
        }

        #endregion
    }
}