using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
        public bool ShouldSerializeINN()
        {
            return Type == "law";
        }

        // Web-site of company
        public string Site { get; set; }
        public bool ShouldSerializeSite()
        {
            return Type == "law";
        }

        // Surname of individual
        public string Surname { get; set; }
        public bool ShouldSerializeSurname()
        {
            return Type == "private";
        }

        // Patronymic of individual
        public string Patronymic { get; set; }
        public bool ShouldSerializePatronymic()
        {
            return Type == "private";
        }

        // Phone of individual
        public string Phone { get; set; }
        public bool ShouldSerializePhone()
        {
            return Type == "private";
        }

        // Email of individual
        public string Email { get; set; }
        public bool ShouldSerializeEmail()
        {
            return Type == "private";
        }

        // Description of individual
        public string Description { get; set; }

        [JsonIgnore]
        public bool Deleted { get; set; } = false;

        [JsonIgnore]
        public List<TagPartner> TagPartners { get; set; }

        [JsonIgnore]
        public List<EventPartner> EventPartners { get; set; }

        [JsonIgnore]
        public List<Task> Tasks { get; set; }

        [NotMapped]
        public List<int> TagIds => TagPartners?.Select(tp => tp.TagId).ToList();

        [NotMapped]
        public List<int> EventIds => EventPartners?.Select(ep => ep.EventId).ToList();

        [NotMapped]
        public string Type => INN == null ? "private" : "law";
    }
}
