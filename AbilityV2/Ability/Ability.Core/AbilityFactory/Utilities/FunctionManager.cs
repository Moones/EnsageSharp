namespace Ability.Core.AbilityFactory.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FunctionManager<T> : IDisposable
    {
        private Dictionary<int, Func<T, bool>> functions = new Dictionary<int, Func<T, bool>>();

        private int count;

        public FunctionManager()
        {
            
        }

        public IReadOnlyDictionary<int, Func<T, bool>> Functions => this.functions;

        public int Subscribe(Func<T, bool> function)
        {
            this.count++;
            this.functions.Add(this.count, function);
            return this.count;
        }

        public void Unsubscribe(int id)
        {
            this.functions.Remove(id);
        }

        public bool AnyFunctionPasses(T value)
        {
            return this.functions.Any(x => x.Value.Invoke(value));
        }

        public void Dispose()
        {
            this.functions.Clear();
        }
    }
}
