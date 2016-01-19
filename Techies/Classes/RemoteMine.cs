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
        #region Fields

        /// <summary>
        ///     The detonate ability.
        /// </summary>
        public Ability DetonateAbility;

        /// <summary>
        ///     The entity.
        /// </summary>
        public Unit Entity;

        /// <summary>
        ///     The handle.
        /// </summary>
        public float Handle;

        /// <summary>
        ///     The level.
        /// </summary>
        public uint Level;

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
            if (Variables.Stacks != null && !Variables.Stacks.Any(x => x.Position.Distance(this.Position) < 350))
            {
                Variables.Stacks.Add(new Stack(this.Position));
            }

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