using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CinnabunsFinal.Models
{
    public class Tag
    {
        // Primary key
        public int Id { get; set; }

        // Name of tag
        public string Name { get; set; }

        [JsonIgnore]
        public List<TagPartner> TagPartners { get; set; }

        [NotMapped]
        public List<int> PartnerIds => TagPartners?.Select(x => x.PartnerId).ToList();
    }
}
