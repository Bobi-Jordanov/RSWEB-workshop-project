using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBookStore.Data;
using MVCBookStore.Models;
using MVCBookStore.ViewModels;

namespace MVCBookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly MVCBookStoreContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BooksController(MVCBookStoreContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }




        // GET: Books, implement searching here!
        public async Task<IActionResult> Index(string title, string genre)
        {
            var booksQuery = _context.Books.
                Include(b => b.Author).
                Include(b => b.Reviews).
                Include(b => b.Genres).
                ThenInclude(b => b.Genre);
            var genresQuery = _context.BookGenre.Include(b => b.Genre).Include(b => b.Books);
            var genres = from n in genresQuery select n;
            var books = from m in booksQuery select m;
             

            if (!string.IsNullOrEmpty(title))
            {
                books = books.Where(x => x.Title.Contains(title));
            }
            if (!string.IsNullOrEmpty(genre))
            {
                genres = genres.Where(s => s.Genre.GenreName!.Contains(genre));
                var innerJoinQuery =
                 from m in books
                 join n in genres on m.Id equals n.BooksId
                 select new { m };
                ArrayList lista = new ArrayList();

                foreach (var bookAndGenre in innerJoinQuery)
                {

                    lista.Add(bookAndGenre.m);
                }

                ViewBag.lista = lista;
            }

            return View(await books.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Reviews)
                .Include(m => m.Genres)
                .ThenInclude(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Books book)
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "FullName", book.AuthorId);
            ViewData["Genres"] = new MultiSelectList(_context.Set<Genre>(), "Id", "GenreName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileNameIMG = UploadedFile(model);
                string uniqueFileNamePDF = UploadedPdf(model);

                Books book = new Books
                {
                    Title = model.Title,
                    YearPublished = model.YearPublished,
                    NumPages = model.NumPages,
                    Description = model.Description,
                    Publisher = model.Publisher,
                    FrontPage = uniqueFileNameIMG,
                    DownloadUrl = uniqueFileNamePDF,
                    AuthorId = model.AuthorId
                };
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FullName", model.AuthorId);
            return View();
        }

        //Uploading Images
        private string UploadedFile(BookViewModel model)
        {
            string uniqueFileNameIMG = null;
            string uniqueFileNamePDF = null;

            if (model.FrontPageVM != null)
            {
                string uploadsFolderIMG = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileNameIMG = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.FrontPageVM.FileName);
                string filePath = Path.Combine(uploadsFolderIMG, uniqueFileNameIMG);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.FrontPageVM.CopyTo(fileStream);
                }
            }
            return uniqueFileNameIMG;
        }

        //Downloading the File
        public async Task<IActionResult> GetPDF(string url)
        {
            string RenameFile = url;
            var path = Path.Combine(
            Directory.GetCurrentDirectory(), "wwwroot/pdfs/" + url);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, "application/pdf", RenameFile);
        }

        private string GetFileName(BookViewModel model)
        {
            return model.DownloadUrlVM.FileName;
        }

        //Uploading Files
        private string UploadedPdf(BookViewModel model)
        {

            string uniqueFileNamePDF = null;

            if (model.DownloadUrlVM != null)
            {
                string uploadsFolderPDF = Path.Combine(webHostEnvironment.WebRootPath, "pdfs");
                uniqueFileNamePDF = Guid.NewGuid().ToString() + "_" + model.DownloadUrlVM.FileName;
                string filePathh = Path.Combine(uploadsFolderPDF, uniqueFileNamePDF);
                using (var fileStreamm = new FileStream(filePathh, FileMode.Create))
                {
                    model.DownloadUrlVM.CopyTo(fileStreamm);
                }
            }

            return uniqueFileNamePDF;
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var books = _context.Books.Where(m => m.Id == id).Include(m => m.Genres).First();
            if (books == null)
            {
                return NotFound();
            }

            var genres = _context.Genre.AsEnumerable();
            genres = genres.OrderBy(s => s.GenreName);

            BookGenresEditViewModel viewmodel = new BookGenresEditViewModel
            {
                Book = books,
                GenreList = new MultiSelectList(genres, "Id", "GenreName"),
                SelectedGenres = books.Genres.Select(sa => sa.GenreId)
            };


            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FirstName", books.AuthorId);
            return View(viewmodel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, BookGenresEditViewModel viewmodel)
        {
            if (id != viewmodel.Book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.Book);
                    await _context.SaveChangesAsync();

                    //Genres
                    IEnumerable<int> newGenreList = viewmodel.SelectedGenres;
                    IEnumerable<int> prevGenreList = _context.BookGenre.Where(s => s.BooksId == id).Select(s => s.GenreId);
                    IQueryable<BookGenre> toBeRemoved = _context.BookGenre.Where(s => s.BooksId == id);
                    if (newGenreList != null)
                    {
                        toBeRemoved = toBeRemoved.Where(s => !newGenreList.Contains(s.GenreId));
                        foreach (int genreId in newGenreList)
                        {
                            if (!prevGenreList.Any(s => s == genreId))
                            {
                                _context.BookGenre.Add(new BookGenre { GenreId = genreId, BooksId = id });
                            }
                        }
                    }
                    _context.BookGenre.RemoveRange(toBeRemoved);

                    string uniqueFileNameIMG = null;
                    if (viewmodel.FrontPageEVM != null)
                    {
                        string uploadsFolderIMG = Path.Combine(webHostEnvironment.WebRootPath, "images");
                        uniqueFileNameIMG = viewmodel.FrontPageEVM.FileName;
                        string filePath = Path.Combine(uploadsFolderIMG, uniqueFileNameIMG);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            viewmodel.FrontPageEVM.CopyTo(fileStream);
                        }
                    }

                    viewmodel.Book.FrontPage = uniqueFileNameIMG; //filling the FrontPage from the Edit side

                    string uniqueFileNamePDF = null;
                    if (viewmodel.DownloadUrlEVM != null)
                    {
                        string uploadsFolderPDF = Path.Combine(webHostEnvironment.WebRootPath, "pdfs");
                        uniqueFileNamePDF = viewmodel.DownloadUrlEVM.FileName;
                        string filePath = Path.Combine(uploadsFolderPDF, uniqueFileNamePDF);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            viewmodel.DownloadUrlEVM.CopyTo(fileStream);
                        }
                    }

                    viewmodel.Book.DownloadUrl = uniqueFileNamePDF;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(viewmodel.Book.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FullName", viewmodel.Book.AuthorId);
            return View(viewmodel);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Reviews)
                .Include(m => m.Genres).ThenInclude(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Books/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'MVCBookStoreContext.Books'  is null.");
            }
            var books = await _context.Books.FindAsync(id);
            if (books != null)
            {
                _context.Books.Remove(books);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
