using ToDoList.Domain.Entities;
using ToDoList.Models;

namespace ToDoList.Domain.Repositories.Abstract
{
    public interface ITaskListRepository
    {
        Task<bool> Create(TaskListViewModel entity, string userId);

        Task<bool> Update(TaskListViewModel entity,int id); 
        
        Task<bool> Delete(int id);

        Task<List<TaskList>> GetAll(string userid);

        Task<TaskList> GetById(int id);
    }
}
