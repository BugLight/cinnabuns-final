using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinnabunsFinal.Models
{
    public class Partner
    {
        // Primary key
        public int Id { get; set; }

        // Name of company / Name of individual
        public string Name { get; set; }
        // INN of company
        public string INN { get; set; }
        // Web-site of company
        public string Site { get; set; }

        // Surname of individual
        public string Surname { get; set; }
        // Patronymic of individual
        public string Patronymic { get; set; }
        // Phone of individual
        public string Phone { get; set; }
        // Email of individual
        public string Email { get; set; }
        // Description of individual
        public string Description { get; set; }
    }
}
