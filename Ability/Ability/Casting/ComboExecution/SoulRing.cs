namespace Ability.Casting.ComboExecution
{
    using Ability.AbilityMenu.Menus.HealsMenu;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    internal class SoulRing
    {
        #region Public Properties

        public static float ManaGained
        {
            get
            {
                return MyAbilities.SoulRing != null ? MyAbilities.SoulRing.GetAbilityData("mana_gain") : 0;
            }
        }

        #endregion

        #region Public Methods and Operators

        public static bool Cast(Ability ability)
        {
            if (!Check(ability))
            {
                return true;
            }

            MyAbilities.SoulRing.UseAbility();
            Utils.Sleep(250, "Ability.SoulRing");
            return false;
        }

        public static bool Check(Ability ability)
        {
            return MyAbilities.SoulRing.CanBeCasted() && ability.ManaCost > 0 && Utils.SleepCheck("Ability.SoulRing")
                   && Heals.HealsMenuDictionary["item_soul_ring"].Item("item_soul_ringuseBeforeCast").GetValue<bool>()
                   && Heals.HealsMenuDictionary["item_soul_ring"].Item("item_soul_ringminHp").GetValue<Slider>().Value
                   < AbilityMain.Me.Health;
        }

        #endregion
    }
}