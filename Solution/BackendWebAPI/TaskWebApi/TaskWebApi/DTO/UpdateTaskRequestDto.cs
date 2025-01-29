namespace TaskWebApi.DTO
{
    public class UpdateTaskRequestDto
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public bool NewStatus { get; set; } = false;
    }
}
