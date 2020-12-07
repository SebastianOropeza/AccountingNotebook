using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingNotebook.Domain
{
    public class FinancialTransaction
    {
        public Guid Id { get; }
        public decimal Amount { get; }
        public TransactionType Type { get; }
        public DateTime EffectiveDate { get; }

        public FinancialTransaction(TransactionType type, decimal amount)
        {
            if (amount < 0)
                throw new DomainException("amount can't be negative");

            Id = Guid.NewGuid();
            Amount = amount;
            Type = type;
            EffectiveDate = DateTime.Now;
        }
    }
}
