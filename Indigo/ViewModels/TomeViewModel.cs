using System.ComponentModel.DataAnnotations;

namespace Indigo.ViewModels
{
    public class TomeViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
