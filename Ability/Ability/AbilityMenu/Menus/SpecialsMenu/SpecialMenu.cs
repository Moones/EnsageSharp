namespace Ability.AbilityMenu.Menus.SpecialsMenu
{
    using Ability.AbilityMenu.Items;

    using Ensage;
    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Menu;

    internal class SpecialMenu
    {
        #region Public Methods and Operators

        public static Menu Create(string name, Ability ability)
        {
            var menu = new Menu(name, name, textureName: name);
            if (name == "rubick_spell_steal")
            {
                return menu;
            }

            menu.AddItem(Togglers.OnSight(name));
            var data = AbilityDatabase.Find(name);
            var defaultValue = ability.AbilityType != AbilityType.Ultimate;
            if (data != null)
            {
                if (data.TrueSight)
                {
                    menu.AddItem(Togglers.OnInvisible(name, defaultValue));
                }

                if (data.IsPurge)
                {
                    menu.AddItem(Togglers.OnPurgable(name, defaultValue));
                }

                if (data.WeakensEnemy)
                {
                    menu.AddItem(Togglers.OnAttack(name, defaultValue));
                }
            }

            menu.AddItem(Sliders.MinManaCheck(name));
            menu.AddItem(Sliders.MinManaCheck(name, true));
            return menu;
        }

        #endregion
    }
}