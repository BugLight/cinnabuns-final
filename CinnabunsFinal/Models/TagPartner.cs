using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinnabunsFinal.Models
{
    public class TagPartner
    {
        // Foreign key
        public int TagId { get; set; }
        // Foreign key
        public int PartnerId { get; set; }
    }
}
