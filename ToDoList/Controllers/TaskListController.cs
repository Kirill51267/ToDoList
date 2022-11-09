using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Repositories.Abstract;
using ToDoList.Domain.Repositories.EntityFramework;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TaskListController : Controller
    {

        private readonly ITaskListRepository _taskListRepository;

        private readonly UserManager<User> _userManager;

        public TaskListController(UserManager<User> userManager, ITaskListRepository taskListRepository)
        {
            _userManager = userManager;
            _taskListRepository = taskListRepository;
        }

        public async Task<IActionResult> Index()
        {
            string user = User.FindFirstValue(ClaimTypes.Email);
            List<TaskList> responce = await _taskListRepository.GetAll(user);
            ViewData["TaskLists"] = responce;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddTaskList(TaskListViewModel model)
        {
            if (ModelState.IsValid)
            {
                string user = User.FindFirstValue(ClaimTypes.Email);
                if( await _taskListRepository.Create(model, user))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if( await _taskListRepository.Delete(id))
            {
                return RedirectToAction("Index");   
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateTaskList(TaskListViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                if (await _taskListRepository.Update(model, id))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    return RedirectToAction("Index", "Home");
                }
            }
            return NotFound();
        }
    }
}
