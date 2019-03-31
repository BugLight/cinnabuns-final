using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CinnabunsFinal.DTO
{
    [DataContract]
    public class UserResponse
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public string Patronymic { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}
