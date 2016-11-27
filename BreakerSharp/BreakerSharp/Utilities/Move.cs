namespace BreakerSharp.Utilities
{
    using Ensage;

    using SharpDX;

    /// <summary>
    ///     The move.
    /// </summary>
    public class Move
    {
        #region Fields

        /// <summary>
        ///     The sleeper.
        /// </summary>
        private readonly Sleeper sleeper;

        /// <summary>
        ///     The unit.
        /// </summary>
        private readonly Unit unit;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Move" /> class.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        public Move(Unit unit)
        {
            this.sleeper = new Sleeper();
            this.unit = unit;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The pause.
        /// </summary>
        /// <param name="duration">
        ///     The duration.
        /// </param>
        public void Pause(float duration)
        {
            this.sleeper.Sleep(duration);
        }

        /// <summary>
        ///     The to position.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        public void ToPosition(Vector3 position)
        {
            this.Execute(position);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The execute.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        private void Execute(Vector3 position)
        {
            if (this.sleeper.Sleeping)
            {
                return;
            }

            this.unit.Move(position);
            this.sleeper.Sleep(200);
        }

        #endregion
    }
}