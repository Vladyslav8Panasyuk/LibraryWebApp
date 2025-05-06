using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApp.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Book title is required")]
        [StringLength(200, ErrorMessage = "Book title cannot exceed 200 characters")]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters")]
        [Display(Name = "Author")]
        public string Author { get; set; } = string.Empty;

        [Display(Name = "Publication Year")]
        [Range(1, 9999, ErrorMessage = "Publication year must be between 1 and 9999")]
        public int PublicationYear { get; set; }

        [Display(Name = "Library")]
        public int LibraryId { get; set; }

        [ForeignKey("LibraryId")]
        public Library? Library { get; set; }
    }
}
