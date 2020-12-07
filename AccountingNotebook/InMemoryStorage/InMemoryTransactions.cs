using AccountingNotebook.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingNotebook
{
    public class InMemoryTransactions
    {
        private readonly List<FinancialTransaction> _transactions
        = new List<FinancialTransaction>();

        public List<FinancialTransaction> Transactions => _transactions;
    }
}
