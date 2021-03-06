﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Tranlator.Models
{
    public class AuthLink
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public DateTime Expires { get; set; }
    }
}