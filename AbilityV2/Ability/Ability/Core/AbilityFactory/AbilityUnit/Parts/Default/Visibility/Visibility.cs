// <copyright file="Visibility.cs" company="EnsageSharp">
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
namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Visibility
{
    using Ability.Core.AbilityFactory.Utilities;

    using Ensage;

    /// <summary>
    ///     The visibility.
    /// </summary>
    public class Visibility : IVisibility
    {
        #region Fields

        private float fogTime;

        private bool visible = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Visibility" /> class.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        public Visibility(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets the came out of fog notifier.</summary>
        public Notifier CameOutOfFogNotifier { get; } = new Notifier();

        /// <summary>
        ///     Gets the fog time.
        /// </summary>
        public float FogTime => Game.RawGameTime - this.fogTime;

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public IAbilityUnit Unit { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether visible.
        /// </summary>
        public bool Visible
        {
            get
            {
                return this.visible;
            }

            set
            {
                if (!this.visible && value)
                {
                    this.visible = true;
                    this.CameOutOfFogNotifier.Notify();
                }
                else if (!value && this.visible)
                {
                    this.visible = false;
                    this.fogTime = Game.RawGameTime;
                }
                else
                {
                    this.visible = value;
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        public virtual void Dispose()
        {
        }

        public virtual void Initialize()
        {
        }

        #endregion
    }
}