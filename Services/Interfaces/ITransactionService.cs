
namespace MFui.Services
{
    public interface ITransactionService
    {
        void NAVResponseCTOR(INAVResponseService _navResponseService);
        Task CalculateTransactions(int _schemeCode);
        Task CalculateAllTransactions();
        Task ClearInvestments(int _schemeCode);
    }
}
