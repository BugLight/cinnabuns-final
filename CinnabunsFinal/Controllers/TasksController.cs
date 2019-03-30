using System.Linq;
using CinnabunsFinal.DTO;
using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinnabunsFinal.Controllers
{
    [Route("api/tasks")]
    public class TasksController : Controller
    {
        private readonly AppContext context;

        public TasksController(AppContext context)
        {
            this.context = context;
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
    }
}
