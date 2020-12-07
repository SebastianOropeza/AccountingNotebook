using AccountingNotebook.Domain;
using NUnit.Framework;
using System;

namespace AccountingBook.Tests
{
    public class AccountingTests
    {
        public FinancialAccount Account { get; set; }
        [SetUp]
        public void Setup()
        {
            Account = new FinancialAccount();
        }

        [Test]
        public void Crediting_an_amount_is_added_to_accounts_balance()
        {
            var amount = 100.00M;

            var transaction = Account.ExecuteTransaction(TransactionType.Credit, amount);

            Assert.AreEqual(amount, transaction.Amount);
            Assert.AreEqual(transaction.Amount, Account.Balance);
            Assert.AreEqual(TransactionType.Credit, transaction.Type);
        }

        [Test]
        public void Cannot_credit_a_negative_amount_into_account()
        {
            var amount = -100.00M;

            Assert.Throws<DomainException>(() => Account.ExecuteTransaction(TransactionType.Credit, amount));
        }

        [Test]
        public void Debiting_amount_debits_that_amount_from_accounts_balance()
        {
            var amountToCredit = 100.00M;
            var amountToDebit = 50.00M;

            var creditTransaction = Account.ExecuteTransaction(TransactionType.Credit, amountToCredit);
            var debitTransaction = Account.ExecuteTransaction(TransactionType.Debit, amountToDebit);

            Assert.AreEqual(amountToCredit, creditTransaction.Amount);
            Assert.AreEqual(amountToDebit, debitTransaction.Amount);
            Assert.AreEqual(TransactionType.Debit, debitTransaction.Type);

            Assert.AreEqual((creditTransaction.Amount - debitTransaction.Amount), Account.Balance);
        }

        [Test]
        public void Cannot_debit_a_negative_amount_from_account()
        {
            var amount = -50.00M;

            Assert.Throws<DomainException>(() => Account.ExecuteTransaction(TransactionType.Debit, amount));
        }

        [Test]
        public void Cannot_debit_amount_greater_than_current_account_balance()
        {
            var amount = 150.00M;
            
            Assert.Throws<DomainException>(() => Account.ExecuteTransaction(TransactionType.Debit, amount));
        }
    }
}