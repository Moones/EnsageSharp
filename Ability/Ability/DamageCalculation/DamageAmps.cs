namespace Ability.DamageCalculation
{
    using System;
    using System.Collections.Generic;

    using Ensage;

    internal class DamageAmps
    {
        #region Static Fields

        public static Dictionary<string, float> MultiplierDictionary = new Dictionary<string, float>
                                                                           {
                                                                               { "item_ethereal_blade", 40 }, 
                                                                               { "item_veil_of_discord", 25 }
                                                                           };

        #endregion

        #region Public Methods and Operators

        public static float DamageAmpValue(Ability ability)
        {
            var multi = !MultiplierDictionary.ContainsKey(ability.Name)
                            ? 0
                            : Math.Abs(MultiplierDictionary[ability.Name]);
            return multi;
        }

        public static bool IsDamageAmp(Ability ability)
        {
            return MultiplierDictionary.ContainsKey(ability.Name);
        }

        #endregion
    }
}