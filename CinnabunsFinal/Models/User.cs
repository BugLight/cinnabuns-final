using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace CinnabunsFinal.Models
{
    public class User : IdentityUser<int>
    {
        [NotMapped]
        public readonly static string AdminRole = "admin";
        [NotMapped]
        public readonly static string OrganizerRole = "organizeer";
        [NotMapped]
        public readonly static string VolunteerRole = "volunteer";

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        override public int Id { get; set; }
        // Name of user
        public string Name { get; set; }
        // Surname of user
        public string Surname { get; set; }
        // Patronymic of user
        public string Patronymic { get; set; }
        // Phone of user
        public string Phone { get; set; }

        public static User GetCurrentUser(UserManager<User> userManager, ClaimsPrincipal principal)
        {
            return AsyncHelper.RunSync(() => userManager.GetUserAsync(principal));
        }

        public string GetRole(UserManager<User> userManager)
        {
            var roles = AsyncHelper.RunSync(() => userManager.GetRolesAsync(this));
            return roles.Count == 0 ? null : roles[0];
        }

        public bool IsRole(UserManager<User> userManager, List<string> roles)
        {
            return roles.Contains(GetRole(userManager));
        }
    }
}
