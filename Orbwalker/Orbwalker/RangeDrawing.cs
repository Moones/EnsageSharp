namespace Orbwalker
{
    using System;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using SharpDX;

    /// <summary>
    ///     The range drawing.
    /// </summary>
    internal class RangeDrawing
    {
        #region Fields

        /// <summary>
        ///     The last range.
        /// </summary>
        private float lastRange;

        /// <summary>
        ///     The me.
        /// </summary>
        private Hero me;

        /// <summary>
        ///     The range display.
        /// </summary>
        private ParticleEffect rangeDisplay;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RangeDrawing" /> class.
        /// </summary>
        public RangeDrawing()
        {
            Events.OnLoad += this.Events_OnLoad;
            this.me = ObjectManager.LocalHero;
            this.rangeDisplay = null;
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="RangeDrawing" /> class.
        /// </summary>
        ~RangeDrawing()
        {
            if (this.rangeDisplay == null)
            {
                return;
            }

            this.rangeDisplay.Dispose();
            this.rangeDisplay = null;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The create.
        /// </summary>
        public void Create()
        {
            this.rangeDisplay = this.me.AddParticleEffect(@"particles\ui_mouseactions\drag_selected_ring.vpcf");
            this.rangeDisplay.SetControlPoint(1, new Vector3(255, 80, 50));
            this.rangeDisplay.SetControlPoint(3, new Vector3(20, 0, 0));
            this.lastRange = this.GetAttackRange();
            this.rangeDisplay.SetControlPoint(2, new Vector3(this.lastRange, 255, 0));
        }

        /// <summary>
        ///     The dispose.
        /// </summary>
        public void Dispose()
        {
            if (this.IsDisposed() || this.rangeDisplay == null || this.rangeDisplay.IsDestroyed)
            {
                return;
            }

            this.rangeDisplay.Dispose();
            this.rangeDisplay = null;
        }

        /// <summary>
        ///     The get attack range.
        /// </summary>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public float GetAttackRange()
        {
            return this.me.GetAttackRange() + this.me.HullRadius + 70;
        }

        /// <summary>
        ///     The is disposed.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool IsDisposed()
        {
            try
            {
                return this.rangeDisplay == null || this.rangeDisplay.IsDestroyed;
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        ///     The is updated.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool IsUpdated()
        {
            return this.lastRange.Equals(this.GetAttackRange());
        }

        /// <summary>
        ///     The me.
        /// </summary>
        public void Me()
        {
            if (this.me == null || !this.me.IsValid)
            {
                this.me = ObjectManager.LocalHero;
            }
        }

        /// <summary>
        ///     The update.
        /// </summary>
        public void Update()
        {
            this.lastRange = this.GetAttackRange();
            this.Dispose();
            this.rangeDisplay = this.me.AddParticleEffect(@"particles\ui_mouseactions\drag_selected_ring.vpcf");
            this.rangeDisplay.SetControlPoint(1, new Vector3(255, 80, 50));
            this.rangeDisplay.SetControlPoint(3, new Vector3(15, 0, 0));
            this.rangeDisplay.SetControlPoint(2, new Vector3(this.lastRange, 255, 0));
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The events_ on load.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void Events_OnLoad(object sender, EventArgs e)
        {
            this.me = ObjectManager.LocalHero;
        }

        #endregion
    }
}