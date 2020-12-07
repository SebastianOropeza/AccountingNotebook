using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AccountingNotebook.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountingNotebook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly InMemoryTransactions inMemoryTransactions;
        private readonly InMemoryAccount financialAccount;

        public TransactionController(InMemoryTransactions transactions, InMemoryAccount financialAccount)
        {
            this.inMemoryTransactions = transactions;
            this.financialAccount = financialAccount;
        }

        [HttpGet]
        public ActionResult GetTransactions()
        {
            var transactions = inMemoryTransactions.Transactions
                .Select(e =>
                    new { 
                        e.Id,
                        Type = e.Type.ToString(),
                        e.Amount,
                        e.EffectiveDate
                    }
                );
            return Ok(transactions);
        }

        [HttpPost]
        public ActionResult CommitTransactionToAccount(TransactionRequest request)
        {
            TransactionType transactionType;
            if (!Enum.TryParse(request.Type, true, out transactionType))
                return BadRequest("the transaction type does not exist");

            FinancialTransaction transaction;

            lock(inMemoryTransactions.Transactions)
            {
                transaction = financialAccount.Account.ExecuteTransaction(transactionType, request.Amount);
                inMemoryTransactions.Transactions.Add(transaction);
            }

            return Ok(new { 
                transaction.Id, 
                Type = transaction.Type.ToString(), 
                transaction.Amount, 
                transaction.EffectiveDate 
            });
        }

        [HttpGet("{id}")]
        public ActionResult GetTransactionById(string id)
        {
            var transaction = inMemoryTransactions.Transactions.SingleOrDefault(e => e.Id.ToString() == id);
            return Ok(new
            {
                transaction.Id,
                Type = transaction.Type.ToString(),
                transaction.Amount,
                transaction.EffectiveDate
            });
        }
    }

    public class TransactionRequest {
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}
