using TaskWebApi.Classes;

namespace TaskWebApi.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetTaskByIdAsync(int taskId);
        Task<List<TaskItem>> GetTasksByUserIdAsync(int userId);
        Task<bool> UpdateTaskStatusAsync(TaskItem taskItem);
    }

}
