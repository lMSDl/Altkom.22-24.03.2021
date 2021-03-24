namespace ClassLibrary.BankTransfer
{
    public class Transaction
    {
        public TransactionType Type { get; }
        public IAccount Account { get; }
        public double Amount { get; }
    }
}