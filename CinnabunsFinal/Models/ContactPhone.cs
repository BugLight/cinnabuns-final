using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinnabunsFinal.Models
{
    public class ContactPhone
    {
        // Primary key
        public int Id { get; set; }

        // Prefered type of communication (False - No, True - Yes)
        public bool Prefered { get; set; }
        // Phone
        public string Phone { get; set; }

        // Id of contact
        public int ContactId { get; set; }
    }
}
