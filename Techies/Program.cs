namespace Techies
{
    using Ensage;

    internal class Program
    {
        #region Methods

        private static void Main()
        {
            Game.OnUpdate += Techies.Game_OnUpdate;
        }

        #endregion
    }
}