using Microsoft.AspNetCore.Identity;

namespace ToDoList.Domain.Entities
{
    public class User : IdentityUser
    {
        public List<TaskList>? TaskLists { get; set; }  
    }
}
