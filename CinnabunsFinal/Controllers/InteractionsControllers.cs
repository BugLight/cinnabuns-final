using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Mvc;
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
        public PageResult<Interaction> GetInteractions([FromQuery] PageFrame pageFrame)
        {
            return new PageResult<Interaction>
            {
                Data = new PageFrameDb<Interaction>().FrameDb(context.Interactions.AsQueryable(), pageFrame).ToList(),
                TotalCount = context.Interactions.Count()
            };
        }

        // Functions for adding interaction
        [HttpPost]
        public ActionResult<Interaction> AddInteraction([FromBody] Interaction interaction)
        {
            if (interaction == null)
                return BadRequest();

            context.Interactions.Add(interaction);
            context.SaveChanges();

            return interaction;
        }

        // Function for editing interaction
        [HttpPut("{id}")]
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

            return interaction;
        }

        // Function for deleting interaction
        [HttpDelete("{id}")]
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
