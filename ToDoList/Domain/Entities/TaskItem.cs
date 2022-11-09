using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Название задачи")]
        public string? Title { get; set; }


        [Required]
        public bool? Complited { get; set; }

        public TaskList? TaskList { get; set; }
    }
}
