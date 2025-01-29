namespace TaskWebApi.DTO
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public bool Status { get; set; } = false;

    }
}
