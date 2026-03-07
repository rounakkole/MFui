namespace MFui.Data
{
    public class SearchListItem
    {
        //www.mfapi.in/docs/#/Schemes/get_mf
        [Newtonsoft.Json.JsonProperty("schemeCode")]
        public int SchemeCode { get; set; }
        [Newtonsoft.Json.JsonProperty("schemeName")]
        public string SchemeName { get; set; }
    }
}
