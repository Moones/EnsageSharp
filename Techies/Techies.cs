namespace Techies
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

    using global::Techies.Classes;
    using global::Techies.Utility;

    using SharpDX;

    /// <summary>
    ///     The techies.
    /// </summary>
    internal class Techies
    {
        #region Fields

        /// <summary>
        ///     The enabled heroes.
        /// </summary>
        public readonly Dictionary<ClassID, bool> EnabledHeroes;

        /// <summary>
        ///     The hero top panel.
        /// </summary>
        private readonly Dictionary<ClassID, double[]> heroTopPanel;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Techies" /> class.
        /// </summary>
        public Techies()
        {
            Variables.EnemyTeam = Variables.Techies.GetEnemyTeam();

            Variables.RemoteMinesAbility = Variables.Techies.Spellbook.SpellR;
            Variables.RemoteMines = new List<RemoteMine>();

            Variables.LandMines = new List<LandMine>();
            Variables.LandMinesAbility = Variables.Techies.Spellbook.SpellQ;

            Variables.StasisTrapAbility = Variables.Techies.Spellbook.SpellW;
            Variables.StasisTraps = new List<StasisTrap>();

            Variables.SuicideAbility = Variables.Techies.Spellbook.SpellE;

            Variables.Stacks = new List<Stack>();

            if (Variables.Menu == null)
            {
                Variables.Menu = new TechiesMenu();
            }

            if (Variables.Damage == null)
            {
                Variables.Damage = new Damage();
            }
            else
            {
                Variables.Damage.OnLoad();
            }

            this.EnabledHeroes = new Dictionary<ClassID, bool>();
            this.heroTopPanel = new Dictionary<ClassID, double[]>();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The drawing_ on draw.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void Drawing_OnDraw(EventArgs args)
        {
            if (!Game.IsInGame || Game.IsPaused || Variables.Techies == null || !Variables.Techies.IsValid)
            {
                return;
            }

            foreach (var hero in Heroes.GetByTeam(Variables.EnemyTeam))
            {
                var classId = hero.ClassID;
                bool enabled;
                if (!this.EnabledHeroes.TryGetValue(classId, out enabled))
                {
                    this.EnabledHeroes[classId] = true;
                }

                var health = hero.Health;
                if (!hero.IsAlive)
                {
                    health = hero.MaximumHealth;
                }

                double[] topPanel;
                if (!this.heroTopPanel.TryGetValue(classId, out topPanel))
                {
                    topPanel = new double[3];
                    topPanel[0] = HUDInfo.GetTopPanelSizeX(hero);
                    topPanel[1] = HUDInfo.GetTopPanelPosition(hero).X;
                    topPanel[2] = HUDInfo.GetTopPanelSizeY(hero) * 1.4;
                    this.heroTopPanel.Add(classId, topPanel);
                }

                var sizeX = topPanel[0];
                var x = topPanel[1];
                var sizey = topPanel[2];
                if (Variables.Menu.DrawingsMenu.Item("drawTopPanel").GetValue<bool>())
                {
                    DrawRemoteMineNumber(classId, health, x, sizeX, sizey, enabled);
                    DrawLandMineNumber(classId, health, x, sizey, enabled);
                }

                if (!Variables.Damage.GetSuicideDamage().ContainsKey(classId))
                {
                    continue;
                }

                DrawSuicide(classId, health, x, sizey, sizeX, enabled, hero);
            }
        }

        /// <summary>
        ///     The game_ on update.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void Game_OnUpdate(EventArgs args)
        {
            if (!Game.IsInGame || Game.IsPaused || Variables.Techies == null || !Variables.Techies.IsValid)
            {
                return;
            }

            foreach (var module in Variables.Modules.Where(x => x.CanBeExecuted()).Where(module => !module.IsHeroLoop())
                )
            {
                module.Execute();
            }

            foreach (var hero in Heroes.GetByTeam(Variables.EnemyTeam).Where(x => x.IsAlive && x.IsVisible))
            {
                foreach (
                    var module in Variables.Modules.Where(x => x.CanBeExecuted()).Where(module => module.IsHeroLoop()))
                {
                    module.Execute(hero);
                }
            }
        }

        /// <summary>
        ///     The On Window Procedure Event
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void Game_OnWndProc(WndEventArgs args)
        {
            if (args.Msg != (ulong)Utils.WindowsMessages.WM_LBUTTONDOWN)
            {
                return;
            }

            foreach (var hero in
                from play in Heroes.GetByTeam(Variables.EnemyTeam)
                select play
                into hero
                let sizeX = (float)HUDInfo.GetTopPanelSizeX(hero)
                let x = HUDInfo.GetTopPanelPosition(hero).X
                let sizey = HUDInfo.GetTopPanelSizeY(hero) * 1.4
                where Utils.IsUnderRectangle(Game.MouseScreenPosition, x, 0, sizeX, (float)(sizey * 1.4))
                select hero)
            {
                bool enabled;
                if (this.EnabledHeroes.TryGetValue(hero.ClassID, out enabled))
                {
                    this.EnabledHeroes[hero.ClassID] = !enabled;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The draw land mine number.
        /// </summary>
        /// <param name="classId">
        ///     The class id.
        /// </param>
        /// <param name="health">
        ///     The health.
        /// </param>
        /// <param name="x">
        ///     The x.
        /// </param>
        /// <param name="sizey">
        ///     The size y.
        /// </param>
        /// <param name="enabled">
        ///     The enabled.
        /// </param>
        private static void DrawLandMineNumber(ClassID classId, float health, double x, double sizey, bool enabled)
        {
            var landMinesDmg = Variables.Damage.GetLandMineDamage(Variables.LandMinesAbility.Level, classId);
            if (!(landMinesDmg > 0))
            {
                return;
            }

            var landNumber = Math.Ceiling(health / landMinesDmg);
            Drawing.DrawText(
                landNumber.ToString(CultureInfo.InvariantCulture), 
                new Vector2((float)x, (float)sizey), 
                new Vector2(17, 17), 
                enabled ? Color.Red : Color.DimGray, 
                FontFlags.AntiAlias);
        }

        /// <summary>
        ///     The draw remote mine number.
        /// </summary>
        /// <param name="classId">
        ///     The class id.
        /// </param>
        /// <param name="health">
        ///     The health.
        /// </param>
        /// <param name="x">
        ///     The x.
        /// </param>
        /// <param name="sizeX">
        ///     The size x.
        /// </param>
        /// <param name="sizey">
        ///     The size y.
        /// </param>
        /// <param name="enabled">
        ///     The enabled.
        /// </param>
        private static void DrawRemoteMineNumber(
            ClassID classId, 
            float health, 
            double x, 
            double sizeX, 
            double sizey, 
            bool enabled)
        {
            var remoteDmg = Variables.Damage.GetRemoteMineDamage(Variables.RemoteMinesAbility.Level, classId);
            if (!(remoteDmg > 0))
            {
                return;
            }

            var remoteNumber = Math.Ceiling(health / remoteDmg);
            Drawing.DrawText(
                remoteNumber.ToString(CultureInfo.InvariantCulture), 
                new Vector2((float)(x + sizeX / 3.6), (float)sizey), 
                new Vector2(17, 17), 
                enabled ? Color.Green : Color.DimGray, 
                FontFlags.AntiAlias);
        }

        /// <summary>
        ///     The draw suicide.
        /// </summary>
        /// <param name="classId">
        ///     The class id.
        /// </param>
        /// <param name="health">
        ///     The health.
        /// </param>
        /// <param name="x">
        ///     The x.
        /// </param>
        /// <param name="sizey">
        ///     The size y.
        /// </param>
        /// <param name="sizeX">
        ///     The size x.
        /// </param>
        /// <param name="enabled">
        ///     The enabled.
        /// </param>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        private static void DrawSuicide(
            ClassID classId, 
            float health, 
            double x, 
            double sizey, 
            double sizeX, 
            bool enabled, 
            Unit hero)
        {
            var suicideAttackDmg = Variables.Damage.GetSuicideDamage()[classId];
            if (!(suicideAttackDmg > 0))
            {
                return;
            }

            var dmg = health - suicideAttackDmg;
            var canKill = dmg <= 0;
            if (Variables.Menu.DrawingsMenu.Item("drawTopPanel").GetValue<bool>())
            {
                Drawing.DrawText(
                    canKill ? "Yes" : "No", 
                    new Vector2(canKill ? (float)(x + sizeX / 2) : (float)(x + sizeX / 1.7), (float)sizey), 
                    new Vector2(17, 17), 
                    enabled ? Color.DarkOrange : Color.DimGray, 
                    FontFlags.AntiAlias);
            }

            if (!hero.IsVisible || !hero.IsAlive)
            {
                return;
            }

            if (!Variables.Menu.DrawingsMenu.Item("drawSuicideKills").GetValue<bool>())
            {
                return;
            }

            var screenPos = HUDInfo.GetHPbarPosition(hero);
            if (screenPos.X + 20 > Drawing.Width || screenPos.X - 20 < 0 || screenPos.Y + 100 > Drawing.Height
                || screenPos.Y - 30 < 0)
            {
                return;
            }

            var text = canKill ? "Yes" : "No " + Math.Floor(dmg);
            var size = new Vector2(15, 15);
            var textSize = Drawing.MeasureText(text, "Arial", size, FontFlags.AntiAlias);
            var position = new Vector2(screenPos.X - textSize.X - 2, screenPos.Y - 3);
            Drawing.DrawText(
                text, 
                position, 
                size, 
                enabled ? (canKill ? Color.LawnGreen : Color.Red) : Color.Gray, 
                FontFlags.AntiAlias);
        }

        #endregion
    }
}