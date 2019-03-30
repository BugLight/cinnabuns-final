using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinnabunsFinal.Models
{
    public class Interaction
    {
        // Primary key
        public int Id { get; set; }

        // Date of interaction
        public DateTime Date { get; set; }
        // Type of contact
        public int Type { get; set; }
        // Description of interaction
        public string Description { get; set; }

        // Id of responsible user
        [ForeignKey("User")]
        public int ResponsibleId { get; set; }
        // Id of contact
        public int ContactId { get; set; }
    }
}
