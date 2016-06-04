namespace Ability.AbilityEvents
{
    using System;
    using System.Collections.Generic;

    using Ability.AbilityMenu;
    using Ability.Casting;
    using Ability.DamageCalculation;
    using Ability.Drawings;
    using Ability.ObjectManager;
    using Ability.ObjectManager.Heroes;
    using Ability.ObjectManager.Players;
    using Ability.ObjectManager.Towers;

    using Ensage;

    using SharpDX;

    internal class OnLoad
    {
        #region Public Methods and Operators

        public static void Event(object sender, EventArgs e)
        {
            if (Manage.Loaded || ObjectManager.LocalHero == null)
            {
                return;
            }

            AbilityMain.Me = ObjectManager.LocalHero;
            Manage.Loaded = true;
            MainMenu.RestartMenu();
            MyAbilities.OffensiveAbilities = new Dictionary<string, Ability>();
            MyAbilities.DefensiveAbilities = new Dictionary<string, Ability>();
            MyAbilities.NukesCombo = new List<Ability>();
            RangeDrawing.RangesDictionary = new Dictionary<Ability, ParticleEffect>();
            RangeDrawing.RangesValueDictionary = new Dictionary<string, float>();
            GankDamage.IncomingDamages = new Dictionary<string, float>();
            HpBar.HpBarPositionDictionary = new Dictionary<string, Vector2>();
            NameManager.NameDictionary = new Dictionary<float, string>();
            if (AbilityMain.Me.ClassID == ClassID.CDOTA_Unit_Hero_Rubick)
            {
                Rubick.CdDictionary = new Dictionary<string, bool>();
                Rubick.LastCastedDictionary = new Dictionary<string, Ability>();
                Rubick.MyCdDictionary = new Dictionary<string, float>();
            }

            MyAbilities.Blink = null;
            MyAbilities.SoulRing = null;
            MyAbilities.TinkerRearm = null;
            Dictionaries.Init();
            MainMenu.InitializeMenu();
            MenuInit.AddAllToMenu();
            EnemyHeroes.Heroes = new List<Hero>();
            AllyHeroes.Heroes = new List<Hero>();
            EnemyHeroes.UsableHeroes = new Hero[] { };
            AllyHeroes.UsableHeroes = new Hero[] { };
            EnemyPlayers.All = new List<Player>();
            AllyPlayers.All = new List<Player>();
            AllyTowers.Towers = new List<Building>();
            EnemyTowers.Towers = new List<Building>();
            AllyHeroes.AbilityDictionary = new Dictionary<string, List<Ability>>();
            EnemyHeroes.AbilityDictionary = new Dictionary<string, List<Ability>>();
            AllyHeroes.ItemDictionary = new Dictionary<string, List<Item>>();
            EnemyHeroes.ItemDictionary = new Dictionary<string, List<Item>>();
            Manage.SubscribeAllEvents();
            var id = AbilityMain.Me.ClassID;
            var msg = "Fully supported";
            var color = "#009900";
            if (id == ClassID.CDOTA_Unit_Hero_Invoker)
            {
                msg = "Fully supported (configure combo order in Menu>Options>ComboKeys)";
                color = "#009900";
            }
            else if (id == ClassID.CDOTA_Unit_Hero_EarthSpirit)
            {
                msg = "Not supported (only items will be used)";
                color = "#ff0000";
            }
            else if (id == ClassID.CDOTA_Unit_Hero_EmberSpirit)
            {
                msg = "Not supported (only items will be used)";
                color = "#ff0000";
            }
            else if (id == ClassID.CDOTA_Unit_Hero_Shredder)
            {
                msg = "Not supported (only items will be used)";
                color = "#ff0000";
            }
            else if (id == ClassID.CDOTA_Unit_Hero_Wisp)
            {
                msg = "Not supported (only items will be used)";
                color = "#ff0000";
            }
            else if (id == ClassID.CDOTA_Unit_Hero_Meepo)
            {
                msg = "Not supported (only items will be used)";
                color = "#ff0000";
            }
            else if (id == ClassID.CDOTA_Unit_Hero_AncientApparition)
            {
                msg = "Partially supported (will not use ultimate)";
                color = "#ff4400";
            }
            else if (id == ClassID.CDOTA_Unit_Hero_Obsidian_Destroyer)
            {
                msg = "Partially supported (will not use ultimate)";
                color = "#ff4400";
            }
            else if (id == ClassID.CDOTA_Unit_Hero_Chen)
            {
                msg = "Partially supported (will not control creeps)";
                color = "#ff4400";
            }
            else if (id == ClassID.CDOTA_Unit_Hero_Warlock)
            {
                msg = "Partially supported (will not use ultimate)";
                color = "#ff4400";
            }
            else if (id == ClassID.CDOTA_Unit_Hero_StormSpirit)
            {
                msg = "Partially supported (will not use ultimate)";
                color = "#ff4400";
            }

            Game.PrintMessage(
                "<font face='Tahoma'><font color='#ff6600'>A</font><font color='#ffffff'>BILITY</font><font color='#ffff00'>#</font> loaded - hero <font color='#4488ff'>"
                + (id != ClassID.CDOTA_Unit_Hero_Zuus ? id.ToString().Substring("CDOTA_Unit_Hero_".Length) : "Zeus")
                + "</font>: <font color='" + color + "'>" + msg + "</font></font>",
                MessageType.LogMessage);
        }

        #endregion

        // public static bool heroSupported;
    }
}