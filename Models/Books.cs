using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBookStore.Models
{
    public class Books
    {
        [Required]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Year Published")]
        public int? YearPublished { get; set; }

        [Display(Name = "Number of Pages")]
        public int? NumPages { get; set; }


        public string? Description { get; set; }

        [StringLength(50)]
        public string? Publisher { get; set; }

        [Display(Name = "Front Page")]
        public string? FrontPage { get; set; }

        [Display(Name = "Download")]
        public string? DownloadUrl { get; set; }

        //external keys
        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        [Display(Name = "Genres")]
        public ICollection<BookGenre>? Genres { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<UserBooks>? UserBooks { get; set; }

        [NotMapped]

        [Display(Name = "Average Rating")]
        public double Average
        {
            get
            {
                if (Reviews == null)
                    return 0;

                double average = 0;
                int i = 0;
                if (Reviews != null)
                {
                    foreach (var review in Reviews)
                    {
                        average += review.Rating;
                        i++;
                    }

                    return Math.Round(average / i, 2);
                }
                return 0;
            }
        }
    }
}
