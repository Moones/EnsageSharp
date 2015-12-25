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
        #region Public Methods and Operators

        public static void Cast(Ability ability)
        {
            if (!Check(ability))
            {
                return;
            }
            MyAbilities.SoulRing.UseAbility();
            Utils.Sleep(250, "Ability.SoulRing");
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