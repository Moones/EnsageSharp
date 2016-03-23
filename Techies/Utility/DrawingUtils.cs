namespace Techies.Utility
{
    using System;
    using System.Globalization;

    using Ensage;
    using Ensage.Common;

    using SharpDX;

    /// <summary>
    ///     The drawing utils.
    /// </summary>
    public static class DrawingUtils
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The draw land mine number.
        /// </summary>
        /// <param name="classId">
        ///     The class id.
        /// </param>
        /// <param name="health">
        ///     The health.
        /// </param>
        /// <param name="x">
        ///     The x.
        /// </param>
        /// <param name="sizey">
        ///     The size y.
        /// </param>
        /// <param name="enabled">
        ///     The enabled.
        /// </param>
        public static void DrawLandMineNumber(ClassID classId, float health, double x, double sizey, bool enabled)
        {
            var landMinesDmg = Variables.Damage.GetLandMineDamage(Variables.LandMinesAbility.Level, classId);
            if (!(landMinesDmg > 0))
            {
                return;
            }

            var landNumber = Math.Ceiling(health / landMinesDmg);
            Drawing.DrawText(
                landNumber.ToString(CultureInfo.InvariantCulture), 
                new Vector2((float)x, (float)sizey), 
                new Vector2(17, 17), 
                enabled ? Color.Red : Color.DimGray, 
                FontFlags.AntiAlias);
        }

        /// <summary>
        ///     The draw remote mine number.
        /// </summary>
        /// <param name="classId">
        ///     The class id.
        /// </param>
        /// <param name="health">
        ///     The health.
        /// </param>
        /// <param name="x">
        ///     The x.
        /// </param>
        /// <param name="sizeX">
        ///     The size x.
        /// </param>
        /// <param name="sizey">
        ///     The size y.
        /// </param>
        /// <param name="enabled">
        ///     The enabled.
        /// </param>
        public static void DrawRemoteMineNumber(
            ClassID classId, 
            float health, 
            double x, 
            double sizeX, 
            double sizey, 
            bool enabled)
        {
            var remoteDmg = Variables.Damage.GetRemoteMineDamage(Variables.RemoteMinesAbility.Level, classId);
            if (!(remoteDmg > 0))
            {
                return;
            }

            var remoteNumber = Math.Ceiling(health / remoteDmg);
            Drawing.DrawText(
                remoteNumber.ToString(CultureInfo.InvariantCulture), 
                new Vector2((float)(x + (sizeX / 3.6)), (float)sizey), 
                new Vector2(17, 17), 
                enabled ? Color.Green : Color.DimGray, 
                FontFlags.AntiAlias);
        }

        /// <summary>
        ///     The draw suicide.
        /// </summary>
        /// <param name="classId">
        ///     The class id.
        /// </param>
        /// <param name="health">
        ///     The health.
        /// </param>
        /// <param name="x">
        ///     The x.
        /// </param>
        /// <param name="sizey">
        ///     The size y.
        /// </param>
        /// <param name="sizeX">
        ///     The size x.
        /// </param>
        /// <param name="enabled">
        ///     The enabled.
        /// </param>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        public static void DrawSuicide(
            ClassID classId, 
            float health, 
            double x, 
            double sizey, 
            double sizeX, 
            bool enabled, 
            Unit hero)
        {
            var suicideAttackDmg = Variables.Damage.GetSuicideDamage()[classId];
            if (!(suicideAttackDmg > 0))
            {
                return;
            }

            var dmg = health - suicideAttackDmg;
            var canKill = dmg <= 0;
            if (Variables.Menu.DrawingsMenu.Item("drawTopPanel").GetValue<bool>())
            {
                Drawing.DrawText(
                    canKill ? "Yes" : "No", 
                    new Vector2(canKill ? (float)(x + (sizeX / 2)) : (float)(x + (sizeX / 1.7)), (float)sizey), 
                    new Vector2(17, 17), 
                    enabled ? Color.DarkOrange : Color.DimGray, 
                    FontFlags.AntiAlias);
            }

            if (!hero.IsVisible || !hero.IsAlive)
            {
                return;
            }

            if (!Variables.Menu.DrawingsMenu.Item("drawSuicideKills").GetValue<bool>())
            {
                return;
            }

            var screenPos = HUDInfo.GetHPbarPosition(hero);
            if (screenPos.X + 20 > Drawing.Width || screenPos.X - 20 < 0 || screenPos.Y + 100 > Drawing.Height
                || screenPos.Y - 30 < 0)
            {
                return;
            }

            var text = canKill ? "Yes" : "No " + Math.Floor(dmg);
            var size = new Vector2(15, 15);
            var textSize = Drawing.MeasureText(text, "Arial", size, FontFlags.AntiAlias);
            var position = new Vector2(screenPos.X - textSize.X - 2, screenPos.Y - 3);
            Drawing.DrawText(
                text, 
                position, 
                size, 
                enabled ? (canKill ? Color.LawnGreen : Color.Red) : Color.Gray, 
                FontFlags.AntiAlias);
        }

        /// <summary>
        ///     The rounded rectangle.
        /// </summary>
        /// <param name="x">
        ///     The x.
        /// </param>
        /// <param name="y">
        ///     The y.
        /// </param>
        /// <param name="w">
        ///     The w.
        /// </param>
        /// <param name="h">
        ///     The h.
        /// </param>
        /// <param name="iSmooth">
        ///     The i smooth.
        /// </param>
        /// <param name="color">
        ///     The color.
        /// </param>
        public static void RoundedRectangle(float x, float y, float w, float h, int iSmooth, Color color)
        {
            var pt = new Vector2[4];

            // Get all corners 
            pt[0].X = x + (w - iSmooth);
            pt[0].Y = y + (h - iSmooth);

            pt[1].X = x + iSmooth;
            pt[1].Y = y + (h - iSmooth);

            pt[2].X = x + iSmooth;
            pt[2].Y = y + iSmooth;

            pt[3].X = x + w - iSmooth;
            pt[3].Y = y + iSmooth;

            // Draw cross 
            Drawing.DrawRect(new Vector2(x, y + iSmooth), new Vector2(w, h - (iSmooth * 2)), color);

            Drawing.DrawRect(new Vector2(x + iSmooth, y), new Vector2(w - (iSmooth * 2), h), color);

            float fDegree = 0;

            for (var i = 0; i < 4; i++)
            {
                for (var k = fDegree; k < fDegree + ((Math.PI * 2) / 4f); k += (float)(1 * (Math.PI / 180.0f)))
                {
                    // Draw quarter circles on every corner
                    Drawing.DrawLine(
                        new Vector2(pt[i].X, pt[i].Y), 
                        new Vector2(pt[i].X + (float)(Math.Cos(k) * iSmooth), pt[i].Y + (float)(Math.Sin(k) * iSmooth)), 
                        color);
                }

                fDegree += (float)(Math.PI * 2) / 4; // quarter circle offset 
            }
        }

        #endregion
    }
}