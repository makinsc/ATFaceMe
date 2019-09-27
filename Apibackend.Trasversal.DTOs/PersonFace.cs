using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apibackend.Trasversal.DTOs
{
    public class PersonFace
    {
        public PersonFace() { }
        ~PersonFace() { }
        public string Name { get; set; }
        public Guid[] PersistedFaceIds { get; set; }
        public Guid PersonId { get; set; }
        public string UserData { get; set; }
      
    }
}
