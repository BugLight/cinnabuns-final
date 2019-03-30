using System;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("User")]
        public int ResponsibleId { get; set; }
        [ForeignKey("ResponsibleId")]
        public User Responsible { get; set; }
        // Id of assigner user
        [ForeignKey("User")]
        public int AssignerId { get; set; }
        [ForeignKey("User")]
        public User Assigner { get; set; }
        // Id of partner
        public int PartnerId { get; set; }
        // Id of event
        public int EventId { get; set; }
    }
}
