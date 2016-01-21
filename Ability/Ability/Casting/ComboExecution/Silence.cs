namespace Ability.Casting.ComboExecution
{
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.Extensions;

    internal class Silence
    {
        #region Public Methods and Operators

        public static bool Cast(Ability ability, Unit target, string name)
        {
            if (target.IsSilenced())
            {
                return false;
            }

            return ability.CastStun(
                target, 
                MyHeroInfo.Position, 
                1, 
                soulRing: SoulRing.Check(ability) ? MyAbilities.SoulRing : null);
        }

        #endregion
    }
}