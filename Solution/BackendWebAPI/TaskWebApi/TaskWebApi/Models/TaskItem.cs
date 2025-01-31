using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskWebApi.Classes
{
    public class TaskItem
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public required string TaskName { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public bool Status { get; set; } = false;
    }

}
