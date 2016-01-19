// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITechiesModule.cs" company="Moones">
//   EnsageSharp
// </copyright>
// <summary>
//   The TechiesModule interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Techies.Modules
{
    using Ensage;

    /// <summary>
    ///     The TechiesModule interface.
    /// </summary>
    internal interface ITechiesModule
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The can be executed.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool CanBeExecuted();

        /// <summary>
        ///     The execute.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool Execute(Hero hero = null);

        /// <summary>
        ///     The is hero loop.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        bool IsHeroLoop();

        /// <summary>
        ///     The on close.
        /// </summary>
        void OnClose();

        /// <summary>
        ///     The on load.
        /// </summary>
        void OnLoad();

        #endregion
    }
}