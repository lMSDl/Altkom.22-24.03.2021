using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.BankTransfer
{
    public interface IAccount
    {
        int AccountNumber { get; }
        double Balance { get; }

        void AddTransaction(Transaction transaction);
        IEnumerable<Transaction> GetTransactions();
        IEnumerable<Transaction> FilterTransactions(TransactionType type, IAccount account /*int accountNumber*/);

        void Transfer(IAccount toAccount, double amount, ITransactionProvider provider);
    }
}