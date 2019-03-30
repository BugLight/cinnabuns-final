using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var query = from e in context.Events.Include(e => e.EventPartners)
                        orderby e.BeginDate descending
                        select e;

            if (beginDate != null)
            {
                query = from e in query
                        where beginDate <= e.BeginDate
                        orderby e.BeginDate descending
                        select e;
            }
            if (endDate != null)
            {
                query = from e in query
                        where endDate >= e.BeginDate
                        orderby e.BeginDate descending
                        select e;
            }

            return new PageResult<Event>
            {       
                Data = new PageFrameDb<Event>().FrameDb(query, pageFrame).ToList(),
                TotalCount = query.Count()
            };
        }

        // Functions for adding event
        [HttpPost]
        public ActionResult<Event> AddEvent([FromBody] Event e)
        {
            if (e == null)
                return BadRequest();

            e.Id = 0;
            context.Events.Add(e);
            context.SaveChanges();

            return context.Events.Include(x => x.EventPartners).FirstOrDefault(x => x.Id == e.Id);
        }

        [HttpGet("{id}")]
        public ActionResult<Event> GetEvent(int id)
        {
            return context.Events.Include(e => e.EventPartners)
                .FirstOrDefault(e => e.Id == id) ?? 
                (ActionResult<Event>)NotFound();
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

            return context.Events.Include(x => x.EventPartners).FirstOrDefault(x => x.Id == e.Id);
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
