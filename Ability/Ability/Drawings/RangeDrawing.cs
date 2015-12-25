namespace Ability.Drawings
{
    using System.Collections.Generic;

    using Ensage;
    using Ensage.Common.Extensions;

    using SharpDX;

    internal class RangeDrawing
    {
        #region Static Fields

        public static Dictionary<string, ParticleEffect> RangesDictionary = new Dictionary<string, ParticleEffect>();

        public static Dictionary<string, float> RangesValueDictionary = new Dictionary<string, float>();

        #endregion

        #region Public Methods and Operators

        public static void AddRange(Ability ability, float crange = 0)
        {
            if (RangesDictionary.ContainsKey(ability.Name))
            {
                return;
            }
            var castrange = crange;
            if (castrange == 0)
            {
                castrange = ability.GetCastRange();
            }
            var range = AbilityMain.Me.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
            range.SetControlPoint(1, new Vector3(castrange, 0, 0));
            RangesValueDictionary.Add(ability.Name, castrange);
            RangesDictionary.Add(ability.Name, range);
        }

        public static void RemoveRange(Ability ability)
        {
            if (!RangesDictionary.ContainsKey(ability.Name))
            {
                return;
            }
            RangesDictionary[ability.Name].Dispose();
            RangesValueDictionary.Remove(ability.Name);
            RangesDictionary.Remove(ability.Name);
        }

        public static void Toggle(Ability ability, float crange = 0)
        {
            if (RangesDictionary.ContainsKey(ability.Name))
            {
                RangesValueDictionary.Remove(ability.Name);
                RangesDictionary.Remove(ability.Name);
                return;
            }
            var castrange = crange;
            if (castrange == 0)
            {
                castrange = ability.GetCastRange();
            }
            var range = AbilityMain.Me.AddParticleEffect(@"particles\ui_mouseactions\range_display.vpcf");
            range.SetControlPoint(1, new Vector3(castrange, 0, 0));
            RangesValueDictionary.Add(ability.Name, castrange);
            RangesDictionary.Add(ability.Name, range);
        }

        #endregion
    }
}