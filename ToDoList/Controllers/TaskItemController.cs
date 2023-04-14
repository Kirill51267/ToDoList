using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Repositories.Abstract;
using ToDoList.Domain.Repositories.EntityFramework;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    //test
    public class TaskItemController : Controller
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public TaskItemController(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems(int id)
        {
            List<TaskItem> responce = await _taskItemRepository.GetAll(id);
            ViewData["TaskItems"] = responce;
            ViewData["ListId"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTaskItem(TaskItemViewModel model,int listId)
        {
            model.TaskListId = listId;
            if (ModelState.IsValid)
            {
                if (await _taskItemRepository.Create(model))
                {
                    return RedirectToAction("GetAllItems", "TaskItem",new { id = model.TaskListId });
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id,int listId)
        {
            if (await _taskItemRepository.Delete(id))
            {
                return RedirectToAction("GetAllItems", "TaskItem", new { id = listId });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTaskItem(TaskItemViewModel model, int id,int listId)
        {
            if (ModelState.IsValid)
            {
                if (await _taskItemRepository.Update(model, id))
                {
                    return RedirectToAction("GetAllItems", "TaskItem", new { id = listId });
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeComplited( int id,int listId)
        {
            if(await _taskItemRepository.ChangeComplited(id))
            {
                return RedirectToAction("GetAllItems", "TaskItem", new { id = listId });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
