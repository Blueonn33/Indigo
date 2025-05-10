using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Indigo.Models
{
    public class Publication
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = false;
        public byte[]? PdfFileData { get; set; }
        public string? PdfMimeType { get; set; }
        public string? PdfFileName { get; set; }
        [ForeignKey(nameof(PartId))]
        public int PartId { get; set; }
        public Part Part { get; set; }
        public ICollection<KeyWord> KeyWords { get; set; }
        public ICollection<Literature> Literatures { get; set; }
        public ICollection<PublicationReview> PublicationReviews { get; set; }
    }
}
