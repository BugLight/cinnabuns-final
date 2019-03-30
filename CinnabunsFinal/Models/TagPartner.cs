namespace CinnabunsFinal.Models
{
    public class TagPartner
    {
        // Foreign key
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        // Foreign key
        public int PartnerId { get; set; }
        public Partner Partner { get; set; }
    }
}
