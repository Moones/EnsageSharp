// <copyright file="Number.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Core.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Ensage;
    using Ensage.Common.Objects;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>
    ///     The number.
    /// </summary>
    public sealed class Number : DrawObject
    {
        #region Fields

        /// <summary>
        ///     The color dictionary.
        /// </summary>
        private readonly Dictionary<NumberTextureColor, string> colorDictionary =
            new Dictionary<NumberTextureColor, string>
                {
                    { NumberTextureColor.BabyBlue, "128x128/default_bold_blue" }, { NumberTextureColor.Black, "black" },
                    { NumberTextureColor.Blue, "128x128/default_bold_blue" },
                    { NumberTextureColor.Default, "128x128/default_bold" }, { NumberTextureColor.Gray, "gray" },
                    { NumberTextureColor.Green, "green" }, { NumberTextureColor.Red, "128x128/default_bold_red" }
                };

        /// <summary>
        ///     The texture dictionary.
        /// </summary>
        private readonly Dictionary<char, DotaTexture> textureDictionary = new Dictionary<char, DotaTexture>();

        /// <summary>
        ///     The border.
        /// </summary>
        private bool border;

        /// <summary>
        ///     The color.
        /// </summary>
        private NumberTextureColor color;

        /// <summary>
        ///     The color assigned.
        /// </summary>
        private bool colorAssigned;

        private string colorName;

        /// <summary>
        ///     The current textures.
        /// </summary>
        private Collection<DotaTexture> currentTextures = new Collection<DotaTexture>();

        /// <summary>
        ///     The number size.
        /// </summary>
        private float numberSize;

        /// <summary>
        ///     The value.
        /// </summary>
        private double value;

        #endregion

        #region Constructors and Destructors

        public Number(NumberTextureColor color, bool border, float size)
        {
            this.Border = border;
            this.Color = color;
            this.NumberSize = size;
            this.Value = 0;
        }

        public Number(NumberTextureColor color, bool border)
        {
            this.Border = border;
            this.Color = color;
            this.Value = 0;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether border.
        /// </summary>
        public bool Border
        {
            get
            {
                return this.border;
            }

            set
            {
                if (this.border != value && this.colorAssigned)
                {
                    this.border = value;
                    this.Color = this.color;
                }
                else
                {
                    this.border = value;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        public NumberTextureColor Color
        {
            get
            {
                return this.color;
            }

            set
            {
                this.color = value;
                this.colorAssigned = true;
                var border = this.Border ? "_alt" : string.Empty;
                this.colorName = this.colorDictionary[this.color];
                for (var i = 0; i < 10; i++)
                {
                    var c = Convert.ToChar(i.ToString());

                    // if (this.Color == NumberTextureColor.Default)
                    // {
                    this.textureDictionary[c] =
                        Textures.GetTexture("ensage_ui/other/fonts/" + this.colorName + "/" + i + border);

                    // }
                    // else
                    // {
                    // this.textureDictionary[c] =
                    // Textures.GetTexture(
                    // "ensage_ui/other/fonts/" + this.colorName + "/" + i + "_" + this.colorName + border);
                    // }
                }

                this.textureDictionary[Convert.ToChar(".")] =
                    Textures.GetTexture("ensage_ui/other/fonts/" + this.colorName + "/" + "dot" + border);
            }
        }

        /// <summary>
        ///     Gets or sets the number size.
        /// </summary>
        public float NumberSize
        {
            get
            {
                return this.numberSize;
            }

            set
            {
                this.numberSize = value;
                this.Indent = new Vector2((float)(this.numberSize / 1.85), 0);
                this.CharSize = new Vector2(this.numberSize, this.numberSize);

                // Console.WriteLine(this.CharSize);
                if (this.currentTextures == null)
                {
                    return;
                }

                this.Size = new Vector2(
                    this.Indent.X * this.currentTextures.Count + this.CharSize.X - this.Indent.X,
                    this.CharSize.Y);
            }
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        public double Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (this.value.Equals(value) || value < 0)
                {
                    return;
                }

                this.value = value;
                var temp = new Collection<DotaTexture>();
                foreach (var character in this.value.ToString())
                {
                    temp.Add(this.textureDictionary[character]);
                }

                this.currentTextures = temp;
                this.Size = new Vector2(
                    this.Indent.X * this.currentTextures.Count + this.CharSize.X - this.Indent.X,
                    this.CharSize.Y);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the char size.
        /// </summary>
        private Vector2 CharSize { get; set; }

        /// <summary>
        ///     Gets or sets the indent.
        /// </summary>
        private Vector2 Indent { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        public override void Draw()
        {
            var position = this.Position; // - (new Vector2(this.CharSize.X - this.Indent.X, 0));
            foreach (var currentTexture in this.currentTextures)
            {
                Drawing.DrawRect(position, this.CharSize, currentTexture);
                position += new Vector2(this.Indent.X, 0);
            }
        }

        #endregion
    }

    /// <summary>
    ///     The number texture color.
    /// </summary>
    public enum NumberTextureColor
    {
        /// <summary>
        ///     The baby blue.
        /// </summary>
        BabyBlue,

        /// <summary>
        ///     The black.
        /// </summary>
        Black,

        /// <summary>
        ///     The blue.
        /// </summary>
        Blue,

        /// <summary>
        ///     The default.
        /// </summary>
        Default,

        /// <summary>
        ///     The gray.
        /// </summary>
        Gray,

        /// <summary>
        ///     The green.
        /// </summary>
        Green,

        /// <summary>
        ///     The red.
        /// </summary>
        Red
    }
}