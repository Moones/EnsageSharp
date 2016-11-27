namespace Ability.ObjectManager
{
    using System.Collections.Generic;

    using Ensage;

    internal class NameManager
    {
        #region Static Fields

        public static Dictionary<float, string> NameDictionary = new Dictionary<float, string>();

        #endregion

        #region Public Methods and Operators

        public static string Name(Entity entity)
        {
            var handle = entity.Handle;
            string name;
            if (NameDictionary.TryGetValue(handle, out name))
            {
                return name;
            }

            name = entity.Name;
            NameDictionary.Add(handle, name);
            return name;
        }

        #endregion
    }
}