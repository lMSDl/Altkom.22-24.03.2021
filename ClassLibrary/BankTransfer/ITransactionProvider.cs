namespace ClassLibrary.BankTransfer
{
    public interface ITransactionProvider
    {
        Transaction To(IAccount account, double amount);
        Transaction From(IAccount account, double amount);
    }
}