using MFui.Data;

namespace MFui.Services
{
    public interface ISchemeLatestNAVService
    {
        Task<NAVResponse> GetSchemeLatestNAV(string _schemeCode);
    }
}
