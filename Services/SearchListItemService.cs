using MFui.Data;
using System.Net.Http;

namespace MFui.Services
{
    public class SearchListItemService : ISearchListItemService
    {
        public HttpClient? httpClient; // = new HttpClient();

        public async Task<List<SearchListItem>> GetSearchQuery(string _searchQuery)
        {
            if (string.IsNullOrEmpty(_searchQuery))
            {
                return null;
            }

            string connectionString;
            if (httpClient is null)
            {
                httpClient = new HttpClient();
            }
            connectionString = "https://" + $@"api.mfapi.in/mf/search?q={_searchQuery}";

            HttpResponseMessage response = await httpClient.GetAsync(connectionString);
            string json = await response.Content.ReadAsStringAsync();
            List<SearchListItem> searchListItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SearchListItem>>(json);

            return searchListItemList;
        }

        public async Task<List<SchemeListItem>> GetSearchQuery_new(string _searchQuery)
        {
            if (string.IsNullOrEmpty(_searchQuery))
            {
                return null;
            }

            string connectionString;
            if (httpClient is null)
            {
                httpClient = new HttpClient();
            }
            connectionString = "https://" + $@"api.mfapi.in/mf/search?q={_searchQuery}";

            HttpResponseMessage response = await httpClient.GetAsync(connectionString);
            string json = await response.Content.ReadAsStringAsync();
            List<SchemeListItem> searchListItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SchemeListItem>>(json);

            return searchListItemList;
        }


    }
}
