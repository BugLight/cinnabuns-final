using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CinnabunsFinal.Controllers
{
    [Route("api/interactions")]
    public class InteractionsControllers : Controller
    {
        private readonly AppContext context;

        public InteractionsControllers(AppContext context)
        {
            this.context = context;
        }

        // Functions for getting interactions
        [HttpGet]
        [Authorize(Roles="admin,organizer")]
        public PageResult<Interaction> GetInteractions([FromQuery] PageFrame pageFrame)
        {
            var q = context.Interactions.AsQueryable();

            return new PageResult<Interaction>
            {
                Data = new PageFrameDb<Interaction>().FrameDb(q, pageFrame).ToList(),
                TotalCount = q.Count()
            };
        }

        // Functions for adding interaction
        [HttpPost]
        [Authorize(Roles="admin,organizer")]
        public ActionResult<Interaction> AddInteraction([FromBody] Interaction interaction)
        {
            if (interaction == null)
                return BadRequest();

            context.Interactions.Add(interaction);
            context.SaveChanges();

            return context.Interactions.Include(c => c.Contact).Include(c => c.Responsible)
                .FirstOrDefault(c => c.Id == interaction.Id);
        }

        // Function for editing interaction
        [HttpPut("{id}")]
        [Authorize(Roles="admin")]
        public ActionResult<Interaction> EditInteraction([FromBody] Interaction newInteraction, int id)
        {
            if (newInteraction == null)
                return BadRequest();

            var interaction = context.Interactions.Find(id);

            if (interaction == null)
                return NotFound();

            interaction.Date = newInteraction.Date;
            interaction.Type = newInteraction.Type;
            interaction.Description = newInteraction.Description;
            interaction.ResponsibleId = newInteraction.ResponsibleId;
            interaction.ContactId = newInteraction.ContactId;
            context.SaveChanges();

            return context.Interactions.Include(c => c.Contact).Include(c => c.Responsible)
                .FirstOrDefault(c => c.Id == interaction.Id);
        }

        // Function for deleting interaction
        [HttpDelete("{id}")]
        [Authorize(Roles="admin")]
        public ActionResult DeleteInteraction(int id)
        {
            var interaction = context.Interactions.Find(id);

            if (interaction == null)
                return NotFound();

            context.Interactions.Remove(interaction);
            context.SaveChanges();

            return Ok();
        }
    }
}
