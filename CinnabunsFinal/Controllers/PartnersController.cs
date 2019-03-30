using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CinnabunsFinal.Controllers
{
    [Route("api/partners")]
    public class PartnersController : Controller
    {
        private readonly AppContext context;

        public PartnersController(AppContext context)
        {
            this.context = context;
        }

        // Functions for getting partners
        [HttpGet]
        public PageResult<Partner> GetPartners([FromQuery] PageFrame pageFrame)
        {
            return new PageResult<Partner>
            {
                Data = new PageFrameDb<Partner>().FrameDb(context.Partners.AsQueryable(), pageFrame).ToList(),
                TotalCount = context.Partners.Count()
            };
        }

        // Functions for adding partner
        [HttpPost]
        public ActionResult<Partner> AddPartner([FromBody] Partner partner)
        {
            if (partner == null)
                return BadRequest();

            context.Partners.Add(partner);
            context.SaveChanges();

            return partner;
        }

        // Function for editing partner
        [HttpPut("{id}")]
        public ActionResult<Partner> EditPartner([FromBody] Tag newPartner, int id)
        {
            if (newPartner == null)
                return BadRequest();

            var partner = context.Partners.Find(id);

            if (partner == null)
                return NotFound();

            partner.Name = newPartner.Name;
            context.SaveChanges();

            return partner;
        }

        // Function for deleting partner
        [HttpDelete("{id}")]
        public ActionResult DeletePartner(int id)
        {
            var partner = context.Partners.Find(id);

            if (partner == null)
                return NotFound();

            context.Partners.Remove(partner);
            context.SaveChanges();

            return Ok();
        }
    }
}
