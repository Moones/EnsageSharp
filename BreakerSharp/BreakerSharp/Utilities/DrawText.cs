namespace BreakerSharp.Utilities
{
    using Ensage;

    using SharpDX;

    /// <summary>
    ///     The draw text.
    /// </summary>
    public class DrawText
    {
        #region Fields

        /// <summary>
        ///     The sleeper.
        /// </summary>
        private readonly Sleeper sleeper;

        /// <summary>
        ///     The text.
        /// </summary>
        private string text;

        /// <summary>
        ///     The text size.
        /// </summary>
        private Vector2 textSize;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DrawText" /> class.
        /// </summary>
        public DrawText()
        {
            this.sleeper = new Sleeper();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        ///     Gets or sets the font flags.
        /// </summary>
        public FontFlags FontFlags { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        ///     Gets the size.
        /// </summary>
        public Vector2 Size { get; private set; }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                if (this.sleeper.Sleeping)
                {
                    return;
                }

                this.Size = Drawing.MeasureText(this.text, "Arial", this.textSize, this.FontFlags);
                this.sleeper.Sleep(2000);
            }
        }

        /// <summary>
        ///     Gets or sets the text size.
        /// </summary>
        public Vector2 TextSize
        {
            get
            {
                return this.textSize;
            }

            set
            {
                this.textSize = value;
                this.Size = Drawing.MeasureText(this.text, "Arial", this.textSize, this.FontFlags);
                this.sleeper.Sleep(2000);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        public void Draw()
        {
            Drawing.DrawText(this.text, this.Position, this.textSize, this.Color, this.FontFlags);
        }

        #endregion
    }
}