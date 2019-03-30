using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CinnabunsFinal.Controllers
{
    [Route("api/events")]
    public class EventsController : Controller
    {
        private readonly AppContext context;

        public EventsController(AppContext context)
        {
            this.context = context;
        }

        // Functions for getting events
        [HttpGet]
        public PageResult<Event> GetEvents([FromQuery] PageFrame pageFrame, [FromQuery] DateTime? beginDate, [FromQuery] DateTime? endDate)
        {
            var query = from e in context.Events
                        select e;

            if (beginDate != null)
            {
                query = from e in query
                        where beginDate <= e.BeginDate
                        select e;
            }
            if (endDate != null)
            {
                query = from e in query
                        where endDate >= e.BeginDate
                        select e;
            }

            return new PageResult<Event>
            {       
                Data = new PageFrameDb<Event>().FrameDb(query, pageFrame).ToList(),
                TotalCount = context.Events.Count()
            };
        }

        // Functions for adding event
        [HttpPost]
        public ActionResult<Event> AddEvent([FromBody] Event e)
        {
            if (e == null)
                return BadRequest();

            context.Events.Add(e);
            context.SaveChanges();

            return e;
        }

        // Function for editing event
        [HttpPut("{id}")]
        public ActionResult<Event> EditEvent([FromBody] Event newE, int id)
        {
            if (newE == null)
                return BadRequest();

            var e = context.Events.Find(id);

            if (e == null)
                return NotFound();

            e.Name = newE.Name;
            e.BeginDate = newE.BeginDate;
            e.EndDate = newE.EndDate;
            e.Description = newE.Description;
            context.SaveChanges();

            return e;
        }

        // Function for deleting event
        [HttpDelete("{id}")]
        public ActionResult DeleteEvent(int id)
        {
            var e = context.Events.Find(id);

            if (e == null)
                return NotFound();

            context.Events.Remove(e);
            context.SaveChanges();

            return Ok();
        }
    }
}
