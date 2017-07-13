namespace Ability.Core.AbilityFactory.Utilities
{
    using System;
    using System.Collections.Generic;

    /// <summary>The update manager.</summary>
    public class ActionManager : IDisposable
    {
        private Dictionary<int, Action> actions = new Dictionary<int, Action>();

        private int count;

        public ActionManager()
        {
            
        }
    
        public IReadOnlyDictionary<int, Action> Actions => this.actions;

        public int Subscribe(Action action)
        {
            this.count++;
            var tempActions = new Dictionary<int, Action>(this.actions) { { this.count, action } };
            this.actions = tempActions;
            return this.count;
        }

        public void Unsubscribe(int id)
        {
            var tempActions = new Dictionary<int, Action>(this.actions);
            tempActions.Remove(id);
            this.actions = tempActions;
        }

        public void InvokeActions()
        {
            foreach (var action in this.actions)
            {
                action.Value.Invoke();
            }
        }

        public void Dispose()
        {
            this.actions.Clear();
        }
    }
}
