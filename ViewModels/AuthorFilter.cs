using MVCBookStore.Models;

namespace MVCBookStore.ViewModels
{
    public class AuthorFilter
    {
        public IList<Author> Authors { get; set; }

        public string FullNameAuthor { get; set; }

        public string Nationality { get; set; }
    }
}
