namespace Techies.Classes
{
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using global::Techies.Utility;

    using SharpDX;

    /// <summary>
    ///     The land mine.
    /// </summary>
    internal class LandMine
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LandMine" /> class.
        /// </summary>
        /// <param name="entity">
        ///     The land mine entity.
        /// </param>
        public LandMine(Entity entity)
        {
            this.Handle = entity.Handle;
            this.Position = entity.Position;
            this.Level = Variables.LandMinesAbility.Level;
            this.Radius = Variables.LandMinesAbility.GetAbilityData("small_radius");
            this.Entity = entity as Unit;
            this.Damage = Variables.Damage.CurrentLandMineDamage;

            DelayAction.Add(
                500, 
                () =>
                    {
                        if (Variables.Stacks != null
                            && !Variables.Stacks.Any(
                                x =>
                                (x.RemoteMines.Count > 0 || x.LandMines.Count > 0)
                                && x.Position.Distance(this.Position) < 200))
                        {
                            Variables.Stacks.Add(new Stack(this.Position));
                        }
                    });

            this.CreateRangeDisplay();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the damage.
        /// </summary>
        public float Damage { get; set; }

        /// <summary>
        ///     Gets or sets the entity.
        /// </summary>
        public Unit Entity { get; set; }

        /// <summary>
        ///     Gets or sets the handle.
        /// </summary>
        public float Handle { get; set; }

        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        public uint Level { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        ///     Gets or sets the radius.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        ///     Gets or sets the range display.
        /// </summary>
        public ParticleEffect RangeDisplay { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The create range display.
        /// </summary>
        public void CreateRangeDisplay()
        {
            this.RangeDisplay = this.Entity.AddParticleEffect(@"particles\ui_mouseactions\drag_selected_ring.vpcf");
            this.RangeDisplay.SetControlPoint(1, new Vector3(255, 80, 80));
            this.RangeDisplay.SetControlPoint(3, new Vector3(9, 0, 0));
            this.RangeDisplay.SetControlPoint(2, new Vector3(this.Radius + 30, 255, 0));
        }

        /// <summary>
        ///     The delete.
        /// </summary>
        public void Delete()
        {
            this.RangeDisplay.Dispose();
            Variables.LandMines.Remove(this);
        }

        /// <summary>
        ///     The distance.
        /// </summary>
        /// <param name="v">
        ///     The v.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public float Distance(Vector3 v)
        {
            return this.Position.Distance(v);
        }

        #endregion
    }
}