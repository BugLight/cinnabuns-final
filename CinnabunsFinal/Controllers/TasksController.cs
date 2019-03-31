using System.Linq;
using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinnabunsFinal.Controllers
{
    [Route("api/tasks")]
    public class TasksController : Controller
    {
        private readonly AppContext context;
        private readonly UserManager<User> userManager;

        public TasksController(AppContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        bool IsPartnerAllowed(int taskId, int partnerId)
        {
            var events = context.EventPartners
                    .Where(ep => ep.PartnerId == partnerId)
                    .Select(ep => ep.EventId);

            return context.Tasks
                .Include(t => t.Partner)
                .ThenInclude(p => p.EventPartners)
                .Where(t => t.Id == taskId && events.Contains(t.EventId))
                .Count() != 0;
        }

        // Functions for getting tasks
        [HttpGet]
        public PageResult<Task> GetTasks([FromQuery] PageFrame pageFrame, int? assignerId)
        {
            var q = context.Tasks
                .Include(t => t.Assigner)
                .Include(t => t.Responsible)
                .AsQueryable();

            if (assignerId != null)
            {
                q = q.Where(t => t.AssignerId == assignerId);
            }

            return new PageResult<Task>
            {
                Data = new PageFrameDb<Task>().FrameDb(q, pageFrame).ToList(),
                TotalCount = q.Count()
            };
        }

        // Functions for adding tasks
        [HttpPost]
        [Authorize(Roles="admin,organizer")]
        public ActionResult<Task> AddTask([FromBody] Task task)
        {
            if (task == null)
                return BadRequest();

            var user = Models.User.GetCurrentUser(userManager, HttpContext.User);
            var role = user.GetRole(userManager);

            task.Id = 0;
            task.PartnerId = context.EventPartners
                .Where(ep => ep.PartnerId == task.PartnerId)
                .Select(ep => ep.EventId).Contains(task.EventId) ? task.PartnerId : 0;
            task.AssignerId = user.Id;
            context.Tasks.Add(task);
            context.SaveChanges();

            return task;
        }

        // Function for editing task
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,organizer")]
        public ActionResult<Task> EditTask([FromBody] Task newTask, int id)
        {
            if (newTask == null)
                return BadRequest();

            var task = context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            task.Name = newTask.Name;
            task.EndDate = newTask.EndDate;
            task.Description = newTask.Description;
            task.ResponsibleId = newTask.ResponsibleId;
            task.PartnerId = IsPartnerAllowed(id, newTask.PartnerId) ? newTask.PartnerId : task.PartnerId;
            task.EventId = newTask.EventId;
            context.SaveChanges();

            return task;
        }

        [HttpPost("{id}/complete")]
        [Authorize]
        public ActionResult CompleteTask(int id)
        {
            var task = context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            task.Completed = true;
            context.SaveChanges();
            return Ok();
        }

        // Function for deleting task
        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var task = context.Tasks.Find(id);

            if (task == null)
                return NotFound();

            context.Tasks.Remove(task);
            context.SaveChanges();

            return Ok();
        }
    }
}
