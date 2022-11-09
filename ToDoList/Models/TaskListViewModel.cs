using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ToDoList.Models
{
    public class TaskListViewModel
    {
        [Required]
        [Display(Name = "Название списка")]
        [MinLength(length:1)]
        [MaxLength(length:50)]
        public string? Title { get; set; }
    }
}
