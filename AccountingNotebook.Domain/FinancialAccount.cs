using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingNotebook.Domain
{
    public class FinancialAccount
    {
        public decimal Balance { get; private set; }

        public FinancialAccount()
        {
            Balance = 0M;
        }

        public FinancialTransaction ExecuteTransaction(TransactionType type, decimal amount)
        {
            var transaction = new FinancialTransaction(type, amount);

            if (transaction.Type == TransactionType.Credit)
                Credit(transaction);

            if (transaction.Type == TransactionType.Debit)
                Debit(transaction);

            return transaction;
        }

        private void Credit(FinancialTransaction transaction)
        {
            Balance += transaction.Amount;
        }

        private void Debit(FinancialTransaction transaction)
        {
            if (Balance - transaction.Amount < 0)
                throw new DomainException("can't debit more than current balance");

            Balance -= transaction.Amount;
        }
    }
}
