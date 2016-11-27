namespace Ability.DamageCalculation
{
    using System.Collections.Generic;

    internal class Dictionaries
    {
        #region Static Fields

        public static Dictionary<float, float> HitDamageDictionary;

        public static Dictionary<string, double> HitsDictionary;

        public static Dictionary<float, float> InDamageDictionary;

        public static Dictionary<float, float> OutDamageDictionary;

        #endregion

        #region Constructors and Destructors

        static Dictionaries()
        {
            Init();
        }

        #endregion

        #region Public Methods and Operators

        public static void Init()
        {
            HitDamageDictionary = new Dictionary<float, float>();
            InDamageDictionary = new Dictionary<float, float>();
            OutDamageDictionary = new Dictionary<float, float>();
            HitsDictionary = new Dictionary<string, double>();
        }

        #endregion
    }
}