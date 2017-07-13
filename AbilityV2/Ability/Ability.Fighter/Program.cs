using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Fighter
{
    class Program
    {
        static void Main(string[] args)
        {
            Ability.Core.AbilityBootstrapper.Load();
        }

        public static string Description(string unitName)
        {
            return "Fights with your " + unitName;
        }
    }
}
