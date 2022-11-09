using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;

namespace ToDoList.Domain
{
    public class ApplicationContext: IdentityDbContext<User>
    {
        public DbSet<TaskList>? TaskLists { get; set; }

        public DbSet<TaskItem>? TaskItems { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
