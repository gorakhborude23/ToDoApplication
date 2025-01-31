using System.ComponentModel.DataAnnotations;

namespace TaskWebApi.Classes
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string Role { get; set; }

        public List<TaskItem> Tasks { get; set; } = new();
    }

}
