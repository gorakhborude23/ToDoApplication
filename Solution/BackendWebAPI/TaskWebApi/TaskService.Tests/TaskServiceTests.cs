using TaskWebApi.Services;

public class TaskServiceTests
{
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _taskService = new TaskService();
    }

    [Fact]
    public void GetTasks_ReturnsUserTasks_WhenUserExists()
    {
        int userId = 1;
        var result = _taskService.GetTasks(userId);
        Assert.NotNull(result);
        Assert.Equal(userId, result.User.UserId);
        Assert.NotEmpty(result.Tasks);
    }

    [Fact]
    public void GetTasks_ReturnsNull_WhenUserDoesNotExist()
    {
        int userId = 999;
        var result = _taskService.GetTasks(userId);
        Assert.Null(result);
    }

    [Fact]
    public void UpdateTaskStatus_ReturnsTrue_WhenTaskExists()
    {
        int userId = 1;
        int taskId = 1;
        bool newStatus = true;
        var result = _taskService.UpdateTaskStatus(userId, taskId, newStatus);
        Assert.True(result);
    }

    [Fact]
    public void UpdateTaskStatus_ReturnsFalse_WhenTaskDoesNotExist()
    {
        int userId = 1;
        int taskId = 999;
        bool newStatus = true;
        var result = _taskService.UpdateTaskStatus(userId, taskId, newStatus);
        Assert.False(result);
    }
}