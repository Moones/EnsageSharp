#region

using Ensage;
using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;

#endregion

namespace Ensage.Common
{
    public class VectorOp
    {

        public VectorOp() { }

        public static Vector3 UnitVectorFromXYAngle(double alpha)
        {
            return new Vector3((float)Math.Cos(alpha), (float)Math.Sin(alpha), 0);
        }

        public static float GetDistance2D(Vector3 p1, Vector3 p2)
        {
            return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
        public static float GetDistance2D(Entity unit, Vector3 p2)
        {
            return GetDistance2D(unit.Position, p2);
        }
        public static float GetDistance2D(Vector3 p1, Entity unit)
        {
            return GetDistance2D(p1, unit.Position);
        }

    }
}
