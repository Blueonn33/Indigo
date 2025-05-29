using Indigo.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Indigo.Models
{
    public class Journal
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Заглавието е твърде кратко")]
        public string Title { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Описанието е твърде кратко")]
        public string Description { get; set; }
        [Required]
        public byte[]? ImageData { get; set; }
        [Required]
        public string? ImageMimeType { get; set; }
        [Required]
        public string ISSN_Online { get; set; }
        [Required]
        public string ISSN_Print { get; set; }
        [Required]
        public string License { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public ICollection<Tome> Tomes { get; set; }
    }
}
