namespace Ability.Extensions
{
    using System.Collections.Generic;

    using Ability.DamageCalculation;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using SharpDX;

    /// <summary>
    ///     The unit extensions.
    /// </summary>
    internal static class UnitExtensions
    {
        #region Static Fields

        /// <summary>
        ///     The position dictionary.
        /// </summary>
        private static readonly Dictionary<float, Vector3> PositionDictionary = new Dictionary<float, Vector3>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get doable damage.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float GetDoableDamage(this Unit unit)
        {
            var dmg = 0f;
            if (Dictionaries.InDamageDictionary.ContainsKey(unit.Handle))
            {
                dmg += Dictionaries.InDamageDictionary[unit.Handle];
            }

            if (Dictionaries.HitDamageDictionary.ContainsKey(unit.Handle)
                && AbilityMain.Me.Distance2D(unit) < AbilityMain.Me.GetAttackRange() + 150)
            {
                dmg += Dictionaries.HitDamageDictionary[unit.Handle];
            }

            return dmg;
        }

        /// <summary>
        ///     The predicted position.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="bonusDelay">
        ///     The bonus delay.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector3" />.
        /// </returns>
        public static Vector3 PredictedPosition(this Unit unit, double bonusDelay = 0)
        {
            Vector3 position;
            var handle = unit.Handle;
            if (!PositionDictionary.TryGetValue((float)(handle + bonusDelay), out position)
                || Utils.SleepCheck(handle + bonusDelay + "PredictedPosition"))
            {
                position = unit.NetworkActivity == NetworkActivity.Move
                               ? Prediction.InFront(
                                   unit, 
                                   (float)(unit.MovementSpeed * ((Game.Ping / 1000) + bonusDelay)))
                               : unit.Position;
                if (PositionDictionary.ContainsKey((float)(handle + bonusDelay)))
                {
                    PositionDictionary[(float)(handle + bonusDelay)] = position;
                }
                else
                {
                    PositionDictionary.Add((float)(handle + bonusDelay), position);
                }

                Utils.Sleep(5, handle + bonusDelay + "PredictedPosition");
            }

            return position;
        }

        #endregion
    }
}