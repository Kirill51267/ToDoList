using ToDoList.Domain.Entities;
using ToDoList.Models;

namespace ToDoList.Domain.Repositories.Abstract
{
    public interface ITaskItemRepository
    {
        Task<bool> Create(TaskItemViewModel entity);

        Task<bool> Update(TaskItemViewModel entity, int id);

        Task<bool> Delete(int id);

        Task<List<TaskItem>> GetAll(int listId);

        Task<bool> ChangeComplited(int id);
    }
}
