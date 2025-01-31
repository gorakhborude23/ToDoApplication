using AutoMapper;
using Moq;
using TaskWebApi.Classes;
using TaskWebApi.DTO;
using TaskWebApi.Exceptions;
using TaskWebApi.Mapper;
using TaskWebApi.Repositories;
using TaskWebApi.Services;

public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _mockTaskRepo;
    private readonly Mock<IUserRepository> _mockUserRepo;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _mockTaskRepo = new Mock<ITaskRepository>();
        _mockUserRepo = new Mock<IUserRepository>();

        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<TaskMappingProfile>());
        var mapper = mapperConfig.CreateMapper();
        var users = new List<User>
            {
                new User { UserId = 1, FirstName = "John", LastName = "Doe", Role = "Admin" },
                new User { UserId = 2, FirstName = "Jane", LastName = "Smith", Role = "Manager" }
            };
        var tasks = new List<TaskItem>
            {
                new TaskItem { TaskId = 1, TaskName = "Task 1", UserId = 1, Status = false },
                new TaskItem { TaskId = 2, TaskName = "Task 2", UserId = 1, Status = true },
                new TaskItem { TaskId = 3, TaskName = "Task 3", UserId = 2, Status = false }
            };
        _mockUserRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int userId) => users.Find(u => u.UserId == userId));
        _mockTaskRepo.Setup(repo => repo.GetTasksByUserIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int userId) => tasks.FindAll(t => t.UserId == userId));
        _mockTaskRepo.Setup(repo => repo.GetTaskByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int taskId) => tasks.Find(t => t.TaskId == taskId));
        _mockTaskRepo.Setup(repo => repo.UpdateTaskStatusAsync(It.IsAny<TaskItem>()))
            .ReturnsAsync((TaskItem task) =>
            {
                var existingTask = tasks.Find(t => t.TaskId == task.TaskId);
                if (existingTask == null) return false;
                existingTask.Status = task.Status;
                return true;
            });

        _taskService = new TaskService(_mockTaskRepo.Object, _mockUserRepo.Object, mapper);
    }

    [Fact]
    public async Task GetTasksByUserIdAsync_UserExists_ReturnsTasks()
    {
        var result = await _taskService.GetTasksByUserIdAsync(1);
        Assert.NotNull(result);
        Assert.Equal(2, result.Tasks.Count);
        Assert.Equal("John", result.User.FirstName);
    }

    [Fact]
    public async Task GetTasksByUserIdAsync_ShouldThrowUserNotFoundException_WhenUserDoesNotExist()
    {
        await Assert.ThrowsAsync<UserNotFoundException>(() => _taskService.GetTasksByUserIdAsync(99));
    }

    [Fact]
    public async Task UpdateTaskStatusAsync_ValidTask_UpdatesSuccessfully()
    {   
        var result = await _taskService.UpdateTaskStatusAsync(new UpdateTaskRequestDto() { UserId = 1, TaskId =1 , NewStatus =true});
        Assert.True(result);
        _mockTaskRepo.Verify(repo => repo.UpdateTaskStatusAsync(It.IsAny<TaskItem>()), Times.Once);
    }

    [Fact]
    public async Task UpdateTaskStatusAsync_ShouldThrowTaskNotFoundException_WhenTaskDoesNotExist()
    {
        await Assert.ThrowsAsync<TaskNotFoundException>(() => _taskService.UpdateTaskStatusAsync(new UpdateTaskRequestDto() { UserId = 1, TaskId = 99, NewStatus = true }));
    }

    [Fact]
    public async Task UpdateTaskStatusAsync_RepositoryThrowsException_ThrowsException()
    {
        _mockTaskRepo.Setup(repo => repo.UpdateTaskStatusAsync(It.IsAny<TaskItem>())).ThrowsAsync(new System.Exception("Database failure"));
        await Assert.ThrowsAsync<System.Exception>(() => _taskService.UpdateTaskStatusAsync(new UpdateTaskRequestDto() { UserId = 1, TaskId = 1, NewStatus = true }));
    }
}
