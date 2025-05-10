using System.ComponentModel.DataAnnotations;

namespace Indigo.ViewModels
{
    public class PublicationReviewViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public string ReviewerName { get; set; }
        [Required]
        public string ReviewerEmail { get; set; }
    }
}
