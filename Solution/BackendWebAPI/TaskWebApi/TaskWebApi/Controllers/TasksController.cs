using Microsoft.AspNetCore.Mvc;
using TaskWebApi.DTO;
using TaskWebApi.Interface;

namespace TaskWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{userId}")]
        public IActionResult GetTasks(int userId)
        {
            var usersTaskDto = _taskService.GetTasks(userId);
            if (usersTaskDto == null)
                return NotFound($"User with ID {userId} not found.");

            return Ok(usersTaskDto);

        }

        [HttpPut]
        public IActionResult UpdateTaskStatus([FromBody] UpdateTaskRequestDto request)
        {
            var success = _taskService.UpdateTaskStatus(request.UserId, request.TaskId, request.NewStatus);
            if (!success)
                return BadRequest(new { Message = "Task not found or invalid update." });

            return Ok(new { Message = "Task status updated successfully." });
        }
    }

}
