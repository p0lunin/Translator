using System.ComponentModel.DataAnnotations;

namespace Tranlator.Models
{
    public class Paragraph
    {
        public int Id { get; set; }
        [Required]
        public File File { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public string Content { get; set; }
    }
}