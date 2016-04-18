namespace Ability
{
    using Ability.Casting.ComboExecution;

    /// <summary>
    ///     The program.
    /// </summary>
    internal class Program
    {
        #region Methods

        /// <summary>
        ///     The main.
        /// </summary>
        private static void Main()
        {
            AbilityMain.Init();

            Variables.Killsteal = new Killsteal();
            Variables.AutoUsage = new AutoUsage();
        }

        #endregion
    }
}