namespace Techies.Classes
{
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using global::Techies.Utility;

    using SharpDX;

    /// <summary>
    ///     The remote mine.
    /// </summary>
    internal class RemoteMine
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RemoteMine" /> class.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        public RemoteMine(Ability ability, Entity entity)
        {
            this.Handle = entity.Handle;
            this.Position = entity.Position;
            this.DetonateAbility = ability;
            this.Level = Variables.RemoteMinesAbility.Level;
            this.Radius = Variables.RemoteMinesAbility.GetAbilityData("radius");
            this.Entity = entity as Unit;
            this.Damage = Variables.Damage.CurrentRemoteMineDamage;
            if (Variables.Stacks != null && !Variables.Stacks.Any(x => x.Position.Distance(this.Position) < 350))
            {
                Variables.Stacks.Add(new Stack(this.Position));
            }

            this.CreateRangeDisplay();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the damage.
        /// </summary>
        public float Damage { get; set; }

        /// <summary>
        ///     Gets or sets the detonate ability.
        /// </summary>
        public Ability DetonateAbility { get; set; }

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
            this.RangeDisplay.SetControlPoint(1, new Vector3(80, 255, 50));
            this.RangeDisplay.SetControlPoint(3, new Vector3(20, 0, 0));
            this.RangeDisplay.SetControlPoint(2, new Vector3(this.Radius + 30, 255, 0));
        }

        /// <summary>
        ///     The delete.
        /// </summary>
        public void Delete()
        {
            this.RangeDisplay.Dispose();
            Variables.RemoteMines.Remove(this);
        }

        /// <summary>
        ///     The detonate.
        /// </summary>
        public void Detonate()
        {
            if (!Utils.SleepCheck("Techies.RemoteMine.Detonate." + this.Entity.Handle) || !this.Entity.IsAlive)
            {
                return;
            }

            this.DetonateAbility.UseAbility();
            this.Delete();
            Utils.Sleep(250, "Techies.RemoteMine.Detonate." + this.Entity.Handle);
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