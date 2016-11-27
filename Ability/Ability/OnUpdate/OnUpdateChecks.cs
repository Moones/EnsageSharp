namespace Ability.OnUpdate
{
    using Ensage;

    internal class OnUpdateChecks
    {
        #region Public Methods and Operators

        public static bool CanUpdate()
        {
            return !Game.IsPaused && Game.IsInGame && AbilityMain.Me != null && AbilityMain.Me.IsValid;
        }

        #endregion
    }
}