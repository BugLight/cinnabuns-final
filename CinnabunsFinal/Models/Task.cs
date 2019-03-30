using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinnabunsFinal.Models
{
    public class Task
    {
        // Primary key
        public int Id { get; set; }

        // Name of task
        public string Name { get; set; }
        // Date of task
        public DateTime EndDate { get; set; }
        // Description of task
        public string Description { get; set; }

        // Id of responsible user
        public int ResponsibleId { get; set; }
        // Id of assigner user
        public int AssignerId { get; set; }
        // Id of partner
        public int PartnerId { get; set; }
        // Id of event
        public int EventId { get; set; }
    }
}
