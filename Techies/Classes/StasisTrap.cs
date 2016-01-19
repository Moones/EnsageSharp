namespace Techies.Classes
{
    using Ensage;
    using Ensage.Common.Extensions;

    using global::Techies.Utility;

    using SharpDX;

    /// <summary>
    ///     The stasis trap.
    /// </summary>
    internal class StasisTrap
    {
        #region Fields

        /// <summary>
        ///     The entity.
        /// </summary>
        public Unit Entity;

        /// <summary>
        ///     The handle.
        /// </summary>
        public float Handle;

        /// <summary>
        ///     The position.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        ///     The radius.
        /// </summary>
        public float Radius;

        /// <summary>
        ///     The range display.
        /// </summary>
        public ParticleEffect RangeDisplay;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StasisTrap" /> class.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        public StasisTrap(Entity entity)
        {
            this.Handle = entity.Handle;

            this.Position = entity.Position;

            this.Radius = Variables.StasisTrapAbility.GetAbilityData("activation_radius");
            this.Entity = entity as Unit;

            this.CreateRangeDisplay();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The create range display.
        /// </summary>
        public void CreateRangeDisplay()
        {
            this.RangeDisplay = this.Entity.AddParticleEffect(@"particles\ui_mouseactions\drag_selected_ring.vpcf");
            this.RangeDisplay.SetControlPoint(1, new Vector3(80, 100, 255));
            this.RangeDisplay.SetControlPoint(3, new Vector3(20, 0, 0));
            this.RangeDisplay.SetControlPoint(2, new Vector3(this.Radius + 30, 255, 0));
        }

        /// <summary>
        ///     The delete.
        /// </summary>
        public void Delete()
        {
            this.RangeDisplay.Dispose();
            Variables.StasisTraps.Remove(this);
        }

        #endregion
    }
}