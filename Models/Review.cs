using System.ComponentModel.DataAnnotations;

namespace MVCBookStore.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int BooksId { get; set; }
        public Books? Books { get; set; }

        [Required]
        [MaxLength(450)]
        [Display(Name = "User")]
        public string AppUser { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comment { get; set; }

        [Range(1, 10)]
        public int Rating { get; set; }
    }
}
