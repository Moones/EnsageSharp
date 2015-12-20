namespace Ability.Drawings
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Ability.AbilityMenu;
    using Ability.ObjectManager;
    using Ability.ObjectManager.Heroes;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using SharpDX;

    internal class AbilityOverlay
    {
        #region Static Fields

        private static readonly float BoxSizeX;

        private static readonly float BoxSizeY;

        private static readonly float ItemBoxSizeX;

        private static readonly float ItemBoxSizeY;

        private static readonly Dictionary<string, DotaTexture> TextureDictionary =
            new Dictionary<string, DotaTexture>();

        #endregion

        #region Constructors and Destructors

        static AbilityOverlay()
        {
            BoxSizeX = HpBar.SizeX / 6;
            BoxSizeY = BoxSizeX + 1;
            ItemBoxSizeX = HpBar.SizeX / 6;
            ItemBoxSizeY = (float)(ItemBoxSizeX / 1.24);
        }

        #endregion

        #region Public Methods and Operators

        public static void Drawing_OnDraw(EventArgs args)
        {
            if (!Game.IsInGame || AbilityMain.Me == null || !AbilityMain.Me.IsValid || EnemyHeroes.Heroes == null)
            {
                return;
            }
            var size = new Vector2(HpBar.SizeX, (float)(HpBar.SizeY / 2.8));
            var spellOverlayEnabledEnemy = MainMenu.AbilityOverlayMenu.Item("enableSpellOverlayEnemy").GetValue<bool>();
            try
            {
                foreach (var hero in EnemyHeroes.UsableHeroes)
                {
                    var heroName = NameManager.Name(hero);
                    var mana = hero.Mana;
                    var maxMana = hero.MaximumMana;
                    var hpbarpos = HpBar.HpBarPositionDictionary[heroName];
                    if (hpbarpos.X + 20 > HUDInfo.ScreenSizeX() || hpbarpos.X - 20 < 0
                        || hpbarpos.Y + 100 > HUDInfo.ScreenSizeY() || hpbarpos.Y - 30 < 0)
                    {
                        continue;
                    }
                    var start = hpbarpos + new Vector2(0, HpBar.SizeY + 1);
                    var manaperc = mana / maxMana;
                    Drawing.DrawRect(start, size + new Vector2(1, 1), new Color(0, 0, 50, 150));
                    Drawing.DrawRect(start, new Vector2(size.X * manaperc, size.Y), new Color(70, 120, 220));
                    Drawing.DrawRect(start + new Vector2(-1, -1), size + new Vector2(3, 3), Color.Black, true);
                    var defaultPos = hpbarpos + new Vector2(0, HpBar.SizeY + size.Y + 3);
                    var position = defaultPos;

                    if (spellOverlayEnabledEnemy)
                    {
                        foreach (var ability in
                            EnemyHeroes.AbilityDictionary[heroName].Where(
                                x => (int)x.AbilitySlot >= 0 && (int)x.AbilitySlot <= 5)
                                .OrderBy(x => (int)x.AbilitySlot))
                        {
                            DrawAbilityOverlay(ability, position, heroName, mana);
                            position += new Vector2(BoxSizeX, 0);
                        }
                    }
                    if (MainMenu.AbilityOverlayMenu.Item("enableItemOverlayEnemy").GetValue<bool>())
                    {
                        var itemPos = hpbarpos - new Vector2(0, ItemBoxSizeY);
                        foreach (var ability in EnemyHeroes.ItemDictionary[heroName].Where(ability => ability.IsValid))
                        {
                            DrawItemOverlay(ability, itemPos, mana);
                            itemPos += new Vector2(ItemBoxSizeX, 0);
                        }
                    }
                }
            }
            catch (EntityNotFoundException)
            {
                EnemyHeroes.UpdateHeroes();
            }
            var spellOverlayEnabledAlly = MainMenu.AbilityOverlayMenu.Item("enableSpellOverlayAlly").GetValue<bool>();
            try
            {
                foreach (var hero in AllyHeroes.UsableHeroes)
                {
                    var heroName = NameManager.Name(hero);
                    var mana = hero.Mana;
                    var hpbarpos = HpBar.HpBarPositionDictionary[heroName];
                    if (hpbarpos.X + 20 > HUDInfo.ScreenSizeX() || hpbarpos.X - 20 < 0
                        || hpbarpos.Y + 100 > HUDInfo.ScreenSizeY() || hpbarpos.Y - 30 < 0)
                    {
                        continue;
                    }
                    var defaultPos = hpbarpos + new Vector2(0, HpBar.SizeY + size.Y + 3);
                    if (hero.Equals(AbilityMain.Me))
                    {
                        defaultPos += new Vector2(-3, 3);
                    }
                    var position = defaultPos;
                    if (spellOverlayEnabledAlly)
                    {
                        foreach (var ability in
                            AllyHeroes.AbilityDictionary[heroName].Where(
                                x => (int)x.AbilitySlot >= 0 && (int)x.AbilitySlot <= 5)
                                .OrderBy(x => (int)x.AbilitySlot))
                        {
                            DrawAbilityOverlay(ability, position, heroName, mana);
                            position += new Vector2(BoxSizeX, 0);
                        }
                    }
                    if (MainMenu.AbilityOverlayMenu.Item("enableItemOverlayAlly").GetValue<bool>())
                    {
                        var itemPos = hpbarpos - new Vector2(0, ItemBoxSizeY);
                        if (hero.Equals(AbilityMain.Me))
                        {
                            itemPos += new Vector2(-3, 1);
                        }
                        foreach (var ability in
                            AllyHeroes.ItemDictionary[heroName].Where(ability => ability.IsValid))
                        {
                            DrawItemOverlay(ability, itemPos, mana);
                            itemPos += new Vector2(ItemBoxSizeX, 0);
                        }
                    }
                }
            }
            catch (EntityNotFoundException)
            {
                AllyHeroes.UpdateHeroes();
            }
        }

        #endregion

        #region Methods

        private static void DrawAbilityOverlay(Ability ability, Vector2 position, string heroName, float mana)
        {
            var name = NameManager.Name(ability);
            var level = ability.Level;
            var isInvoker = heroName == "npc_dota_hero_invoker";
            var color = new Color(168, 168, 168);
            var enoughMana = mana >= ability.ManaCost;
            var notinvospell = !isInvoker
                               || (ability.AbilitySlot != AbilitySlot.Slot_4
                                   && ability.AbilitySlot != AbilitySlot.Slot_5);
            DotaTexture texture;
            if (!TextureDictionary.TryGetValue(name, out texture))
            {
                texture = Drawing.GetTexture("materials/ensage_ui/spellicons/" + name + ".vmat");
                TextureDictionary.Add(name, texture);
            }
            Drawing.DrawRect(position + new Vector2(1, 0), new Vector2(BoxSizeX, BoxSizeY), texture);
            if (!ability.CanBeCasted())
            {
                Drawing.DrawRect(
                    position + new Vector2(1, 0),
                    new Vector2(BoxSizeX - 1, BoxSizeY),
                    level <= 0
                        ? new Color(10, 10, 10, 243)
                        : (enoughMana ? new Color(40, 40, 40, 180) : new Color(25, 25, 130, 190)));
            }
            else
            {
                Drawing.DrawRect(
                    position + new Vector2(1, 0),
                    new Vector2(BoxSizeX - 1, BoxSizeY),
                    new Color(0, 0, 0, 100));
            }
            if (notinvospell)
            {
                var s = level.ToString();
                var textSize = Drawing.MeasureText(s, "Arial", new Vector2(HpBar.SizeY, 1), FontFlags.AntiAlias);
                Drawing.DrawRect(
                    position + new Vector2(1, 0),
                    new Vector2(textSize.X + 2, textSize.Y + 1),
                    new Color(0, 0, 0, 220));
                Drawing.DrawText(
                    s,
                    position + new Vector2(1, 0),
                    new Vector2(HpBar.SizeY, 1),
                    color,
                    FontFlags.AntiAlias | FontFlags.StrikeOut);
            }
            var cooldown = Math.Ceiling(ability.Cooldown);
            if (cooldown > 0)
            {
                var h = Math.Min(cooldown, 99).ToString(CultureInfo.InvariantCulture);
                var hsize = cooldown > 9 ? new Vector2(HpBar.SizeY + 3, 15) : new Vector2(HpBar.SizeY + 5, 25);
                var textSize = Drawing.MeasureText(h, "Arial", hsize, FontFlags.AntiAlias);
                Vector2 pos;
                if (!notinvospell)
                {
                    pos = position + new Vector2((BoxSizeX / 2 - textSize.X / 2), (BoxSizeY / 2) - (textSize.Y / 2));
                }
                else
                {
                    pos = position
                          + new Vector2((float)(BoxSizeX / 1.5 - textSize.X / 2), (BoxSizeY / 2) - (textSize.Y / 2));
                }
                if (cooldown < 10)
                {
                    pos += new Vector2(1, 0);
                }
                Drawing.DrawText(h, pos, hsize, Color.WhiteSmoke, FontFlags.AntiAlias);
            }
            if (!enoughMana && cooldown <= 0)
            {
                var h = Math.Min(Math.Ceiling(ability.ManaCost - mana), 999).ToString(CultureInfo.InvariantCulture);
                var textSize = Drawing.MeasureText(h, "Arial", new Vector2(HpBar.SizeY + 1, 1), FontFlags.AntiAlias);
                Vector2 pos;
                if (!notinvospell)
                {
                    pos = position
                          + new Vector2((BoxSizeX / 2 - textSize.X / 2), (float)((BoxSizeY / 1.5) - (textSize.Y / 2)));
                }
                else
                {
                    pos = position
                          + new Vector2(
                                (float)(BoxSizeX / 1.5 - textSize.X / 2),
                                (float)((BoxSizeY / 1.5) - (textSize.Y / 2)));
                }
                Drawing.DrawText(h, pos, new Vector2(HpBar.SizeY + 1, 1), Color.LightBlue, FontFlags.AntiAlias);
            }
            Drawing.DrawRect(position, new Vector2(BoxSizeX + 1, BoxSizeY), Color.Black, true);
        }

        private static void DrawItemOverlay(Item ability, Vector2 position, float mana)
        {
            var name = NameManager.Name(ability);
            var level = ability.Level;
            var enoughMana = mana >= ability.ManaCost;
            DotaTexture texture;
            if (!TextureDictionary.TryGetValue(name, out texture))
            {
                texture = Drawing.GetTexture("materials/ensage_ui/items/" + name.Substring("item_".Length) + ".vmat");
                TextureDictionary.Add(name, texture);
            }
            Drawing.DrawRect(
                position + new Vector2(1, 0),
                new Vector2((float)(ItemBoxSizeX + ItemBoxSizeX / 2.6), ItemBoxSizeY),
                texture);
            if (!ability.CanBeCasted())
            {
                Drawing.DrawRect(
                    position + new Vector2(1, 0),
                    new Vector2(ItemBoxSizeX - 1, ItemBoxSizeY),
                    level <= 0
                        ? new Color(10, 10, 10, 243)
                        : (enoughMana ? new Color(40, 40, 40, 180) : new Color(25, 25, 130, 190)));
            }
            else
            {
                Drawing.DrawRect(
                    position + new Vector2(1, 0),
                    new Vector2(ItemBoxSizeX - 1, ItemBoxSizeY),
                    new Color(0, 0, 0, 100));
            }
            var cooldown = Math.Ceiling(ability.Cooldown);
            if (cooldown > 0)
            {
                var h = Math.Min(cooldown, 99).ToString(CultureInfo.InvariantCulture);
                var hsize = cooldown > 9 ? new Vector2(HpBar.SizeY - 1, 15) : new Vector2(HpBar.SizeY + 1, 25);
                var textSize = Drawing.MeasureText(h, "Arial", hsize, FontFlags.AntiAlias);
                var pos = position
                          + new Vector2((ItemBoxSizeX / 2 - textSize.X / 2), (ItemBoxSizeY / 2) - (textSize.Y / 2));
                Drawing.DrawText(h, pos, hsize, Color.WhiteSmoke, FontFlags.AntiAlias);
            }
            if (!enoughMana && cooldown <= 0)
            {
                var h = Math.Min(Math.Ceiling(ability.ManaCost - mana), 999).ToString(CultureInfo.InvariantCulture);
                var textSize = Drawing.MeasureText(h, "Arial", new Vector2(HpBar.SizeY - 2, 1), FontFlags.AntiAlias);
                var pos = position
                          + new Vector2((ItemBoxSizeX / 2 - textSize.X / 2), (ItemBoxSizeY / 2) - (textSize.Y / 2));
                Drawing.DrawText(h, pos, new Vector2(HpBar.SizeY - 2, 1), Color.LightBlue, FontFlags.AntiAlias);
            }
            if (ability.IsRequiringCharges || ability.Name == "item_ward_dispenser"
                || ability.Name == "item_ward_observer" || ability.Name == "item_ward_sentry")
            {
                var s = ability.CurrentCharges.ToString();
                var tSize = new Vector2(HpBar.SizeY, 1);
                var textSize = Drawing.MeasureText(s, "Arial", tSize, FontFlags.AntiAlias);
                var tPos = position + new Vector2(ItemBoxSizeX - textSize.X - 2, ItemBoxSizeY - textSize.Y - 1);
                Drawing.DrawRect(
                    tPos - new Vector2(1, 0),
                    new Vector2(textSize.X + 1, textSize.Y + 1),
                    new Color(0, 0, 0, 220));
                Drawing.DrawText(s, tPos, tSize, new Color(168, 168, 168), FontFlags.AntiAlias | FontFlags.StrikeOut);
                var secondcharges = ability.SecondaryCharges;
                if (secondcharges > 0)
                {
                    tPos = position + new Vector2(2, ItemBoxSizeY - textSize.Y - 1);
                    s = secondcharges.ToString();
                    tSize = new Vector2(HpBar.SizeY, 1);
                    var textSize1 = Drawing.MeasureText(s, "Arial", tSize, FontFlags.AntiAlias);
                    Drawing.DrawRect(
                        tPos - new Vector2(1, 0),
                        new Vector2(textSize1.X + 1, textSize1.Y + 1),
                        new Color(0, 0, 0, 220));
                    Drawing.DrawText(
                        s,
                        tPos,
                        tSize,
                        new Color(168, 168, 168),
                        FontFlags.AntiAlias | FontFlags.StrikeOut);
                }
            }

            Drawing.DrawRect(position, new Vector2(ItemBoxSizeX + 1, ItemBoxSizeY), Color.Black, true);
        }

        #endregion
    }
}