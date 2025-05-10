using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Indigo.Models
{
    public class Tome
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey(nameof(JournalId))]
        public int JournalId { get; set; }
        public Journal Journal { get; set; }
        public ICollection<Part> Parts { get; set; }
    }
}
