using ApiBackend.Transversal.DTOs.PLC;
using System;

namespace Apibackend.Trasversal.DTOs
{
    public class AuthResult : BaseDto
    {
        public string AccessToken { get; set; }
        public string idToken { get; set; }
        public string UserId { get; set; }
        public virtual DateTimeOffset ExpiresOn { get; set; }
    }
}
