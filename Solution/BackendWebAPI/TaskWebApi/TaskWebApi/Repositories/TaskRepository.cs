using Microsoft.EntityFrameworkCore;
using TaskWebApi.Classes;
using TaskWebApi.Data;

namespace TaskWebApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int taskId)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId);
        }

        public async Task<List<TaskItem>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<bool> UpdateTaskStatusAsync(TaskItem taskItem)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.UserId == taskItem.UserId && t.TaskId == taskItem.TaskId);
            if (task == null)
                return false;

            task.Status = taskItem.Status;
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
