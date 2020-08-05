using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tranlator.Models
{
    public class File
    {
        public int Id { get; set; }
        [Required]
        public Lang Lang { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
    }
}