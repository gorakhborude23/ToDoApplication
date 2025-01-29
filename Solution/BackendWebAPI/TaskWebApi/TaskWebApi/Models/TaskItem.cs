namespace TaskWebApi.Classes
{
    public class TaskItem
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public int UserId { get; set; }
    }
}
