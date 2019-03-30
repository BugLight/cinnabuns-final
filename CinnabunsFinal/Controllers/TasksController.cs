using System.Linq;
using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        // Functions for getting tasks
        [HttpGet]
        public PageResult<Task> GetTasks([FromQuery] PageFrame pageFrame)
        {
            return new PageResult<Task>
            {
                Data = new PageFrameDb<Task>().FrameDb(context.Tasks.AsQueryable(), pageFrame).ToList(),
                TotalCount = context.Tasks.Count()
            };
        }

        // Functions for adding tasks
        [HttpPost]
        public ActionResult<Task> AddTask([FromBody] Task task)
        {
            var user = Models.User.GetCurrentUser(userManager, HttpContext.User);
            if (user == null)
                return Forbid();

            if (task == null)
                return BadRequest();

            context.Tasks.Add(task);
            context.SaveChanges();

            return task;
        }

        // Function for editing task
        [HttpPut("{id}")]
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
            task.AssignerId = newTask.AssignerId;
            task.PartnerId = newTask.PartnerId;
            task.EventId = newTask.EventId;
            context.SaveChanges();

            return task;
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
