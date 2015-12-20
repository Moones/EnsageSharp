namespace Ability.AbilityMenu
{
    using Ability.Drawings;

    using Ensage;
    using Ensage.Common.Menu;

    internal class BlinkMenu
    {
        #region Public Methods and Operators

        public static void Create(Ability blink)
        {
            var menu = new Menu("Blink", "abilityMenuBlink", false, "item_blink", true);
            menu.AddItem(new MenuItem("abilityMenuShowBlinkRange", "Show Range").SetValue(true)).ValueChanged +=
                delegate(object sender, OnValueChangeEventArgs eventArgs)
                    {
                        if (!eventArgs.GetNewValue<bool>())
                        {
                            RangeDrawing.RemoveRange(blink);
                        }
                        else
                        {
                            RangeDrawing.AddRange(blink, 1200);
                        }
                    };
            RangeDrawing.AddRange(blink, 1200);
            MainMenu.OptionsMenu.AddSubMenu(menu);
        }

        #endregion
    }
}