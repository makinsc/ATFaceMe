using System.Collections.Generic;

namespace ApiBackend.Transversal.DTOs.PLC
{
    public class UsersBlob
    {
        public UsersBlob()
        {
            listaIdUsers = new List<string>();
        }
        public List<string> listaIdUsers { get; set; }
    }
}
