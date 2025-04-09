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
        public string ImageUrl { get; set; }
        [Required]
        public string ISSN_Online { get; set; }
        [Required]
        public string ISSN_Print { get; set; }
        [Required]
        public string License { get; set; }
        //[Required]
        //public int CategoryId { get; set; }
        //public IEnumerable<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();
    }
}
