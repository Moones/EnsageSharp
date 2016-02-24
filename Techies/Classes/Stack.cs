namespace Techies.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

    using global::Techies.Utility;

    using SharpDX;

    /// <summary>
    ///     The stack.
    /// </summary>
    internal class Stack
    {
        #region Fields

        /// <summary>
        ///     The land mine count.
        /// </summary>
        private string landMineCount;

        /// <summary>
        ///     The land mine count text size.
        /// </summary>
        private Vector2 landMineCountTextSize;

        /// <summary>
        ///     The remote mine count.
        /// </summary>
        private string remoteMineCount;

        /// <summary>
        ///     The remote mine count text size.
        /// </summary>
        private Vector2 remoteMineCountTextSize;

        /// <summary>
        ///     The screen position.
        /// </summary>
        private Vector2 screenPosition;

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
            this.Id = Variables.Stacks.Count + 1;
            this.DetonateTextSize = Drawing.MeasureText("DETONATE!", "Arial", new Vector2(16), FontFlags.None);
            this.XTextSize = Drawing.MeasureText("x", "Arial", new Vector2(30), FontFlags.None);
            this.AutoDetonateTextSize = Drawing.MeasureText("[AUTO DETONATE]", "Arial", new Vector2(12), FontFlags.None);
            this.StackDamageTextSize = Drawing.MeasureText("StackDamage: ", "Arial", new Vector2(12), FontFlags.None);
            this.Visible = true;
            this.AutoDetonate = true;

            Game.OnUpdate += this.OnUpdate;
            Drawing.OnDraw += this.Drawing_OnDraw;
            Game.OnWndProc += this.Game_OnWndProc;
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="Stack" /> class.
        /// </summary>
        ~Stack()
        {
            Game.OnUpdate -= this.OnUpdate;
            Drawing.OnDraw -= this.Drawing_OnDraw;
            Game.OnWndProc -= this.Game_OnWndProc;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether auto detonate.
        /// </summary>
        public bool AutoDetonate { get; set; }

        /// <summary>
        ///     Gets or sets the auto detonate text size.
        /// </summary>
        public Vector2 AutoDetonateTextSize { get; set; }

        /// <summary>
        ///     Gets or sets the detonate text size.
        /// </summary>
        public Vector2 DetonateTextSize { get; set; }

        /// <summary>
        /// Gets or sets the stack damage text size.
        /// </summary>
        public Vector2 StackDamageTextSize { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets the land mine count.
        /// </summary>
        public string LandMineCount
        {
            get
            {
                var n = "Techies.Sleep.Stack.LandMineCount." + this.Id;
                if (!Utils.SleepCheck(n))
                {
                    return this.landMineCount;
                }

                Utils.Sleep(700, n);
                this.landMineCount = this.LandMines.Count.ToString();
                return this.landMineCount;
            }
        }

        /// <summary>
        ///     Gets the land mine count text size.
        /// </summary>
        public Vector2 LandMineCountTextSize
        {
            get
            {
                var n = "Techies.Sleep.Stack.LandMineCountTextSize." + this.Id;
                if (!Utils.SleepCheck(n))
                {
                    return this.landMineCountTextSize;
                }

                Utils.Sleep(1000, n);
                this.landMineCountTextSize = Drawing.MeasureText(
                    this.landMineCount, 
                    "Arial", 
                    new Vector2(16), 
                    FontFlags.None);
                return this.landMineCountTextSize;
            }
        }

        /// <summary>
        ///     Gets or sets the land mines.
        /// </summary>
        public List<LandMine> LandMines { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        ///     Gets the remote mine count.
        /// </summary>
        public string RemoteMineCount
        {
            get
            {
                var n = "Techies.Sleep.Stack.RemoteMineCount." + this.Id;
                if (!Utils.SleepCheck(n))
                {
                    return this.remoteMineCount;
                }

                Utils.Sleep(700, n);
                this.remoteMineCount = this.RemoteMines.Count.ToString();
                return this.remoteMineCount;
            }
        }

        /// <summary>
        ///     Gets the remote mine count text size.
        /// </summary>
        public Vector2 RemoteMineCountTextSize
        {
            get
            {
                var n = "Techies.Sleep.Stack.RemoteMineCountTextSize." + this.Id;
                if (!Utils.SleepCheck(n))
                {
                    return this.remoteMineCountTextSize;
                }

                Utils.Sleep(1000, n);
                this.remoteMineCountTextSize = Drawing.MeasureText(
                    this.remoteMineCount, 
                    "Arial", 
                    new Vector2(16), 
                    FontFlags.None);
                return this.remoteMineCountTextSize;
            }
        }

        /// <summary>
        ///     Gets or sets the remote mines.
        /// </summary>
        public List<RemoteMine> RemoteMines { get; set; }

        /// <summary>
        ///     Gets the screen position.
        /// </summary>
        public Vector2 ScreenPosition
        {
            get
            {
                var n = "Techies.Sleep.Stack.ScreenPosition." + this.Id;
                if (!Utils.SleepCheck(n))
                {
                    return this.screenPosition;
                }

                if (!this.RemoteMines.Any() && !this.LandMines.Any())
                {
                    return this.screenPosition;
                }

                Utils.Sleep(5, n);
                float offset;
                try
                {
                    var mine = this.RemoteMines.First();
                    offset = mine.Entity.HealthBarOffset;
                }
                catch (Exception)
                {
                    offset = this.LandMines.First().Entity.HealthBarOffset;
                }

                return !Drawing.WorldToScreen(this.Position + new Vector3(0, 0, offset), out this.screenPosition)
                           ? Vector2.Zero
                           : this.screenPosition;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether visible.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Gets or sets the raw damage.
        /// </summary>
        public float RawDamage { get; set; }

        /// <summary>
        ///     Gets or sets the x text size.
        /// </summary>
        public Vector2 XTextSize { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The delete.
        /// </summary>
        public void Delete()
        {
            Game.OnUpdate -= this.OnUpdate;
            Drawing.OnDraw -= this.Drawing_OnDraw;
            Game.OnWndProc -= this.Game_OnWndProc;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The drawing_ on draw.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Drawing_OnDraw(EventArgs args)
        {
            if (!Variables.Menu.DrawingsMenu.Item("drawStackOverlay").GetValue<bool>())
            {
                return;
            }

            var position = this.ScreenPosition;
            if (position == Vector2.Zero)
            {
                return;
            }

            if (!this.RemoteMines.Any() && !this.LandMines.Any())
            {
                return;
            }

            if (Game.MousePosition.Distance(this.Position) > 1600)
            {
                return;
            }

            if (!this.Visible)
            {
                var alpha3 = Utils.IsUnderRectangle(
                    Game.MouseScreenPosition,
                    position.X - 45,
                    position.Y - 40,
                    this.XTextSize.X + 4,
                    this.XTextSize.Y)
                                 ? 50
                                 : 0;
                Drawing.DrawText(
                    "*",
                    new Vector2(position.X - 45, position.Y - 40),
                    new Vector2(50),
                    new Color(160 + alpha3, 160 + alpha3, 140 + alpha3, 112 + alpha3),
                    FontFlags.None);
                alpha3 = Utils.IsUnderRectangle(
                    Game.MouseScreenPosition,
                    position.X - 45 + this.XTextSize.X + 8,
                    position.Y - 32,
                    this.AutoDetonateTextSize.X,
                    this.AutoDetonateTextSize.Y)
                             ? 40
                             : 0;
                var color2 = this.AutoDetonate
                                 ? new Color(150 + alpha3, 220 + alpha3, 160 + alpha3, 210 + alpha3)
                                 : new Color(180 + alpha3, 170 + alpha3, 170 + alpha3, 210 + alpha3);
                if (this.AutoDetonate)
                {
                    Drawing.DrawRect(
                        new Vector2(position.X - 45 + this.XTextSize.X + 8, position.Y - 32),
                        this.AutoDetonateTextSize,
                        new Color(70 + alpha3, 160 + alpha3, 70 + alpha3, 70 + alpha3));
                }

                Drawing.DrawText(
                    " AUTO DETONATE",
                    new Vector2(position.X - 45 + this.XTextSize.X + 8, position.Y - 32),
                    new Vector2(12),
                    color2,
                    FontFlags.None);
                return;
            }

            position -= new Vector2(28, 85);

            var alpha = Game.MousePosition.Distance(this.Position) < 500
                            ? Utils.IsUnderRectangle(
                                Game.MouseScreenPosition, 
                                position.X - 30, 
                                position.Y - 10, 
                                170, 
                                74)
                                  ? 20
                                  : 0
                            : -25;

            Drawing.DrawRect(
                new Vector2(position.X - 30, position.Y - 11), 
                new Vector2(170, 74), 
                new Color(30 + alpha, 30 + alpha, 30 + alpha, 150 + (alpha * 2)));
            Drawing.DrawRect(
                new Vector2(position.X - 31, position.Y - 12), 
                new Vector2(171, 75), 
                new Color(0 + alpha, 0 + alpha, 0 + alpha, 150 + alpha), 
                true);
            Drawing.DrawText(
                "StackDamage: ",
                new Vector2(position.X - 27, position.Y + 48),
                new Vector2(12),
                new Color(200 + alpha, 200 + alpha, 200 + alpha, 190 + alpha),
                FontFlags.None);
            var r = Math.Min(this.RawDamage / 20, 255);
            var g = Math.Max(255 - (this.RawDamage / 30), 0);
            var cl = new Color(r, g, 0, 200 + alpha) { R = (byte)r, G = (byte)g };
            Drawing.DrawText(
                this.RawDamage.ToString(CultureInfo.InvariantCulture),
                new Vector2(position.X - 27 + this.StackDamageTextSize.X, position.Y + 48),
                new Vector2(12),
                cl,
                FontFlags.None);
            Drawing.DrawRect(
                position - new Vector2(28, 4), 
                new Vector2(25, 25), 
                Textures.GetTexture("materials/ensage_ui/other/npc_dota_techies_remote_mine"));
            Drawing.DrawText(
                this.RemoteMineCount, 
                position, 
                new Vector2(16), 
                new Color(220 + alpha, 220 + alpha, 220 + alpha, 220 + alpha), 
                FontFlags.None);
            var alpha2 = Utils.IsUnderRectangle(
                Game.MouseScreenPosition, 
                position.X + this.RemoteMineCountTextSize.X + 5, 
                position.Y - 2, 
                20, 
                20)
                             ? 25
                             : 0;
            DrawingUtils.RoundedRectangle(
                position.X + this.RemoteMineCountTextSize.X + 5, 
                position.Y - 2, 
                20, 
                20, 
                4, 
                new Color(100 + alpha - alpha2, 100 + alpha - alpha2, 100 + alpha - alpha2));
            Drawing.DrawRect(
                position + new Vector2(this.RemoteMineCountTextSize.X + 7, 0), 
                new Vector2(16, 16), 
                Textures.GetTexture("materials/ensage_ui/other/plus"));
            alpha2 = Utils.IsUnderRectangle(
                Game.MouseScreenPosition, 
                position.X + this.RemoteMineCountTextSize.X + 30, 
                position.Y - 2, 
                8 + this.DetonateTextSize.X, 
                20)
                         ? 25
                         : 0;
            DrawingUtils.RoundedRectangle(
                position.X + this.RemoteMineCountTextSize.X + 30, 
                position.Y - 2, 
                8 + this.DetonateTextSize.X, 
                20, 
                4, 
                new Color(100 + alpha - alpha2, 100 + alpha - alpha2, 100 + alpha - alpha2));
            Drawing.DrawText(
                "DETONATE!", 
                new Vector2(position.X + this.RemoteMineCountTextSize.X + 30 + 5, position.Y - 2 + 2), 
                new Vector2(16), 
                new Color(200 + alpha + alpha2, 170 + alpha - (alpha2 * 2), 60 + alpha), 
                FontFlags.Outline);
            Drawing.DrawRect(
                position - new Vector2(28, -22), 
                new Vector2(25, 25), 
                Textures.GetTexture("materials/ensage_ui/other/npc_dota_techies_land_mine"));
            Drawing.DrawText(
                this.LandMineCount, 
                position - new Vector2(0, -26), 
                new Vector2(16), 
                new Color(220 + alpha, 220 + alpha, 220 + alpha, 220 + alpha), 
                FontFlags.None);
            alpha2 = Utils.IsUnderRectangle(
                Game.MouseScreenPosition, 
                position.X + this.LandMineCountTextSize.X + 5, 
                position.Y + 24, 
                20, 
                20)
                         ? 25
                         : 0;
            DrawingUtils.RoundedRectangle(
                position.X + this.LandMineCountTextSize.X + 5, 
                position.Y + 24, 
                20, 
                20, 
                3, 
                new Color(100 + alpha - alpha2, 100 + alpha - alpha2, 100 + alpha - alpha2));
            Drawing.DrawRect(
                position + new Vector2(this.LandMineCountTextSize.X + 7, 26), 
                new Vector2(16, 16), 
                Textures.GetTexture("materials/ensage_ui/other/plus"));
            alpha2 = Utils.IsUnderRectangle(
                Game.MouseScreenPosition, 
                position.X + 127, 
                position.Y - 20, 
                this.XTextSize.X, 
                this.XTextSize.Y)
                         ? 50
                         : 0;
            Drawing.DrawText(
                "x", 
                new Vector2(position.X + 127, position.Y - 20), 
                new Vector2(30), 
                new Color(150 + alpha2, 150 + alpha2, 120 + alpha2, 100 + alpha2), 
                FontFlags.DropShadow);
            alpha2 = Utils.IsUnderRectangle(
                Game.MouseScreenPosition, 
                position.X + this.LandMineCountTextSize.X + 30, 
                position.Y + 28, 
                this.AutoDetonateTextSize.X, 
                this.AutoDetonateTextSize.Y)
                         ? 40
                         : 0;
            var color = this.AutoDetonate
                            ? new Color(
                                  140 + alpha + alpha2, 
                                  200 + alpha + alpha2, 
                                  150 + alpha + alpha2, 
                                  200 + alpha + alpha2)
                            : new Color(
                                  180 + alpha + alpha2, 
                                  160 + alpha + alpha2, 
                                  170 + alpha + alpha2, 
                                  200 + alpha + alpha2);
            if (this.AutoDetonate)
            {
                DrawingUtils.RoundedRectangle(
                    position.X - 2 + this.LandMineCountTextSize.X + 30,
                    position.Y - 2 + 28,
                    this.AutoDetonateTextSize.X + 3,
                    this.AutoDetonateTextSize.Y + 4,
                    2,
                    new Color(0 + alpha + alpha2, 100 + alpha + alpha2, 0 + alpha + alpha2));
            }

            Drawing.DrawText(
                "[AUTO DETONATE]", 
                new Vector2(position.X + this.LandMineCountTextSize.X + 30, position.Y + 28), 
                new Vector2(12), 
                color, 
                FontFlags.None);
        }

        /// <summary>
        ///     The game_ on wnd proc.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Game_OnWndProc(WndEventArgs args)
        {
            if (!Variables.Menu.DrawingsMenu.Item("drawStackOverlay").GetValue<bool>())
            {
                return;
            }

            if (args.Msg != (ulong)Utils.WindowsMessages.WM_LBUTTONDOWN)
            {
                return;
            }

            var position = this.ScreenPosition;

            if (!this.Visible
                && Utils.IsUnderRectangle(
                    Game.MouseScreenPosition, 
                    position.X - 45, 
                    position.Y - 40, 
                    this.XTextSize.X + 4, 
                    this.XTextSize.Y))
            {
                this.Visible = !this.Visible;
                return;
            }

            if (!this.Visible)
            {
                if (Utils.IsUnderRectangle(
                    Game.MouseScreenPosition, 
                    position.X - 45 + this.XTextSize.X + 8, 
                    position.Y - 32, 
                    this.AutoDetonateTextSize.X, 
                    this.AutoDetonateTextSize.Y))
                {
                    this.AutoDetonate = !this.AutoDetonate;
                    return;
                }

                return;
            }

            position -= new Vector2(28, 85);

            if (Utils.IsUnderRectangle(
                Game.MouseScreenPosition, 
                position.X + this.LandMineCountTextSize.X + 30, 
                position.Y + 28, 
                this.AutoDetonateTextSize.X, 
                this.AutoDetonateTextSize.Y))
            {
                this.AutoDetonate = !this.AutoDetonate;
                return;
            }

            if (Utils.IsUnderRectangle(
                Game.MouseScreenPosition, 
                position.X + 127, 
                position.Y - 20, 
                this.XTextSize.X, 
                this.XTextSize.Y))
            {
                this.Visible = !this.Visible;
                return;
            }

            if (Variables.RemoteMinesAbility.CanBeCasted()
                && Utils.IsUnderRectangle(
                    Game.MouseScreenPosition, 
                    position.X + this.RemoteMineCountTextSize.X + 5, 
                    position.Y - 2, 
                    20, 
                    20))
            {
                Variables.RemoteMinesAbility.UseAbility(this.Position);

                var n = "Techies.Sleep.Stack.RemoteMineCount." + this.Id;
                if (!Utils.SleepCheck(n + "add"))
                {
                    return;
                }

                this.remoteMineCount = (this.remoteMineCount.To<int>() + 1).ToString();
                Utils.Sleep(2500, n);
                Utils.Sleep(1000, n + "add");
                return;
            }

            if (Variables.LandMinesAbility.CanBeCasted()
                && Utils.IsUnderRectangle(
                    Game.MouseScreenPosition, 
                    position.X + this.LandMineCountTextSize.X + 5, 
                    position.Y + 24, 
                    20, 
                    20))
            {
                Variables.LandMinesAbility.UseAbility(this.Position);
                var n = "Techies.Sleep.Stack.LandMineCount." + this.Id;
                if (!Utils.SleepCheck(n + "add"))
                {
                    return;
                }

                this.landMineCount = (this.landMineCount.To<int>() + 1).ToString();
                Utils.Sleep(1500, n);
                Utils.Sleep(1000, n + "add");
                return;
            }

            if (this.RemoteMines.Any()
                && Utils.IsUnderRectangle(
                    Game.MouseScreenPosition, 
                    position.X + this.RemoteMineCountTextSize.X + 30, 
                    position.Y - 2, 
                    8 + this.DetonateTextSize.X, 
                    20))
            {
                foreach (var remoteMine in this.RemoteMines)
                {
                    remoteMine.Detonate();
                }
            }
        }

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
                Variables.RemoteMines.Where(
                    x => x.Entity.IsValid && x.Entity.IsAlive && x.Position.Distance(this.Position) < 350).ToList();

            this.LandMines =
                Variables.LandMines.Where(
                    x => x.Entity.IsValid && x.Entity.IsAlive && x.Position.Distance(this.Position) < 200).ToList();
            if (this.RemoteMines.Count <= 0 && this.LandMines.Count <= 0)
            {
                Variables.Stacks.Remove(this);
                this.Delete();
            }

            this.RawDamage = this.RemoteMines.Sum(remoteMine => remoteMine.Damage)
                             + this.LandMines.Sum(landMine => landMine.Damage);

            Utils.Sleep(500, this.Id + "Techies.Stacks.Update");
        }

        #endregion
    }
}