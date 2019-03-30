namespace CinnabunsFinal.Models
{
    public class Contact
    {
        // Primary key
        public int Id { get; set; }

        // Name of person
        public string Name { get; set; }
        // Surname of person
        public string Surname { get; set; }
        // Patronymic of person
        public string Patronymic { get; set; }

        // Id of partner
        public int PartnerId { get; set; }

        public ContactPhone ContactPhone { get; set; }
        public ContactEmail ContactEmail { get; set; }
    }
}
