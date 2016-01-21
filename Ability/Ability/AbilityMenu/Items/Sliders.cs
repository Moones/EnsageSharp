namespace Ability.AbilityMenu.Items
{
    using Ensage.Common.Menu;

    internal class Sliders
    {
        #region Public Methods and Operators

        public static MenuItem HpPercentBelow(string name)
        {
            var defaultValue = 70;
            if (name == "dazzle_shallow_grave")
            {
                defaultValue = 30;
            }
            else if (name == "dazzle_shadow_wave")
            {
                defaultValue = 80;
            }

            return
                new MenuItem(name + "hppercentbelow", "Use when their HP % goes below: ").SetValue(
                    new Slider(defaultValue, 10))
                    .SetTooltip(
                        "Ally hero/or your health percentage have to be below the specified value in order to use this ability");
        }

        public static MenuItem ManaPercentBelow(string name)
        {
            var defaultValue = 70;
            return
                new MenuItem(name + "manapercentbelow", "Use when their mana % goes below: ").SetValue(
                    new Slider(defaultValue, 10))
                    .SetTooltip(
                        "Ally hero/or your mana percentage have to be below the specified value in order to use this ability");
        }

        public static MenuItem MinHealth(string name)
        {
            return
                new MenuItem(name + "minhealthslider", "MinimumHP: ").SetValue(new Slider(0, 0, 1000))
                    .SetTooltip("Enemy health have to be higher than the specified value in order to use this ability");
        }

        public static MenuItem MinManaCheck(string name, bool combo = false)
        {
            // var defaultValue = 150;
            return combo
                       ? new MenuItem(name + "minManaCheckCombo", "Minimum mana to use when holding key: ").SetValue(
                           new Slider(0, 0, 1000))
                             .SetTooltip(
                                 "If your mana goes below specified value this ability will NOT be used while holding key!")
                       : new MenuItem(name + "minManaCheck", "Minimum mana to use: ").SetValue(new Slider(0, 0, 1000))
                             .SetTooltip(
                                 "If your mana goes below specified value this ability will NOT be used automatically!");
        }

        public static MenuItem MinStraightTime(string name)
        {
            return
                new MenuItem(name + "minstraighttimeslider", "Minimum Straight Time: ").SetValue(new Slider(1, 0, 5))
                    .SetTooltip(
                        "Enemy have to walk atleast this amount of seconds straight in order to use this skillshot ability");
        }

        public static MenuItem MissingHpMin(string name)
        {
            var defaultValue = 300;
            return
                new MenuItem(name + "missinghpmin", "Use when missing hp is higher then: ").SetValue(
                    new Slider(defaultValue, 10, 1500))
                    .SetTooltip(
                        "Ally hero/or your missing hp have to be higher than the specified value in order to use this ability");
        }

        public static MenuItem MissingManaMin(string name)
        {
            var defaultValue = 200;
            return
                new MenuItem(name + "missingmanamin", "Use when missing mana is higher then: ").SetValue(
                    new Slider(defaultValue, 10, 1500))
                    .SetTooltip(
                        "Ally hero/or your missing mana have to be higher than the specified value in order to use this ability");
        }

        #endregion
    }
}