using Microsoft.AspNetCore.Identity;

namespace CinnabunsFinal.Models
{
    public class User : IdentityUser<int>
    {
        // Name of user
        public string Name { get; set; }
        // Surname of user
        public string Surname { get; set; }
        // Patronymic of user
        public string Patronymic { get; set; }
        // Phone of user
        public string Phone { get; set; }
    }
}
