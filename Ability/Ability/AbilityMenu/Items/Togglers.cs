namespace Ability.AbilityMenu.Items
{
    using System.Collections.Generic;

    using Ensage.Common.Menu;

    internal class Togglers
    {
        #region Static Fields

        public static string OnAttackDisplayName = "OnAttack: ";

        public static string OnAttackName = "onattacktoggler";

        public static string OnCastDisplayName = "OnCast: ";

        public static string OnCastName = "oncasttoggler";

        public static string OnChainStunDisplayName = "OnChainStun: ";

        public static string OnChainStunName = "onchainstuntoggler";

        public static string OnChannelDisplayName = "OnChannel: ";

        public static string OnChannelName = "onchanneltoggler";

        public static string OnInvisibleDisplayName = "OnInvisible: ";

        public static string OnInvisibleName = "oninvistoggler";

        public static string OnPurgableDisplayName = "OnPurgable: ";

        public static string OnPurgableName = "onpurgetoggler";

        public static string OnSightDisplayName = "OnSight: ";

        public static string OnSightName = "onsighttoggler";

        public static string UnderTowerDisplayName = "UnderTower: ";

        public static string UnderTowerName = "undertowertoggler";

        public static string UseOnDisplayName = "Use on: ";

        public static string UseOnName = "herotoggler";

        #endregion

        #region Public Methods and Operators

        public static MenuItem OnAttack(string name, bool defaultValue = true)
        {
            return
                new MenuItem(name + OnAttackName, OnAttackDisplayName).SetValue(
                    new HeroToggler(new Dictionary<string, bool>(), true, defaultValues: defaultValue));
        }

        public static MenuItem OnCast(string name, bool defaultValue = true)
        {
            defaultValue = defaultValue && !(name == "necrolyte_reapers_scythe" || name == "legion_commander_duel");
            return
                new MenuItem(name + OnCastName, OnCastDisplayName).SetValue(
                    new HeroToggler(new Dictionary<string, bool>(), true, defaultValues: defaultValue));
        }

        public static MenuItem OnChainStun(string name, bool defaultValue = true)
        {
            return
                new MenuItem(name + OnChainStunName, OnChainStunDisplayName).SetValue(
                    new HeroToggler(new Dictionary<string, bool>(), true, defaultValues: defaultValue));
        }

        public static MenuItem OnChannel(string name, bool defaultValue = true)
        {
            return
                new MenuItem(name + OnChannelName, OnChannelDisplayName).SetValue(
                    new HeroToggler(new Dictionary<string, bool>(), true, defaultValues: defaultValue));
        }

        public static MenuItem OnDisableAlly(string name, bool defaultValue = false)
        {
            return
                new MenuItem(name + "ondisabletoggler", "OnDisabled").SetValue(
                    new HeroToggler(new Dictionary<string, bool>(), useAllyHeroes: true, defaultValues: defaultValue));
        }

        public static MenuItem OnDisableEnemy(string name, bool defaultValue = false)
        {
            return
                new MenuItem(name + "ondisabletoggler", "OnDisabled").SetValue(
                    new HeroToggler(new Dictionary<string, bool>(), true, defaultValues: defaultValue));
        }

        public static MenuItem OnInvisible(string name, bool defaultValue = true)
        {
            return
                new MenuItem(name + "oninvistoggler", "OnInvisible: ").SetValue(
                    new HeroToggler(new Dictionary<string, bool>(), true, defaultValues: defaultValue));
        }

        public static MenuItem OnPurgable(string name, bool defaultValue = true)
        {
            return
                new MenuItem(name + OnPurgableName, OnPurgableDisplayName).SetValue(
                    new HeroToggler(new Dictionary<string, bool>(), true, defaultValues: defaultValue));
        }

        public static MenuItem OnSight(string name)
        {
            return
                new MenuItem(name + OnSightName, OnSightDisplayName).SetValue(
                    new HeroToggler(new Dictionary<string, bool>(), true, defaultValues: false));
        }

        public static MenuItem UnderTower(string name, bool defaultValue = true)
        {
            return
                new MenuItem(name + UnderTowerName, UnderTowerDisplayName).SetValue(
                    new HeroToggler(new Dictionary<string, bool>(), true, defaultValues: defaultValue));
        }

        public static MenuItem UseNear(string name, bool defaultValue = true)
        {
            return
                new MenuItem(name + "usenear", "Use when near: ").SetValue(
                    new HeroToggler(new Dictionary<string, bool>(), true, defaultValues: defaultValue));
        }

        public static MenuItem UseOn(string name, bool defaultValue = true)
        {
            return
                new MenuItem(name + UseOnName, UseOnDisplayName).SetValue(
                    new HeroToggler(new Dictionary<string, bool>(), true, defaultValues: defaultValue));
        }

        public static MenuItem UseOnAllies(string name, bool defaultValue = true)
        {
            return
                new MenuItem(name + "useonallies", "Use on: ").SetValue(
                    new HeroToggler(new Dictionary<string, bool>(), false, true, defaultValue));
        }

        #endregion
    }
}