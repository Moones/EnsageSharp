#region

using System;
using SharpDX;

#endregion

namespace Ensage.Common.Extensions
{
    // https://github.com/LeagueSharp/LeagueSharp.SDK/blob/master/Core/Extensions/SharpDX/Vector3Extensions.cs check this for example to make proper extensions
    public static class Vector3Extensions
    {
        public static Vector3 UnitVectorFromXYAngle(double alpha)
        {
            return new Vector3((float)Math.Cos(alpha), (float)Math.Sin(alpha), 0);
        }

        public static float GetDistance2D(this Vector3 p1, Vector3 p2)
        {
            return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
        public static float GetDistance2D(this Vector3 vector, Entity unit)
        {
            return GetDistance2D(unit.Position, vector);
        }
        public static float GetDistance2D(this Entity unit, Vector3 vector)
        {
            return GetDistance2D(unit.Position, vector);
        }
        public static float GetDistance2D(this Entity unit1, Entity unit2)
        {
            return GetDistance2D(unit1.Position, unit2.Position);
        }

        public static float FindAngleBetween(this Vector3 first, Vector3 second)
        {
            var xAngle = (float)(Math.Atan(Math.Abs(second.X - first.X) / Math.Abs(second.Y - first.Y)) * (180.0 / Math.PI));
            if (first.X <= second.X && first.Y >= second.Y)
                return 90 - xAngle;
            if (first.X >= second.X && first.Y >= second.Y)
                return xAngle + 90;
            if (first.X >= second.X && first.Y <= second.Y)
                return 90 - xAngle + 180;
            if (first.X <= second.X && first.Y <= second.Y)
                return xAngle + 90 + 180;
            return 0;
        }

        public static float FindAngleBetween(this Vector3 first, Entity unit)
        {
            return first.FindAngleBetween(unit.Position);
        }
    }
}
