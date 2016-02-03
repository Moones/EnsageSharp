namespace Ability.Drawings
{
    using System;
    using System.Collections.Generic;

    using Ability.AbilityMenu;
    using Ability.ObjectManager;

    using Ensage;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;

    using SharpDX;

    internal class RangeDrawing
    {
        #region Static Fields

        public static Dictionary<Ability, ParticleEffect> RangesDictionary = new Dictionary<Ability, ParticleEffect>();

        public static Dictionary<string, float> RangesValueDictionary = new Dictionary<string, float>();

        private static bool ALensUpdated;

        #endregion

        #region Public Methods and Operators

        public static void AddRange(Ability ability, float crange = 0, bool visible = true)
        {
            var name = NameManager.Name(ability);
            if (RangesDictionary.ContainsKey(ability))
            {
                return;
            }

            var castrange = crange;
            if (castrange == 0)
            {
                castrange = ability.GetCastRange();
            }

            if (!ability.IsAbilityBehavior(AbilityBehavior.NoTarget))
            {
                castrange += Math.Max(castrange / 9, 80);
            }
            else
            {
                castrange += Math.Max(castrange / 7, 40);
            }

            // var list = new[] { "selected_ring", "drag_selected_ring", "hero_underglow" };
            var menu = new Menu(name, name + "range", false, name);
            menu.AddItem(new MenuItem(name + "rangeenable", "Show range")).SetValue(false).ValueChanged +=
                (sender, args) => { RangeVisible(ability, args.GetNewValue<bool>()); };
            menu.AddItem(new MenuItem(name + "red", "Red:"))
                .SetFontStyle(fontColor: Color.Red)
                .SetValue(new Slider(100, 0, 255))
                .ValueChanged += (sender, args) =>
                    {
                        if (RangesDictionary.ContainsKey(ability))
                        {
                            RangesDictionary[ability].SetControlPoint(
                                1, 
                                new Vector3(
                                    args.GetNewValue<Slider>().Value, 
                                    menu.Item(name + "green").GetValue<Slider>().Value, 
                                    menu.Item(name + "blue").GetValue<Slider>().Value));
                        }
                    };
            menu.AddItem(new MenuItem(name + "green", "Green:"))
                .SetFontStyle(fontColor: Color.Green)
                .SetValue(new Slider(100, 0, 255))
                .ValueChanged += (sender, args) =>
                    {
                        if (RangesDictionary.ContainsKey(ability))
                        {
                            RangesDictionary[ability].SetControlPoint(
                                1, 
                                new Vector3(
                                    menu.Item(name + "red").GetValue<Slider>().Value, 
                                    args.GetNewValue<Slider>().Value, 
                                    menu.Item(name + "blue").GetValue<Slider>().Value));
                        }
                    };
            menu.AddItem(new MenuItem(name + "blue", "Blue:"))
                .SetFontStyle(fontColor: Color.Blue)
                .SetValue(new Slider(100, 0, 255))
                .ValueChanged += (sender, args) =>
                    {
                        if (RangesDictionary.ContainsKey(ability))
                        {
                            RangesDictionary[ability].SetControlPoint(
                                1, 
                                new Vector3(
                                    menu.Item(name + "red").GetValue<Slider>().Value, 
                                    menu.Item(name + "green").GetValue<Slider>().Value, 
                                    args.GetNewValue<Slider>().Value));
                        }
                    };
            MainMenu.RangeDrawingMenu.AddSubMenu(menu);
            var range = AbilityMain.Me.AddParticleEffect(@"particles\ui_mouseactions\drag_selected_ring.vpcf");
            if (menu.Item(name + "rangeenable").GetValue<bool>())
            {
                range.SetControlPoint(
                    1, 
                    new Vector3(
                        menu.Item(name + "red").GetValue<Slider>().Value, 
                        menu.Item(name + "green").GetValue<Slider>().Value, 
                        menu.Item(name + "blue").GetValue<Slider>().Value));
                range.SetControlPoint(2, new Vector3(castrange, 255, 0));
                range.SetControlPoint(3, new Vector3(10, 0, 0));
            }
            else
            {
                range.Dispose();
            }

            RangesValueDictionary.Add(name, castrange);
            RangesDictionary.Add(ability, range);
        }

        public static void RangeVisible(Ability ability, bool visible, float crange = 0)
        {
            var name = NameManager.Name(ability);
            ParticleEffect range;
            if (!RangesDictionary.TryGetValue(ability, out range) || RangesDictionary[ability] == null
                || RangesDictionary[ability].IsDestroyed)
            {
                range = AbilityMain.Me.AddParticleEffect(@"particles\ui_mouseactions\drag_selected_ring.vpcf");
                if (!RangesDictionary.ContainsKey(ability))
                {
                    RangesDictionary.Add(ability, range);
                }
                else
                {
                    RangesDictionary[ability] = range;
                }
            }

            var castrange = crange;
            if (castrange == 0)
            {
                castrange = ability.GetCastRange();
            }

            if (!ability.IsAbilityBehavior(AbilityBehavior.NoTarget))
            {
                castrange += Math.Max(castrange / 9, 80);
            }
            else
            {
                castrange += Math.Max(castrange / 7, 40);
            }

            if (visible)
            {
                var menu = MainMenu.RangeDrawingMenu.SubMenu(name + "range");
                range.SetControlPoint(
                    1, 
                    new Vector3(
                        menu.Item(name + "red").GetValue<Slider>().Value, 
                        menu.Item(name + "green").GetValue<Slider>().Value, 
                        menu.Item(name + "blue").GetValue<Slider>().Value));
                range.SetControlPoint(2, new Vector3(castrange, 255, 0));
                range.SetControlPoint(3, new Vector3(10, 0, 0));
                return;
            }

            RangesDictionary[ability].Dispose();
        }

        public static void RemoveRange(Ability ability)
        {
            if (!RangesDictionary.ContainsKey(ability))
            {
                return;
            }

            MainMenu.RangeDrawingMenu.RemoveSubMenu(ability.Name + "range");
            RangesDictionary[ability].Dispose();
            RangesValueDictionary.Remove(ability.Name);
            RangesDictionary.Remove(ability);
        }

        public static void Update()
        {
            if (ALensUpdated)
            {
                return;
            }

            ALensUpdated = true;
            foreach (var particleEffect in RangesDictionary)
            {
                RangeVisible(particleEffect.Key, false);
                RangeVisible(particleEffect.Key, true);
            }
        }

        #endregion
    }
}