using ApiBackend.Transversal.DTOs.PLC;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Apibackend.Trasversal.DTOs
{

    public class UserInfo : BaseDto
    {
        public string Address { get; set; }
    }

    public class FileInfo : BaseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SharingLink { get; set; }
    }

    public class Message : BaseDto
    {
        public string Subject { get; set; }
        public ItemBody Body { get; set; }
        public List<Recipient> ToRecipients { get; set; }
        public List<Attachment> Attachments { get; set; }
    }

    public class Recipient : BaseDto
    {
        public UserInfo EmailAddress { get; set; }
    }

    public class ItemBody : BaseDto
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
    }

    public class MessageRequest : BaseDto
    {
        public Message Message { get; set; }
        public bool SaveToSentItems { get; set; }
    }

    public class Attachment : BaseDto
    {
        [JsonProperty(PropertyName = "@odata.type")]
        public string ODataType { get; set; }
        public byte[] ContentBytes { get; set; }
        public string Name { get; set; }
    }

    public class PermissionInfo : BaseDto
    {
        public SharingLinkInfo Link { get; set; }
    }

    public class SharingLinkInfo : BaseDto
    {
        public SharingLinkInfo(string type)
        {
            Type = type;
        }

        public string Type { get; set; }
        public string WebUrl { get; set; }
    }

}
