namespace Ability.Extensions
{
    using System.Collections.Generic;

    using Ensage;
    using Ensage.Common;

    using SharpDX;

    internal static class UnitExtensions
    {
        #region Static Fields

        private static readonly Dictionary<float, Vector3> PositionDictionary = new Dictionary<float, Vector3>();

        #endregion

        #region Public Methods and Operators

        public static Vector3 PredictedPosition(this Unit unit, float bonusDelay = 0)
        {
            Vector3 position;
            var handle = unit.Handle;
            if (!PositionDictionary.TryGetValue(handle, out position) || Utils.SleepCheck(handle + "PredictedPosition"))
            {
                position = unit.IsMoving
                               ? Prediction.InFront(unit, unit.MovementSpeed * (Game.Ping / 1000) + bonusDelay)
                               : unit.Position;
                if (PositionDictionary.ContainsKey(handle))
                {
                    PositionDictionary[handle] = position;
                }
                else
                {
                    PositionDictionary.Add(handle, position);
                }

                Utils.Sleep(5, handle + "PredictedPosition");
            }

            return position;
        }

        #endregion
    }
}