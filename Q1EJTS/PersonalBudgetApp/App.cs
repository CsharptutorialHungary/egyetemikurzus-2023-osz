using Q1EJTS.PersonalBudgetApp.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1EJTS.PersonalBudgetApp
{
    class App
    {
        public async static Task Main()
        {
            await new CLIUserInterface().Run();
        }
    }
}
