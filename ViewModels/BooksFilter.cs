using Microsoft.AspNetCore.Mvc.Rendering;
using MVCBookStore.Models;

namespace MVCBookStore.ViewModels
{
    public class BooksFilter
    {
        public IList<Books> Books { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }
    }
}
