using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CinnabunsFinal.Controllers
{
    [Route("api/tags")]
    public class TagsController : Controller
    {
        private readonly AppContext context;

        public TagsController(AppContext context)
        {
            this.context = context;
        }

        // Functions for getting tags
        [HttpGet]
        public PageResult<Tag> GetTags([FromQuery] PageFrame pageFrame)
        {
            var q = context.Tags.Include(c => c.TagPartners);
            return new PageResult<Tag>
            {
                Data = new PageFrameDb<Tag>().FrameDb(q, pageFrame).ToList(),
                TotalCount = q.Count()
            };
        }

        // Functions for adding tag
        [HttpPost]
        public ActionResult<Tag> AddTag([FromBody] Tag tag)
        {
            if (tag == null)
                return BadRequest();

            context.Tags.Add(tag);
            context.SaveChanges();

            return context.Tags.Include(c => c.TagPartners).FirstOrDefault(c => c.Id == tag.Id);
        }

        // Function for editing tag
        [HttpPut("{id}")]
        public ActionResult<Tag> EditTag([FromBody] Tag newTag, int id)
        {
            if (newTag == null)
                return BadRequest();

            var tag = context.Tags.Find(id);

            if (tag == null)
                return NotFound();

            tag.Name = newTag.Name;
            context.SaveChanges();

            return context.Tags.Include(c => c.TagPartners).FirstOrDefault(c => c.Id == tag.Id);
        }

        // Function for deleting tag
        [HttpDelete("{id}")]
        public ActionResult DeleteTag(int id)
        {
            var tag = context.Tags.Find(id);

            if (tag == null)
                return NotFound();

            context.Tags.Remove(tag);
            context.SaveChanges();

            return Ok();
        }
    }
}
