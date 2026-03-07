using MFui.Data;

namespace MFui.Services
{
    public interface INAVResponseService
    {
        Task<NAVResponse> GetNAVResponse(string _schemeCode, DateOnly _startDate, DateOnly _endDate);
    }
}
