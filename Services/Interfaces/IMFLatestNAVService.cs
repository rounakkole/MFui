using MFui.Data;

namespace MFui.Services
{
    public interface IMFLatestNAVService
    {
        Task<List<SchemeLatestNAV>> GetSchemeLatestNAV(int _limit, int _offset);
    }
}
