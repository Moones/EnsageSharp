namespace Ability.AbilityEvents
{
    using System;
    using System.Linq;

    using Ability.AbilityMenu;
    using Ability.Drawings;

    internal class OnClose
    {
        #region Public Methods and Operators

        public static void Event(object sender, EventArgs e)
        {
            if (RangeDrawing.RangesDictionary != null && RangeDrawing.RangesDictionary.Any())
            {
                foreach (var particleEffect in RangeDrawing.RangesDictionary)
                {
                    particleEffect.Value.Dispose();
                }
            }

            Manage.UnsubscribeAllEvents();
            MainMenu.RestartMenu();
            Manage.Loaded = false;
        }

        #endregion
    }
}