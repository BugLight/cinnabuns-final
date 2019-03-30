using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

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
            return new PageResult<Tag>
            {
                Data = new PageFrameDb<Tag>().FrameDb(context.Tags.AsQueryable(), pageFrame).ToList(),
                TotalCount = context.Tags.Count()
            };
        }

        // Functions for adding tags
        [HttpPost]
        public ActionResult<Tag> AddTag([FromBody] Tag tag)
        {
            if (tag == null)
                return BadRequest();

            context.Tags.Add(tag);
            context.SaveChanges();

            return tag;
        }

        // Function for editing tags
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

            return tag;
        }

        // Function for deleting tags
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
