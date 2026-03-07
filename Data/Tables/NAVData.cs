using System.ComponentModel.DataAnnotations;

namespace MFui.Data
{
    public class NAVData
    {

        [Newtonsoft.Json.JsonProperty("date")]
        public string Date { get; set; }

        [Newtonsoft.Json.JsonProperty("nav")]
        public string NAV { get; set; }

    }
}
