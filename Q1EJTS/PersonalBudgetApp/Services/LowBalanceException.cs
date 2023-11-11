using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1EJTS.PersonalBudgetApp.Services
{
    class LowBalanceException : Exception
    {
        public LowBalanceException(string message) : base(message) {  }
    }
}
