// <copyright file="AbilityBootstrapper.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Security.Permissions;

    using Ability.Core.AbilityData.AbilityDataCollector;
    using Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData;
    using Ability.Core.AbilityFactory;
    using Ability.Core.AbilityFactory.AbilityUnit.Parts.Default.Orbwalker;
    using Ability.Core.AbilityFactory.AbilityUnit.Types;
    using Ability.Core.AbilityManager;
    using Ability.Core.AbilityModule;
    using Ability.Core.AbilityModule.Metadata;
    using Ability.Core.AbilityModule.ModuleBase;
    using Ability.Core.MenuManager;

    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;

    using EnsageSharp.Sandbox;

    /// <summary>
    ///     The program.
    /// </summary>
    public class AbilityBootstrapper
    {
        #region Static Fields

        /// <summary>The catalog.</summary>
        private static AggregateCatalog catalog;

        /// <summary>
        ///     The container.
        /// </summary>
        private static CompositionContainer container;

        /// <summary>The loaded.</summary>
        private static bool loaded;

        #endregion

        #region Fields

        /// <summary>The initialized.</summary>
        private bool initialized;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbilityBootstrapper" /> class.
        /// </summary>
        internal AbilityBootstrapper()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the ability services.
        /// </summary>
        [ImportMany]
        internal IEnumerable<Lazy<IAbilityService>> AbilityServices { get; set; }

        /// <summary>
        ///     Gets or sets the ability skill manager.
        /// </summary>
        [Import(typeof(IAbilityManager))]
        internal Lazy<IAbilityManager> AbilityUnitManager { get; set; }

        [Import(typeof(IAbilityDataCollector))]
        internal Lazy<IAbilityDataCollector> AbilityDataCollector { get; set; }

        [Import(typeof(IAbilityFactory))]
        internal Lazy<IAbilityFactory> AbilityFactory { get; set; }

        /// <summary>
        ///     Gets or sets the main menu manager.
        /// </summary>
        [Import(typeof(IMainMenuManager))]
        internal Lazy<IMainMenuManager> MainMenuManager { get; set; }

        [Import(typeof(IAbilityModuleManager))]
        internal Lazy<IAbilityModuleManager> AbilityModuleManager { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The compose parts.
        /// </summary>
        /// <param name="composeObject">
        ///     The compose object.
        /// </param>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        public static void ComposeParts(object composeObject)
        {
            // Fill the imports of this object
            try
            {
                container.ComposeParts(composeObject);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        /// <summary>The load.</summary>
        public static void Load()
        {
            if (loaded)
            {
                return;
            }

            loaded = true;
            new AbilityBootstrapper().Initialize();
        }

        /// <summary>
        ///     The initialize.
        /// </summary>
        internal void Initialize()
        {
            Events.OnLoad += this.Events_OnLoad;
            Events.OnClose += this.Events_OnClose;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The events_ on close.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void Events_OnClose(object sender, EventArgs e)
        {
            if (!this.initialized)
            {
                return;
            }

            this.initialized = false;
            this.MainMenuManager.Value?.OnClose();
            this.AbilityUnitManager.Value?.OnClose();


            this.AbilityFactory.Value?.OnClose();

            this.AbilityModuleManager.Value?.OnClose();

            this.AbilityDataCollector.Value?.OnClose();

            foreach (var abilityService in this.AbilityServices)
            {
                abilityService.Value?.OnClose();
            }

            container?.Dispose();
            catalog?.Dispose();
        }

        /// <summary>
        ///     The events_ on load.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        private void Events_OnLoad(object sender, EventArgs e)
        {
            if (this.initialized)
            {
                return;
            }

            this.initialized = true;
            GlobalVariables.LocalHero = ObjectManager.LocalHero;
            GlobalVariables.EnemyTeam = UnitExtensions.GetEnemyTeam(GlobalVariables.LocalHero);
            GlobalVariables.Team = GlobalVariables.LocalHero.Team;

            // An aggregate catalog that combines multiple catalogs
            catalog = new AggregateCatalog();

            // Adds all the parts found in the Ability# assembly
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(AbilityBootstrapper).Assembly));

            foreach (var cacheAssembly in AssemblyResolver.AssemblyCache)
            {
                if (cacheAssembly.IsLibrary || !cacheAssembly.IsLoaded || !cacheAssembly.Exists)
                {
                    continue;
                }

                catalog.Catalogs.Add(new AssemblyCatalog(cacheAssembly.Assembly));
            }

            // Environment.
            // catalog.Catalogs.Add(new AssemblyCatalog());
            container = new CompositionContainer(catalog);

            //foreach (var c in catalog)
            //{
            //    Console.WriteLine(c);
            //    //foreach (var cExportDefinition in c.ExportDefinitions)
            //    //{
            //    //    Console.WriteLine(cExportDefinition.ToString());
            //    //    foreach (var o in cExportDefinition.Metadata)
            //    //    {
            //    //        Console.WriteLine(o.ToString());
            //    //    }
            //    //}
            //}

            var delay = Game.GameTime < 0 ? 3000 : 500;
            DelayAction.Add(
                delay,
                () =>
                {
                    ComposeParts(this);
                    if (!Game.IsInGame)
                        {
                            return;
                        }
                        
                        this.AbilityFactory.Value.OnLoad();

                        this.AbilityUnitManager.Value.OnLoad();

                        this.MainMenuManager.Value.OnLoad();

                        foreach (var abilityService in this.AbilityServices)
                        {
                            Console.WriteLine("Service: " + abilityService.Value);
                            abilityService.Value.OnLoad();
                        }

                        this.AbilityModuleManager.Value.OnLoad();

                        this.AbilityDataCollector.Value.OnLoad();
                });

            // DelayAction.Add(
            // 500,
            // () =>
            // {
            // foreach (var abilityService in this.AbilityServices)
            // {
            // Console.WriteLine("Service: " + abilityService.Value);
            // }
            // });
        }

        #endregion
    }
}