using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CinnabunsFinal.Models
{
    public class Event
    {
        // Primary key
        public int Id { get; set; }

        // Name of event
        public string Name { get; set; }
        // First date of event
        public DateTime BeginDate { get; set; }
        // Last date of event
        public DateTime EndDate { get; set; }
        // Description of event
        public string Description { get; set; }

        [JsonIgnore]
        public bool Deleted { get; set; } = false;

        public List<EventPartner> EventPartners { get; set; }
    }
}
