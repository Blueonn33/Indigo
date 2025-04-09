using System.ComponentModel.DataAnnotations;

namespace Indigo.ViewModels
{
    public class PublicationViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public bool IsApproved { get; set; } = false;
    }
}
