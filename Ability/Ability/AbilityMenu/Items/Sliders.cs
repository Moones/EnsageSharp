namespace Ability.AbilityMenu.Items
{
    using Ensage.Common.Menu;

    internal class Sliders
    {
        #region Public Methods and Operators

        public static MenuItem HpPercentBelow(string name)
        {
            return
                new MenuItem(name + "hppercentbelow", "Use when their HP % goes below: ").SetValue(new Slider(100, 10))
                    .SetTooltip(
                        "Ally hero/or your health percentage have to be below the specified value in order to use this ability");
        }

        public static MenuItem ManaPercentBelow(string name)
        {
            return
                new MenuItem(name + "manapercentbelow", "Use when their mana % goes below: ").SetValue(
                    new Slider(100, 10))
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
            return
                new MenuItem(name + "missinghpmin", "Use when missing hp is higher then: ").SetValue(
                    new Slider(100, 10, 1500))
                    .SetTooltip(
                        "Ally hero/or your missing hp have to be higher than the specified value in order to use this ability");
        }

        public static MenuItem MissingManaMin(string name)
        {
            return
                new MenuItem(name + "missingmanamin", "Use when missing mana is higher then: ").SetValue(
                    new Slider(100, 10, 1500))
                    .SetTooltip(
                        "Ally hero/or your missing mana have to be higher than the specified value in order to use this ability");
        }

        #endregion
    }
}