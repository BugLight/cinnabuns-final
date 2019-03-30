using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CinnabunsFinal.Controllers
{
    [Route("api/contacts")]
    public class ContactsController : Controller
    {
        private readonly AppContext context;
        private readonly UserManager<User> userManager;

        public ContactsController(AppContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        // Functions for getting contacts
        [HttpGet]
        [Authorize(Roles="admin,organizer,volunteer")]
        public PageResult<Contact> GetContacts([FromQuery] PageFrame pageFrame)
        {
            var q = context.Contacts.Include(c => c.ContactEmail).Include(c => c.ContactPhone);

            return new PageResult<Contact>
            {
                Data = new PageFrameDb<Contact>().FrameDb(q, pageFrame).ToList(),
                TotalCount = q.Count()
            };
        }

        // Functions for adding contact
        [HttpPost]
        [Authorize(Roles = "admin,organizer")]
        public ActionResult<Contact> AddContact([FromBody] Contact contact)
        {
            if (contact == null)
                return BadRequest();

            var user = Models.User.GetCurrentUser(userManager, HttpContext.User);
            var role = user.GetRole(userManager);

            context.Contacts.Add(contact);
            context.SaveChanges();

            return context.Contacts.Include(c => c.ContactEmail).Include(c => c.ContactPhone)
                .FirstOrDefault(c => c.Id == contact.Id);
        }

        // Function for editing contact
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,organizer")]
        public ActionResult<Contact> EditContact([FromBody] Contact newContact, int id)
        {
            if (newContact == null)
                return BadRequest();

            var contact = context.Contacts.Find(id);

            var user = Models.User.GetCurrentUser(userManager, HttpContext.User);
            var role = user.GetRole(userManager);

            if (contact == null)
                return NotFound();

            contact.Name = newContact.Name;
            contact.Surname = newContact.Surname;
            contact.Patronymic = newContact.Patronymic;
            contact.ContactPhone = newContact.ContactPhone;
            contact.ContactEmail = newContact.ContactEmail;
            context.SaveChanges();

            return context.Contacts.Include(c => c.ContactEmail).Include(c => c.ContactPhone)
                .FirstOrDefault(c => c.Id == contact.Id);
        }

        // Function for deleting contact
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,organizer")]
        public ActionResult DeleteContact(int id)
        {
            var contact = context.Contacts.Find(id);

            if (contact == null)
                return NotFound();

            var user = Models.User.GetCurrentUser(userManager, HttpContext.User);
            var role = user.GetRole(userManager);

            context.Contacts.Remove(contact);
            context.SaveChanges();

            return Ok();
        }
    }
}
