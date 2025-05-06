using System.ComponentModel.DataAnnotations;

namespace LibraryWebApp.Models
{
    public class Library
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Library name is required")]
        [StringLength(100, ErrorMessage = "Library name cannot exceed 100 characters")]
        [Display(Name = "Library Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<Reader> Readers { get; set; } = new List<Reader>();
    }
}
