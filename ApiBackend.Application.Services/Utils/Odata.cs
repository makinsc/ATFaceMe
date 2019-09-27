using Newtonsoft.Json;

namespace ApiBackend.Infraestructura.Agents.MSGraph.Utils
{
    public class Odata<T>
    {
        [JsonProperty("@odata.context")]
        public string Metadata { get; set; }
        public T Value { get; set; }
    }
}
