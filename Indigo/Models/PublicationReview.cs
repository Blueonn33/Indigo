using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Indigo.Models
{
    public class PublicationReview
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public string ReviewerName { get; set; }
        [Required]
        public string ReviewerEmail { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
        [ForeignKey(nameof(PublicationId))]
        public int PublicationId { get; set; }
        public Publication Publication { get; set; }
    }
}
