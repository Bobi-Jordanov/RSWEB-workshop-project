using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBookStore.Data;
using MVCBookStore.Models;

namespace MVCBookStore.Controllers
{
    public class UserBooksController : Controller
    {
        private readonly MVCBookStoreContext _context;

        public UserBooksController(MVCBookStoreContext context)
        {
            _context = context;
        }

        // GET: UserBooks
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            var workshopImprovedContext = _context.UserBooks.Include(u => u.Books);
            return View(await workshopImprovedContext.ToListAsync());
        }

        // GET: UserBooks/Details/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserBooks == null)
            {
                return NotFound();
            }

            var userBooks = await _context.UserBooks
                .Include(u => u.Books)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBooks == null)
            {
                return NotFound();
            }

            return View(userBooks);
        }

        // GET: UserBooks/Create
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            ViewData["BooksId"] = new SelectList(_context.Books, "Id", "Title");
            return View();
        }

        // POST: UserBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([Bind("Id,AppUser,BooksId")] UserBooks userBooks)
        {
            if (ModelState.IsValid)
            {
                userBooks.AppUser = HttpContext.User.Identity.Name;
                _context.Add(userBooks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BooksId"] = new SelectList(_context.Books, "Id", "Title", userBooks.BooksId);
            return View(userBooks);
        }

        // GET: UserBooks/Edit/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserBooks == null)
            {
                return NotFound();
            }

            var userBooks = await _context.UserBooks.FindAsync(id);
            if (userBooks == null)
            {
                return NotFound();
            }
            ViewData["BooksId"] = new SelectList(_context.Books, "Id", "Title", userBooks.BooksId);
            return View(userBooks);
        }

        // POST: UserBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppUser,BooksId")] UserBooks userBooks)
        {
            if (id != userBooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userBooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserBooksExists(userBooks.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BooksId"] = new SelectList(_context.Books, "Id", "Title", userBooks.BooksId);
            return View(userBooks);
        }

        // GET: UserBooks/Delete/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserBooks == null)
            {
                return NotFound();
            }

            var userBooks = await _context.UserBooks
                .Include(u => u.Books)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBooks == null)
            {
                return NotFound();
            }

            return View(userBooks);
        }

        // POST: UserBooks/Delete/5
        [Authorize(Roles = "User")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserBooks == null)
            {
                return Problem("Entity set 'MVCBookStoreContext.UserBooks'  is null.");
            }
            var userBooks = await _context.UserBooks.FindAsync(id);
            if (userBooks != null)
            {
                _context.UserBooks.Remove(userBooks);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserBooksExists(int id)
        {
          return (_context.UserBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
