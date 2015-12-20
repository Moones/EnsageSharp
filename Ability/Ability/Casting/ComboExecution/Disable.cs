namespace Ability.Casting.ComboExecution
{
    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    internal class Disable
    {
        #region Public Methods and Operators

        public static bool Cast(Ability ability, Unit target, string name)
        {
            var casted = ability.CastStun(target, 1, abilityName: name);
            if (casted && AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_Pudge && name == "pudge_dismember"
                && Utils.SleepCheck("rotToggle"))
            {
                var rot = AbilityMain.Me.Spellbook.Spell2;
                if (!rot.IsToggled)
                {
                    rot.ToggleAbility();
                    Utils.Sleep(500, "rotToggle");
                    return true;
                }
            }
            return casted;
        }

        #endregion
    }
}