using TaskWebApi.DTO;

namespace TaskWebApi.Services
{
    public interface ITaskService
    {
        Task<UsersTaskDto?> GetTasksByUserIdAsync(int userId);
        Task<bool> UpdateTaskStatusAsync(UpdateTaskRequestDto updateRequest);
    }
}
