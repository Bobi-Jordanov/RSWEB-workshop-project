using System.ComponentModel.DataAnnotations;

namespace MVCBookStore.Models
{
    public class UserBooks
    {
        public int Id { get; set; }

        [Required]
        [StringLength(450)]
        [Display(Name = "User")]
        public string AppUser { get; set; }

        public int BooksId { get; set; }

        public Books? Books { get; set; }
    }
}
