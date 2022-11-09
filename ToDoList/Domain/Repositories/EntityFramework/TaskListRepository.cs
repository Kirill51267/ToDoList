using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Repositories.Abstract;
using ToDoList.Models;

namespace ToDoList.Domain.Repositories.EntityFramework
{
    public class TaskListRepository : ITaskListRepository
    {

        private readonly ApplicationContext _db;

        public TaskListRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(TaskListViewModel entity,string userId)
        {
            try {
                TaskList t = new TaskList()
                {
                    Title = entity.Title,
                    Complited = false,
                    User = await _db.Users.FirstOrDefaultAsync(u => u.Email == userId)
                };
                await _db.TaskLists.AddAsync(t);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> Delete(int id)
        {
            try
            {
                TaskList taskList = _db.TaskLists.FirstOrDefault(t => t.Id == id);
                if (taskList != null)
                {
                    _db.Entry(taskList).State = EntityState.Deleted;
                    await _db.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<TaskList>> GetAll(string userId)
        {
            User user = _db.Users.FirstOrDefault(u => u.Email == userId);
            List<TaskList> responce = await _db.TaskLists.Where(x => x.User == user)
                                                         .ToListAsync();
            return responce;
        }

        public async Task<TaskList> GetById(int id)
        {
            TaskList responce = await _db.TaskLists.FirstOrDefaultAsync(r=>r.Id == id);
            return responce;
        }

        public async Task<bool> Update(TaskListViewModel entity, int id)
        {
            try
            {
                TaskList taskList = await this.GetById(id);

                if (taskList != null)
                {
                    taskList.Title=entity.Title;
                    _db.TaskLists.Update(taskList);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
