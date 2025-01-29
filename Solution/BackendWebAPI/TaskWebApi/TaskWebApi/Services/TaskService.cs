using System.Threading.Tasks;
using TaskWebApi.Classes;
using TaskWebApi.DTO;
using TaskWebApi.Interface;

namespace TaskWebApi.Services
{
    public class TaskService : ITaskService
    {
        private static readonly List<User> Users = new()
        {
            new User { UserId = 1, FirstName = "John", LastName = "Doe", Role = "Admin" },
            new User { UserId = 2, FirstName = "Jane", LastName = "Smith", Role = "Manager" },
            new User { UserId = 3, FirstName = "Bob", LastName = "Brown", Role = "Employee" },
        };

        private static readonly List<TaskItem> Tasks = new()
        {
            new TaskItem { TaskId = 1, TaskName = "Task 1", UserId = 1 },
            new TaskItem { TaskId = 2, TaskName = "Task 2", UserId = 1 },
            new TaskItem { TaskId = 3, TaskName = "Task 3", UserId = 1 },
            new TaskItem { TaskId = 4, TaskName = "Task 4", UserId = 1 },
            new TaskItem { TaskId = 5, TaskName = "Task 5", UserId = 1 },

            new TaskItem { TaskId = 6, TaskName = "Task 6", UserId = 2 },
            new TaskItem { TaskId = 7, TaskName = "Task 7", UserId = 2 },
            new TaskItem { TaskId = 8, TaskName = "Task 8", UserId = 2 },
            new TaskItem { TaskId = 9, TaskName = "Task 9", UserId = 2 },
            new TaskItem { TaskId = 10, TaskName = "Task 10", UserId = 2 },
            new TaskItem { TaskId = 11, TaskName = "Task 11", UserId = 2 },

            new TaskItem { TaskId = 12, TaskName = "Task 12", UserId = 3 },
            new TaskItem { TaskId = 13, TaskName = "Task 13", UserId = 3 },
            new TaskItem { TaskId = 14, TaskName = "Task 14", UserId = 3 },
            new TaskItem { TaskId = 15, TaskName = "Task 15", UserId = 3 },
            new TaskItem { TaskId = 16, TaskName = "Task 16", UserId = 3 },
            new TaskItem { TaskId = 17, TaskName = "Task 17", UserId = 3 },
            new TaskItem { TaskId = 18, TaskName = "Task 18", UserId = 3 },
        };

        public UsersTaskDto? GetTasks(int userId)
        {
            var user = Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
                return null;

            var userTasks = Tasks.Where(t => t.UserId == userId).ToList();

            var result = new UsersTaskDto
            {
                User = new UserDto
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role
                },
                Tasks = userTasks.Select(t => new TaskDto
                {
                    TaskId = t.TaskId,
                    TaskName = t.TaskName,
                    Status = t.Status
                }).ToList()
            };

            return result;
        }


        public bool UpdateTaskStatus(int userId, int taskId, bool newStatus)
        {
            var task = Tasks.FirstOrDefault(t => t.UserId == userId && t.TaskId == taskId);
            if (task == null)
                return false;

            task.Status = newStatus;
            return true;
        }
    }
}
