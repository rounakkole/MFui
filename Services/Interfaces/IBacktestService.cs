
namespace MFui.Services
{
    public interface IBacktestService
    {
        void NAVResponseCTOR(INAVResponseService _navResponseService);
        Task RunInvestments(int _schemeCode);
        Task RunAllInvestments();
    }
}
