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
                        "Ally hero health percentage have to be below the specified value in order to use this ability");
        }

        public static MenuItem MinHealth(string name)
        {
            return
                new MenuItem(name + "minhealthslider", "MinimumHP: ").SetValue(new Slider(0, 0, 1000))
                    .SetTooltip("Enemy health have to be higher than the specified value in order to use this ability");
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
                        "Ally hero missing hp have to be higher than the specified value in order to use this ability");
        }

        #endregion
    }
}