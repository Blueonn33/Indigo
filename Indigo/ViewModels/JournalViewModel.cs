using Indigo.Models;
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
        public IFormFile? ImageFile { get; set; }
        [Required]
        public string ISSN_Online { get; set; }
        [Required]
        public string ISSN_Print { get; set; }
        [Required]
        public string License { get; set; }
    }
}
