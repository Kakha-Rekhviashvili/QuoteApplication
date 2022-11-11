using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QuoteApplication.Models
{
    public class Quote
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string QuoteContent { get; set; }
        public string Author { get; set; }
        public bool? IsFavorite { get; set; }
    }
}
