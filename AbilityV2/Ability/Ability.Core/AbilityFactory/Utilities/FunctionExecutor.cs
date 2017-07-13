namespace Ability.Core.AbilityFactory.Utilities
{
    using System;

    public class FunctionExecutor<T> : IDisposable
    {
        private int id;

        private FunctionManager<T> functionManager;

        public FunctionExecutor(Func<T, bool> function)
        {
            this.Function = function;
        }

        public Func<T, bool> Function { get; }

        public void Subscribe(FunctionManager<T> functionManager)
        {
            this.id = functionManager.Subscribe(this.Function);
            this.functionManager = functionManager;
        }

        public void Dispose()
        {
            this.functionManager.Unsubscribe(this.id);
        }
    }
}
