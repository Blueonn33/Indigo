using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Indigo.Models
{
    public class KeyWord
    {
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }

        [ForeignKey(nameof(Publication.Id))]
        public int PublicationId { get; set; }
        public Publication Publication { get; set; }
    }
}
