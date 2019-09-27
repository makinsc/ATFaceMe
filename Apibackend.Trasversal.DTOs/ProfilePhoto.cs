using System.IO;

namespace Apibackend.Trasversal.DTOs
{
    public class ProfilePhoto
    {
        public string id { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public Stream photobytes { get; set; }
    }
}
