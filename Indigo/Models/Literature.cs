using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Indigo.Models
{
    public class Literature
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [ForeignKey(nameof(PublicationId))]
        public int PublicationId { get; set; }
        public Publication Publication { get; set; }
    }
}
