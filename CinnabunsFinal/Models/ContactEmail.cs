using Newtonsoft.Json;

namespace CinnabunsFinal.Models
{
    public class ContactEmail
    {
        // Primary key
        public int Id { get; set; }

        // Prefered type of communication (False - No, True - Yes)
        public bool Prefered { get; set; }
        // Email
        public string Email { get; set; }

        // Id of contact
        public int ContactId { get; set; }
        [JsonIgnore]
        public Contact Contact;
    }
}
