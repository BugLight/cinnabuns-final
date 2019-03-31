using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
                var q = from p in context.Partners.Include(p => p.EventPartners).Include(p => p.TagPartners)
                        select p;

                return new PageResult<Partner>
                {
                    Data = new PageFrameDb<Partner>().FrameDb(q, pageFrame).ToList(),
                    TotalCount = context.Partners.Count()
                };
            }

            var query = from p in context.Partners.Include(p => p.EventPartners).Include(p => p.TagPartners)
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

        [HttpGet("{id}")]
        public ActionResult<Partner> GetPartner(int id)
        {
            return context.Partners.Include(p => p.EventPartners).Include(p => p.TagPartners)
                .FirstOrDefault(p => p.Id == id) ?? (ActionResult<Partner>)NotFound();
        }

        // Functions for adding partner
        [HttpPost]
        [Authorize(Roles="admin")]
        public ActionResult<Partner> AddPartner([FromBody] Partner partner)
        {
            if (partner == null)
                return BadRequest();

            context.Partners.Add(partner);
            context.SaveChanges();

            return context.Partners.Include(p => p.EventPartners).Include(p => p.TagPartners)
                .FirstOrDefault(p => p.Id == partner.Id);
        }

        // Function for editing partner
        [HttpPut("{id}")]
        [Authorize(Roles="admin")]
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

            return context.Partners.Include(p => p.EventPartners).Include(p => p.TagPartners)
                .FirstOrDefault(p => p.Id == partner.Id);
        }

        // Function for deleting partner
        [HttpDelete("{id}")]
        [Authorize(Roles="admin")]
        public ActionResult DeletePartner(int id)
        {
            var partner = context.Partners.Include(p => p.EventPartners).Include(p => p.TagPartners)
                .FirstOrDefault(p => p.Id == id);

            if (partner == null)
                return NotFound();

            context.Partners.Remove(partner);
            context.SaveChanges();

            return Ok();
        }

        // Functions for getting the best partners
        [HttpGet("top")]
        public List<Partner> GetTopPartners()
        {
            var keys = from p in context.Partners
                       join ep in context.EventPartners on p.Id equals ep.PartnerId
                       group p by p.Id into table
                       select new { table.Key, Count = table.Count() };

            var query = (from p in context.Partners.Include(p => p.EventPartners).Include(p => p.TagPartners)
                         join k in keys on p.Id equals k.Key
                         orderby k.Count descending
                         select p).Take(10);

            return query.ToList();
        }
    }
}
