using Microsoft.AspNetCore.Mvc.Rendering;
using MVCBookStore.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCBookStore.ViewModels
{
    public class BookGenresEditViewModel
    {
        [Display(Name = "Download")]
        public IFormFile DownloadUrlEVM { get; set; }

        [Display(Name = "Front Page")]
        public IFormFile FrontPageEVM { get; set; }

        public Books Book { get; set; }
        public IEnumerable<int>? SelectedGenres { get; set; }
        public IEnumerable<SelectListItem>? GenreList { get; set; }
    }
}
