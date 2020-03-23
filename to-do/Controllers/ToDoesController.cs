using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using to_do.Data;
using to_do.Models;

namespace to_do.Controllers
{
    [Authorize]
    public class ToDoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToDoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToDoes
        public IActionResult Index()
        {
            return View();
        }

        private IEnumerable<ToDo> GetToDo()
        {
            string currUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IdentityUser currUser = _context.Users.Find(currUserId);

            /*IEnumerable<ToDo> toDoList = await _context.ToDos.Where(x => x.User == currUser).ToListAsync();*/
            IEnumerable<ToDo> toDoList = _context.ToDos.ToList().Where(x => x.User == currUser);
            int completeCntr = 0;
            foreach (ToDo toDo in toDoList)
            {
                if (toDo.IsDone)
                    completeCntr++;
            }

            ViewBag.Percent = (toDoList.Count() > 0) ?
                Math.Round(100f * ((float)completeCntr / (float)toDoList.Count())) : 0.0;

            return toDoList;
        }

        public IActionResult RefreshToDoTable()
        {
            return PartialView("_ToDoCurrTable", GetToDo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxCreate([Bind("Id,Description")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                string currUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                IdentityUser currUser = await _context.Users.FindAsync(currUserId);
                toDo.User = currUser;
                toDo.IsDone = false;
                _context.Add(toDo);
                await _context.SaveChangesAsync();
                return PartialView("_ToDoCurrTable", GetToDo());
            }
            return View(toDo);
        }

        [HttpPost]
        public async Task<IActionResult> AjaxEdit(int? id, bool val)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }

            toDo.IsDone = val;
            _context.Update(toDo);
            await _context.SaveChangesAsync();
            return PartialView("_ToDoCurrTable", GetToDo());
        }

        [HttpPost]
        public async Task<IActionResult> AjaxDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }

            _context.ToDos.Remove(toDo);
            await _context.SaveChangesAsync();
            return PartialView("_ToDoCurrTable", GetToDo());
        }
    }
}
