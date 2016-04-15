namespace Ability.ObjectManager
{
    using System.Linq;

    using Ensage;

    using SharpDX;

    internal class Base
    {
        #region Static Fields

        private static Unit baseEntity;

        #endregion

        #region Public Methods and Operators

        public static Vector3 Position()
        {
            if (baseEntity == null || !baseEntity.IsValid)
            {
                baseEntity =
                    ObjectManager.GetEntities<Unit>()
                        .FirstOrDefault(x => x.ClassID == ClassID.CDOTA_Unit_Fountain && x.Team == AbilityMain.Me.Team);
            }

            return baseEntity != null ? baseEntity.Position : Vector3.Zero;
        }

        #endregion
    }
}