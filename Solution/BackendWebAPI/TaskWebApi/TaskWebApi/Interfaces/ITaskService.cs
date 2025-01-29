using TaskWebApi.Classes;
using TaskWebApi.DTO;

namespace TaskWebApi.Interface
{
    public interface ITaskService
    {
        UsersTaskDto? GetTasks(int userId);
        bool UpdateTaskStatus(int userId, int taskId, bool newStatus);
    }
}
