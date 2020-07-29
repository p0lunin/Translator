using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tranlator.Models
{
    public class Lang
    {
        [Required]
        public Project Project { get; set; }
        [Required]
        public string Name { get; set; }
        public List<File> Files { get; set; }
    }
}