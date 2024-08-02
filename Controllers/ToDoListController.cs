using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDO.Data;
using ToDO.Models;

namespace ToDO.Controllers
{
    public class ToDoListController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index(int id)
        {
            var UserList = context.ToDoItems.Where(e => e.UserId == id).ToList();

            ViewData["Id"] = id;
            ViewData["Name"] = context.Users.Find(id)?.Name;
          
            return View(UserList);
        }

        #region Add New Task
        public IActionResult Create(int UserId) 
        {
            
            ViewData["ID"] = UserId;
            ViewData["Name"] = context.Users.Find(UserId)?.Name;
           
            return View(new ToDoItem());
        }

        [HttpPost]
        public IActionResult Create(ToDoItem toDoItem)
        {
            if(ModelState.IsValid)
            {
                context.ToDoItems.Add(toDoItem);
                context.SaveChanges();

                TempData["Sucsses"] = true;
                TempData["TsakName"] = toDoItem.Title;

                return RedirectToAction("Index",new {id = toDoItem.UserId});
            }

            ViewData["ID"] = toDoItem.UserId;
            ViewData["Name"] = context.Users.Find(toDoItem.UserId)?.Name;

            return View(toDoItem);
        }
        #endregion

        #region Edit Task

        public IActionResult Edit(int ItemId)
        {
            return View(context.ToDoItems.Include(e => e.User)
                .FirstOrDefault(e => e.Id == ItemId));
        }

        [HttpPost]
        public IActionResult Edit(ToDoItem toDoItem)
        {
            if(ModelState.IsValid)
            {
                context.ToDoItems.Update(toDoItem);
                context.SaveChanges();
                return RedirectToAction("Index", new {id = toDoItem.UserId});

            }
            return View(context.ToDoItems.Include(e => e.User)
                .FirstOrDefault(e => e.Id == toDoItem.Id));
        }

        #endregion

        #region Delete Task

        public IActionResult Delete(int ItemId)
        {
            var item = context.ToDoItems.Find(ItemId);
            var userId = item.UserId;
            context.ToDoItems.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index", new { id = userId });
        }

        #endregion

    }
}
