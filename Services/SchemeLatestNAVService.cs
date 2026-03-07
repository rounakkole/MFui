using MFui.Data;
using System.Net.Http;

namespace MFui.Services
{
    public class SchemeLatestNAVService : ISchemeLatestNAVService
    {
        public HttpClient? httpClient; // = new HttpClient();

        public async Task<NAVResponse> GetSchemeLatestNAV(string _schemeCode)
        {
            if (string.IsNullOrEmpty(_schemeCode))
            {
                return null;
            }

            string connectionString;
            if (httpClient is null)
            {
                httpClient = new HttpClient();
            }
            connectionString = "https://" + $@"api.mfapi.in/mf/{_schemeCode}/latest";

            HttpResponseMessage response = await httpClient.GetAsync(connectionString);
            string json = await response.Content.ReadAsStringAsync();

            NAVResponse navResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<NAVResponse>(json);
            return navResponse;
        }




    }
}
