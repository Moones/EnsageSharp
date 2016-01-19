namespace Techies.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using global::Techies.Utility;

    using SharpDX;

    /// <summary>
    ///     The stack.
    /// </summary>
    internal class Stack
    {
        #region Fields

        /// <summary>
        ///     The id.
        /// </summary>
        public int Id;

        /// <summary>
        ///     The land mines.
        /// </summary>
        public List<LandMine> LandMines;

        /// <summary>
        ///     The position.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        ///     The remote mines.
        /// </summary>
        public List<RemoteMine> RemoteMines;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Stack" /> class.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        public Stack(Vector3 position)
        {
            this.Position = position;
            this.RemoteMines = Variables.RemoteMines.Where(x => x.Position.Distance(position) < 350).ToList();
            this.LandMines = Variables.LandMines.Where(x => x.Position.Distance(position) < 200).ToList();
            Game.OnUpdate += this.OnUpdate;
            this.Id = Variables.Stacks.Count + 1;
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="Stack" /> class.
        /// </summary>
        ~Stack()
        {
            Game.OnUpdate -= this.OnUpdate;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The delete.
        /// </summary>
        public void Delete()
        {
            Game.OnUpdate -= this.OnUpdate;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The on update.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void OnUpdate(EventArgs args)
        {
            if (!Utils.SleepCheck(this.Id + "Techies.Stacks.Update"))
            {
                return;
            }

            this.RemoteMines =
                Variables.RemoteMines.Where(x => x.Entity.IsValid && x.Position.Distance(this.Position) < 350).ToList();

            this.LandMines =
                Variables.LandMines.Where(x => x.Entity.IsValid && x.Position.Distance(this.Position) < 200).ToList();
            if (this.RemoteMines.Count <= 0 && this.LandMines.Count <= 0)
            {
                Variables.Stacks.Remove(this);
                this.Delete();
            }

            Utils.Sleep(500, this.Id + "Techies.Stacks.Update");
        }

        #endregion
    }
}