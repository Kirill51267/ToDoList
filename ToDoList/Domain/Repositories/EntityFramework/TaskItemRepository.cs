using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Repositories.Abstract;
using ToDoList.Models;

namespace ToDoList.Domain.Repositories.EntityFramework
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ApplicationContext _db;

        public TaskItemRepository(ApplicationContext db)
        {
            _db = db;
        }

        

        public async Task<bool> Create(TaskItemViewModel entity)
        {
            try
            {
                TaskItem t = new TaskItem
                {
                    Title = entity.Title,
                    Complited = false,
                    TaskList = await _db.TaskLists.FirstOrDefaultAsync(tl => tl.Id == entity.TaskListId)
                };
                await _db.TaskItems.AddAsync(t);
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
                TaskItem taskItem = await _db.TaskItems.FirstOrDefaultAsync(tl => tl.Id == id);
                if (taskItem != null)
                {
                    _db.Entry(taskItem).State = EntityState.Deleted;
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

        public async Task<List<TaskItem>> GetAll(int listId)
        {
            TaskList taskList = _db.TaskLists.FirstOrDefault(tl => tl.Id == listId);
            List<TaskItem> responce = await _db.TaskItems.Where(ti => ti.TaskList == taskList)
                                                         .OrderBy(ti => ti.Complited)
                                                         .ToListAsync();
            return responce;
        }

        public async Task<bool> Update(TaskItemViewModel entity, int id)
        {
            try
            {
                TaskItem taskItem = await _db.TaskItems.FirstOrDefaultAsync(ti=>ti.Id==id);

                if (taskItem != null)
                {
                    taskItem.Title = entity.Title;
                    _db.TaskItems.Update(taskItem);
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
        public async Task<bool> ChangeComplited(int id)
        {
            try
            {
                TaskItem taskItem = await _db.TaskItems.FirstOrDefaultAsync(ti => ti.Id == id);

                if (taskItem != null)
                {
                    taskItem.Complited = !taskItem.Complited;
                    _db.TaskItems.Update(taskItem);
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
