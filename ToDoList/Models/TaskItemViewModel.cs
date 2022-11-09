using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ToDoList.Domain.Entities;

namespace ToDoList.Models
{
    public class TaskItemViewModel
    {
        [Required]
        [Display(Name = "Название задачи")]
        [MinLength(length: 1)]
        [MaxLength(length: 50)]
        public string? Title { get; set; }

        public int TaskListId { get; set; }


    }
}
