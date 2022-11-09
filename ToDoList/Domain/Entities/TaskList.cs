using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Entities
{
    public class TaskList
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Название списка")]
        public string? Title { get; set; }

        [Required]
        public bool? Complited { get; set; }

        public User? User { get; set; }

        public List<TaskItem>? TaskItems { get; set; }
    }
}
