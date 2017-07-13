using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.Utilities
{
    public class Reacter : IDisposable
    {
        private int id;

        private Notifier notifier;

        public Reacter(Action reaction, bool reactOnce = false)
        {
            this.Reaction = reaction;
            this.ReactOnce = reactOnce;
        }

        public Action Reaction { get; }

        public bool ReactOnce { get; }

        public void Subscribe(Notifier notifier)
        {
            this.id = notifier.Subscribe(this.React);
            this.notifier = notifier;
        }

        public void Dispose()
        {
            this.notifier?.Unsubscribe(this.id);
        }

        private void React()
        {
            this.Reaction.Invoke();
            if (this.ReactOnce)
            {
                this.Dispose();
            }
        }
    }
}
