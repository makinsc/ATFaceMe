using Apibackend.Trasversal.DTOs;
using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace ApiBackend.Application.Core.MSGraphAuth
{
    public interface IAuthProvider
    {
        Task<AuthResult> GetUserAccessTokenAsync();
    }
}