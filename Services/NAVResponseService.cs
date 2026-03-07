using MFui.Data;
using System.Net.Http;

namespace MFui.Services
{
    public class NAVResponseService : INAVResponseService
    {
        public HttpClient? httpClient; // = new HttpClient();

        public async Task<NAVResponse> GetNAVResponse(string _schemeCode, DateOnly _startDate, DateOnly _endDate)
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
            connectionString = "https://" + $@"api.mfapi.in/mf/{_schemeCode}?startDate={_startDate.ToString("yyyy-MM-dd")}&endDate={_endDate.ToString("yyyy-MM-dd")}";

            HttpResponseMessage response = await httpClient.GetAsync(connectionString);
            string json = await response.Content.ReadAsStringAsync();
            NAVResponse? navResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<NAVResponse>(json);

            return navResponse;
        }


    }
}
