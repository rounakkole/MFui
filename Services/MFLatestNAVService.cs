using MFui.Data;
using System.Net.Http;

namespace MFui.Services
{
    public class MFLatestNAVService : IMFLatestNAVService
    {
        public HttpClient? httpClient; // = new HttpClient();

        public async Task<List<SchemeLatestNAV>> GetSchemeLatestNAV(int _limit, int _offset)
        {
            string connectionString;
            if (httpClient is null)
            {
                httpClient = new HttpClient();
            }
            connectionString = "https://" + $@"api.mfapi.in/mf/latest?limit={_limit}&offset={_offset}";

            HttpResponseMessage response = await httpClient.GetAsync(connectionString);
            string json = await response.Content.ReadAsStringAsync();
            List<SchemeLatestNAV> schemeLatestNAVList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SchemeLatestNAV>>(json);

            return schemeLatestNAVList;
        }




    }
}
