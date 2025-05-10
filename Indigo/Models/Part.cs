using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Indigo.Models
{
    public class Part
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [ForeignKey(nameof(TomeId))]
        public int TomeId { get; set; }
        public Tome Tome { get; set; }
        public ICollection<Publication> Publications { get; set; }
    }
}
