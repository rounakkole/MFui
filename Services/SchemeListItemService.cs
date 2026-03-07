using MFui.Data;
using Newtonsoft.Json;
using System.Net.Http;

namespace MFui.Services
{
    public class SchemeListItemService : ISchemeListItemService
    {
        public HttpClient? httpClient; // = new HttpClient();

        public async Task<List<SchemeListItem>> GetSchemeListItem(int _limit, int _offset)
        {
            string connectionString;
            if (httpClient is null)
            {
                httpClient = new HttpClient();
            }
            connectionString = "https://" + $@"api.mfapi.in/mf?limit={_limit}&offset={_offset}";

            HttpResponseMessage response = await httpClient.GetAsync(connectionString);
            string json = await response.Content.ReadAsStringAsync();
            List<SchemeListItem> schemeListItemList = JsonConvert.DeserializeObject<List<SchemeListItem>>(json);

            return schemeListItemList;
        }



    }
}
