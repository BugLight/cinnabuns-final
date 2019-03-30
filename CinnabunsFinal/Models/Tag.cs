using System.Collections.Generic;

namespace CinnabunsFinal.Models
{
    public class Tag
    {
        // Primary key
        public int Id { get; set; }

        // Name of tag
        public string Name { get; set; }

        public List<TagPartner> TagPartners { get; set; }
    }
}
