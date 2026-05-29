
using MFui.Data;

namespace MFui.Services
{
    public class NavStateService
    {

        public List<SchemeListItem>? CachedSchemeListItem { get; set; }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 100;
        public string SearchQuery = string.Empty;
    }
}
