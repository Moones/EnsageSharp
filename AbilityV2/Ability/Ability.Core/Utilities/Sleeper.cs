using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.Utilities
{
    using Ensage;
    using Ensage.Common;

    /// <summary>
    ///     The sleeper.
    /// </summary>
    public class Sleeper
    {
        #region Fields

        /// <summary>
        ///     The last sleep tick count.
        /// </summary>
        private float lastSleepTickCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Sleeper" /> class.
        /// </summary>
        public Sleeper()
        {
            this.lastSleepTickCount = 0;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether sleeping.
        /// </summary>
        public bool Sleeping => Game.RawGameTime < this.lastSleepTickCount;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The sleep.
        /// </summary>
        /// <param name="duration">
        ///     The duration.
        /// </param>
        public void Sleep(float duration)
        {
            this.lastSleepTickCount = Game.RawGameTime + duration / 1000;
        }

        #endregion
    }
}
