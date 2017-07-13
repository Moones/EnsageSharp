namespace Ability.Core.AbilityFactory.Utilities
{
    using System;
    using System.Collections.Generic;

    public class ActionExecutor : IDisposable
    {
        private int id;

        private ActionManager actionManager;

        private Dictionary<int, ActionManager> idDictionary = new Dictionary<int, ActionManager>();

        public ActionExecutor(Action action)
        {
            this.Action = action;
        }

        public Action Action { get; }


        public void Subscribe(ActionManager actionManager)
        {
            this.idDictionary.Add(actionManager.Subscribe(this.Action), actionManager);
        }

        public void Dispose()
        {
            foreach (var manager in this.idDictionary)
            {
                manager.Value.Unsubscribe(manager.Key);
            }
        }
    }
}
