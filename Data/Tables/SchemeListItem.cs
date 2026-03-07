namespace MFui.Data
{
    public class SchemeListItem
    {
        //www.mfapi.in/docs/#/Schemes/get_mf
        [Newtonsoft.Json.JsonProperty("schemeCode")]
        public int SchemeCode { get; set; }
        [Newtonsoft.Json.JsonProperty("schemeName")]
        public string SchemeName { get; set; }
        [Newtonsoft.Json.JsonProperty("isinGrowth")]
        public string? IsinGrowth { get; set; }
        [Newtonsoft.Json.JsonProperty("isinDivReinvestment")]
        public string? IsinDivReinvestment { get; set; }
    }
}
