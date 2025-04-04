using System.ComponentModel.DataAnnotations;

namespace Indigo.ViewModels
{
    public class LiteratureViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
