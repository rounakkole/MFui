using MFui.Data;

namespace MFui.Services
{
    public interface ISchemeListItemService
    {

        Task<List<SchemeListItem>> GetSchemeListItem(int _limit, int _offset);
    }
}
