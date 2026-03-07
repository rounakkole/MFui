using MFui.Data;

namespace MFui.Services
{
    public interface ISearchListItemService
    {
        Task<List<SearchListItem>> GetSearchQuery(string _searchQuery);
        Task<List<SchemeListItem>> GetSearchQuery_new(string _searchQuery);
    }
}
