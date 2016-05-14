namespace Techies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

    using global::Techies.Classes;
    using global::Techies.Utility;

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

            foreach (var module in Variables.Modules.Where(module => module.CanDraw()))
            {
                module.Draw();
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
                    DrawingUtils.DrawRemoteMineNumber(classId, health, x, sizeX, sizey, enabled);
                    DrawingUtils.DrawLandMineNumber(classId, health, x, sizey, enabled);
                }

                if (!Variables.Damage.GetSuicideDamage().ContainsKey(classId))
                {
                    continue;
                }

                DrawingUtils.DrawSuicide(classId, health, x, sizey, sizeX, enabled, hero);
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

            foreach (
                var hero in Heroes.GetByTeam(Variables.EnemyTeam).Where(x => !x.IsIllusion && x.IsAlive && x.IsVisible))
            {
                foreach (var module in Variables.Modules.Where(x => x.CanBeExecuted() && x.IsHeroLoop()))
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
            foreach (var module in Variables.Modules.Where(x => x.CanDraw()))
            {
                module.OnWndProc(args);
            }

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
    }
}