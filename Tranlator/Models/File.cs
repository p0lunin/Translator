using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tranlator.Models
{
    public class File
    {
        [Required]
        public Lang Lang { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
    }
}