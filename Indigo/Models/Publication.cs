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
        public string Content { get; set; }
        //public string UsedLiterature { get; set; } - отделен модел за използваната литература
        [Required]
        public string AuthorName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = false;

        [ForeignKey(nameof(JournalId))]
        public int JournalId { get; set; }
        public Journal Journal { get; set; }
        public ICollection<KeyWord> KeyWords { get; set; }
        public ICollection<Literature> Literatures { get; set; }

    }
}
