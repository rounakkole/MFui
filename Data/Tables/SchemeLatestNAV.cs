namespace MFui.Data
{
    public class SchemeLatestNAV
    {

        [Newtonsoft.Json.JsonProperty("schemeCode")]
        public int SchemeCode { get; set; }

        [Newtonsoft.Json.JsonProperty("schemeName")]
        public string SchemeName { get; set; }

        [Newtonsoft.Json.JsonProperty("fundHouse")]
        public string FundHouse { get; set; }

        [Newtonsoft.Json.JsonProperty("schemeType")]
        public string SchemeType { get; set; }

        [Newtonsoft.Json.JsonProperty("schemeCategory")]
        public string SchemeCategory { get; set; }

        [Newtonsoft.Json.JsonProperty("isinGrowth")]
        public string IsinGrowth { get; set; }

        [Newtonsoft.Json.JsonProperty("isinDivReinvestment")]
        public string IsinDivReinvestment { get; set; }

        [Newtonsoft.Json.JsonProperty("nav")]
        public string NAV { get; set; }

        [Newtonsoft.Json.JsonProperty("date")]
        public string Date { get; set; }
    }
}
