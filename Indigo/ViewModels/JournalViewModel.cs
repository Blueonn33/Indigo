using System.ComponentModel.DataAnnotations;

namespace Indigo.ViewModels
{
    public class JournalViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
