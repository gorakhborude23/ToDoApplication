namespace TaskWebApi.DTO
{
    public class UsersTaskDto
    {
        public UserDto? User { get; set; }
        public List<TaskDto>? Tasks { get; set; }

    }
}
