using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public PageResult<Contact> GetContacts([FromQuery] PageFrame pageFrame)
        {
            return new PageResult<Contact>
            {
                Data = new PageFrameDb<Contact>().FrameDb(context.Contacts.AsQueryable(), pageFrame).ToList(),
                TotalCount = context.Contacts.Count()
            };
        }

        // Functions for adding contact
        [HttpPost]
        public ActionResult<Contact> AddContact([FromBody] Contact contact)
        {
            if (contact == null)
                return BadRequest();

            var user = Models.User.GetCurrentUser(userManager, HttpContext.User);
            var role = user.GetRole(userManager);

            if (role != "admin" && contact.PartnerId != user.Id)
                return Forbid();

            context.Contacts.Add(contact);
            context.SaveChanges();

            return contact;
        }

        // Function for editing contact
        [HttpPut("{id}")]
        public ActionResult<Contact> EditContact([FromBody] Contact newContact, int id)
        {
            if (newContact == null)
                return BadRequest();

            var contact = context.Contacts.Find(id);

            var user = Models.User.GetCurrentUser(userManager, HttpContext.User);
            var role = user.GetRole(userManager);

            if (role != "admin" && contact.PartnerId != user.Id)
                return Forbid();

            if (contact == null)
                return NotFound();

            contact.Name = newContact.Name;
            contact.Surname = newContact.Surname;
            contact.Patronymic = newContact.Patronymic;
            contact.ContactPhone = newContact.ContactPhone;
            contact.ContactEmail = newContact.ContactEmail;
            context.SaveChanges();

            return contact;
        }

        // Function for deleting contact
        [HttpDelete("{id}")]
        public ActionResult DeleteContact(int id)
        {
            var contact = context.Contacts.Find(id);

            if (contact == null)
                return NotFound();

            var user = Models.User.GetCurrentUser(userManager, HttpContext.User);
            var role = user.GetRole(userManager);

            if (role != "admin" && contact.PartnerId != user.Id)
                return Forbid();

            context.Contacts.Remove(contact);
            context.SaveChanges();

            return Ok();
        }
    }
}
