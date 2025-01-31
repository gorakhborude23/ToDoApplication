using AutoMapper;
using TaskWebApi.DTO;
using TaskWebApi.Exceptions;
using TaskWebApi.Repositories;

namespace TaskWebApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IUserRepository userRepository, IMapper mapper)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UsersTaskDto?> GetTasksByUserIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException($"User with ID {userId} not found.");
            }

            var tasks = await _taskRepository.GetTasksByUserIdAsync(userId);
            var taskDtos = _mapper.Map<List<TaskDto>>(tasks);
            var userDto = _mapper.Map<UserDto>(user);

            return new UsersTaskDto
            {
                User = userDto,
                Tasks = taskDtos
            };
        }

        public async Task<bool> UpdateTaskStatusAsync(UpdateTaskRequestDto updateRequest)
        {
            var user = await _userRepository.GetByIdAsync(updateRequest.UserId);
            if (user == null)
            {
                throw new UserNotFoundException($"User with ID {updateRequest.UserId} not found.");
            }

            var task = await _taskRepository.GetTaskByIdAsync(updateRequest.TaskId);
            if (task == null || task.UserId != updateRequest.UserId)
            {
                throw new TaskNotFoundException($"Task with ID {updateRequest.TaskId} for user {updateRequest.UserId} not found.");
            }

            task.Status = updateRequest.NewStatus;
            return await _taskRepository.UpdateTaskStatusAsync(task);
        }
    }

}
