using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingNotebook
{
    public class InMemoryAccount
    {
        private readonly Domain.FinancialAccount _account
        = new Domain.FinancialAccount();

        public Domain.FinancialAccount Account => _account;
    }
}
