namespace MFui.Data
{
    public class NAVResponse
    {
        [Newtonsoft.Json.JsonProperty("meta")]
        public SchemeMeta Meta { get; set; }

        [Newtonsoft.Json.JsonProperty("data")]
        public List<NAVData> Data { get; set; }
    }
}
