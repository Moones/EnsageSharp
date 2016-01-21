namespace Ability.Drawings
{
    using System;
    using System.Globalization;
    using System.Linq;

    using Ability.AbilityMenu;
    using Ability.DamageCalculation;
    using Ability.ObjectManager;
    using Ability.ObjectManager.Heroes;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Menu;

    using SharpDX;

    internal class DamageIndicator
    {
        #region Public Methods and Operators

        public static void Drawing_OnDraw(EventArgs args)
        {
            if (!Game.IsInGame || AbilityMain.Me == null || !AbilityMain.Me.IsValid || EnemyHeroes.Heroes == null)
            {
                return;
            }

            var enumerable = EnemyHeroes.UsableHeroes;
            if (!enumerable.Any())
            {
                return;
            }

            foreach (var hero in enumerable)
            {
                float dmg;
                if (!Dictionaries.InDamageDictionary.TryGetValue(hero.Handle, out dmg))
                {
                    dmg = 0;
                }

                float outdmg;
                if (!Dictionaries.OutDamageDictionary.TryGetValue(hero.Handle, out outdmg))
                {
                    outdmg = 0;
                }

                var hp = Math.Max(hero.Health - dmg, 0);
                var lhp = Math.Max(hp - outdmg, 0);
                var hpperc = hp / hero.MaximumHealth;
                var dmgperc = Math.Min(dmg, hero.Health) / hero.MaximumHealth;
                Vector2 hbarpos;
                HpBar.HpBarPositionDictionary.TryGetValue(NameManager.Name(hero), out hbarpos);
                if (hbarpos.X + 20 > HUDInfo.ScreenSizeX() || hbarpos.X - 20 < 0
                    || hbarpos.Y + 100 > HUDInfo.ScreenSizeY() || hbarpos.Y - 30 < 0)
                {
                    continue;
                }

                var hpvarx = HpBar.SizeX;
                var hpbary = HpBar.SizeY;
                var position = hbarpos + new Vector2(hpvarx * hpperc, (float)(hpbary * 0.4));
                if (MainMenu.DamageIndicatorMenu.Item("abilityDamageIndicatorDrawDamage").GetValue<bool>())
                {
                    if (dmg > 0)
                    {
                        Drawing.DrawRect(
                            position, 
                            new Vector2(hpvarx * dmgperc, (float)(hpbary / 1.6)), 
                            (lhp > 0) ? new Color(225, 200, 150, 95) : new Color(70, 225, 70, 105));
                        Drawing.DrawRect(
                            position, 
                            new Vector2(hpvarx * dmgperc, (float)(hpbary / 1.6)), 
                            Color.Black, 
                            true);
                    }

                    if (outdmg > 0)
                    {
                        var outdmgperc = Math.Min(outdmg, hp) / hero.MaximumHealth;
                        var pos = position + new Vector2(-hpvarx * outdmgperc, 0);
                        Drawing.DrawRect(
                            pos, 
                            new Vector2(hpvarx * outdmgperc, (float)(hpbary / 1.6)), 
                            (lhp > 0) ? new Color(100, 0, 0, 200) : new Color(0, 120, 0, 200));
                        Drawing.DrawRect(
                            pos, 
                            new Vector2(hpvarx * outdmgperc, (float)(hpbary / 1.6)), 
                            Color.Black, 
                            true);
                    }
                }

                if (MainMenu.DamageIndicatorMenu.Item("abilityDamageIndicatorDrawHits").GetValue<bool>())
                {
                    double hits;
                    if (!Dictionaries.HitsDictionary.TryGetValue(hero.Handle.ToString(), out hits))
                    {
                        continue;
                    }

                    var textt = hits.ToString(CultureInfo.InvariantCulture) + ((hits > 1) ? " hits" : " hit");
                    var hpbarpositionX = HUDInfo.GetHPbarPosition(hero).X;
                    var s = (hits > 0) ? textt : "KILL";
                    var h = "(" + Math.Floor(hero.Health - dmg - outdmg) + ")";
                    var isi =
                        MainMenu.DamageIndicatorMenu.Item("abilityDamageIndicatorTextSize").GetValue<Slider>().Value;
                    var textSize = Drawing.MeasureText(
                        s, 
                        "Arial", 
                        new Vector2(hpbary + 4 + isi, 1), 
                        FontFlags.AntiAlias);
                    var textPos =
                        new Vector2(
                            (int)
                            (hpbarpositionX + 4
                             + (HpBar.SizeX * ((float)hero.Health * 1000 / hero.MaximumHealth)) / 1000), 
                            (int)(HUDInfo.GetHPbarPosition(hero).Y - 2));
                    Drawing.DrawText(
                        s, 
                        textPos, 
                        new Vector2(hpbary + 4 + isi, 1), 
                        (hits > 0) ? Color.White : new Color(100, 225, 110), 
                        FontFlags.AntiAlias);
                    Drawing.DrawText(
                        h, 
                        textPos + new Vector2(textSize.X + 2, 1), 
                        new Vector2(hpbary + 2 + isi, 1), 
                        (hits > 0) ? Color.LightGoldenrodYellow : Color.YellowGreen, 
                        FontFlags.AntiAlias);
                }
            }
        }

        #endregion
    }
}