using Newtonsoft.Json;
using System.Collections.Generic;

namespace Apibackend.Trasversal.DTOs
{
    public class PaginatedUserDetail
    {
        public List<UserDetail> value { get; set; }

        [JsonProperty(PropertyName = "@odata.nextLink")]
        public string ODataNextLink { get; set; }
    }
}
