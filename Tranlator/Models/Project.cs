using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tranlator.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public User Owner { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        [Required]
        public Lang MainLang { get; set; }
        public List<Lang> Langs { get; set; }
    }
}