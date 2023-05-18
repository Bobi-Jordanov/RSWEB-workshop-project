namespace MVCBookStore.Models
{
    public class BookGenre
    {
        public int Id { get; set; }

        public int BooksId { get; set; }
        public Books? Books { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
