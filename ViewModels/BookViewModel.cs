﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MVCBookStore.Models;

namespace MVCBookStore.ViewModels
{
    public class BookViewModel
    {

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [Display(Name = "Year Published")]
        public int YearPublished { get; set; }

        [Display(Name = "Number of pages")]
        public int NumPages { get; set; }

        public string? Description { get; set; }

        [StringLength(50)]
        public string? Publisher { get; set; }

        [Required(ErrorMessage = "Please choose front page image of the book.")]
        [Display(Name = "Front Page")]
        public IFormFile FrontPageVM { get; set; }

        [Display(Name = "Download Url")]
        public IFormFile DownloadUrlVM { get; set; }

        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public ICollection<BookGenre>? Genres { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public ICollection<UserBooks>? UserBooks { get; set; }

        [NotMapped]

        [Display(Name = "Average Rating")]
        public double Prosek
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