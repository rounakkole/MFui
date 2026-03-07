namespace MFui.Data
{
    public class SchemeMeta
    {
        [Newtonsoft.Json.JsonProperty("fund_house")]
        public string FundHouse { get; set; }

        [Newtonsoft.Json.JsonProperty("scheme_type")]
        public string SchemeType { get; set; }

        [Newtonsoft.Json.JsonProperty("scheme_category")]
        public string SchemeCategory { get; set; }

        [Newtonsoft.Json.JsonProperty("scheme_code")]
        public int SchemeCode { get; set; }

        [Newtonsoft.Json.JsonProperty("scheme_name")]
        public string SchemeName { get; set; }

        [Newtonsoft.Json.JsonProperty("isin_growth")]
        public string IsinGrowth { get; set; }

        [Newtonsoft.Json.JsonProperty("isin_div_reinvestment")]
        public string IsinDivReinvestment { get; set; }
    }
}
