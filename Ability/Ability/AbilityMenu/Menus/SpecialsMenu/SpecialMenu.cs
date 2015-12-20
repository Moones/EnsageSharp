namespace Ability.AbilityMenu.Menus.SpecialsMenu
{
    using Ability.AbilityMenu.Items;

    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Menu;

    internal class SpecialMenu
    {
        #region Public Methods and Operators

        public static Menu Create(string name)
        {
            var menu = new Menu(name, name, textureName: name);
            if (name == "rubick_spell_steal")
            {
                return menu;
            }
            menu.AddItem(Togglers.OnSight(name));
            var data = AbilityDatabase.Find(name);
            if (data != null)
            {
                if (data.TrueSight)
                {
                    menu.AddItem(Togglers.OnInvisible(name));
                }
                if (data.IsPurge)
                {
                    menu.AddItem(Togglers.OnPurgable(name));
                }
                if (data.WeakensEnemy)
                {
                    menu.AddItem(Togglers.OnAttack(name));
                }
            }
            return menu;
        }

        #endregion
    }
}