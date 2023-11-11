using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1EJTS.PersonalBudgetApp.UserInterface
{
    interface IMenu
    {
        void RecordTransaction();
        Task ExecuteSerializationAsync();
        Task ExecuteDeSerializationAsync();
        void ExecuteGetCurrentBalance();
        void DisplayMainMenu();
    }
}
