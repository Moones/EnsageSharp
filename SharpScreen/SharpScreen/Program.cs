namespace SharpScreen
{
    using System;
    using System.Collections.Generic;

    using Ensage;

    internal class Program
    {
        #region Static Fields

        private static bool loaded;

        #endregion

        #region Methods

        private static void Game_OnUpdate(EventArgs args)
        {
            if (!Game.IsInGame)
            {
                loaded = false;
                return;
            }
            if (loaded)
            {
                return;
            }
            var list = new Dictionary<string, float>
                           {
                               { "fog_enable", 0 }, { "fog_override", 1 }, { "fog_end", 3000 },
                               { "dota_height_fog_scale", 0 }, { "dota_camera_distance", 1700 }, { "cam_showangles", 0 },
                               { "cl_disable_ragdolls", 0 }, { "dota_camera_fog_end_zoomed_in", 4500 },
                               { "dota_camera_fog_end_zoomed_out", 6000 }, { "dota_camera_fog_start_zoomed_in", 2000 },
                               { "dota_camera_fog_start_zoomed_out", 4500 }, { "r_farz", 18000 }
                           };
            foreach (var data in list)
            {
                var var = Game.GetConsoleVar(data.Key);
                var.RemoveFlags(ConVarFlags.Cheat);
                var.SetValue(data.Value);
            }
            loaded = true;
        }

        private static void Main()
        {
            Game.OnUpdate += Game_OnUpdate;
        }

        #endregion
    }
}