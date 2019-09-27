using ApiBackend.Transversal.DTOs.PLC;
using Newtonsoft.Json;
using System.IO;

namespace Apibackend.Trasversal.DTOs
{
    public class ProfilePhoto : BaseDto
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }

        [JsonProperty("@odata.id")]
        public string OdataId { get; set; }

        [JsonProperty("@odata.mediaContentType")]
        public string OdataMediaContentType { get; set; }

        [JsonProperty("@odata.mediaEtag")]
        public string OdataMediaEtag { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        public Stream photobytes { get; set; }
    }

}
