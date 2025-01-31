using Microsoft.AspNetCore.Mvc;
using TaskWebApi.DTO;
using TaskWebApi.Services;

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
        public async Task<ActionResult<UsersTaskDto>> GetTasks(int userId)
        {
            try
            {
                var usersTaskDto = await _taskService.GetTasksByUserIdAsync(userId);
                if (usersTaskDto == null)
                    return NotFound($"User with ID {userId} not found.");

                return Ok(usersTaskDto);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }

        [HttpPut]
        public async Task<ActionResult> UpdateTaskStatus([FromBody] UpdateTaskRequestDto request)
        {
            try
            {
                var success = await _taskService.UpdateTaskStatusAsync(request);
                if (!success)
                    return BadRequest(new { Message = "Task not found or invalid update." });

                return Ok(new { Message = "Task status updated successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }

}
