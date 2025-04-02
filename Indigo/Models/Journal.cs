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
        public string ImageUrl { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

        public ICollection<Publication> Publications { get; set; }
    }
}
