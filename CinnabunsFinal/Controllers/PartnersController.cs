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
        public PageResult<Partner> GetPartners([FromQuery] PageFrame pageFrame, [FromQuery] string[] tags)
        {
            if (tags.Length == 0)
            {
                var q = from p in context.Partners
                        select p;

                return new PageResult<Partner>
                {
                    Data = new PageFrameDb<Partner>().FrameDb(q, pageFrame).ToList(),
                    TotalCount = context.Partners.Count()
                };
            }

            var query = from p in context.Partners
                        join tp in context.TagPartners on p.Id equals tp.PartnerId
                        join t in context.Tags on tp.TagId equals t.Id
                        where tags.Contains(t.Name)
                        select p;

            return new PageResult<Partner>
            {
                Data = new PageFrameDb<Partner>().FrameDb(query, pageFrame).ToList(),
                TotalCount = query.Count()
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
        public ActionResult<Partner> EditPartner([FromBody] Partner newPartner, int id)
        {
            if (newPartner == null)
                return BadRequest();

            var partner = context.Partners.Find(id);

            if (partner == null)
                return NotFound();

            partner.Name = newPartner.Name;
            partner.INN = newPartner.INN;
            partner.Site = newPartner.Site;
            partner.Site = newPartner.Surname;
            partner.Patronymic = newPartner.Patronymic;
            partner.Phone = newPartner.Phone;
            partner.Email = newPartner.Email;
            partner.Description = newPartner.Description;
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
