using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tranlator.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public List<Project> Projects { get; set; }
    }
}