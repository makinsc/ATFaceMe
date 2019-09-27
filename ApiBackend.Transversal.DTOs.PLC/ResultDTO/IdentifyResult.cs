using Apibackend.Trasversal.DTOs;

namespace ApiBackend.Transversal.DTOs.PLC.ResultDTO
{
    public class IdentifyResult
    {
        public IdentifyResultCode ResultCode { get; set; }
        public UserDetail User { get; set; }
    }

    public enum IdentifyResultCode
    {
        PERSON_FOUND = 0,

        // Errors
        ERROR_NO_PERSON = 1,
        ERROR_UNKNOWN_PERSON = 2,
        ERROR_TOO_MANY_FACES = 3,
    }
}
