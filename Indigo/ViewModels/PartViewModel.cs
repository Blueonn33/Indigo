using System.ComponentModel.DataAnnotations;

namespace Indigo.ViewModels
{
    public class PartViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
