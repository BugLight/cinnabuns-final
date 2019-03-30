namespace CinnabunsFinal.Models
{
    public class EventPartner
    {
        // Foreign key
        public int EventId { get; set; }
        public Event Event { get; set; }
        // Foreign key
        public int PartnerId { get; set; }
        public Partner Partner { get; set; }
    }
}
