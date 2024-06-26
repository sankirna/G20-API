﻿using G20.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace G20.API.Models
{
    public class RegistrationRequest
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        public RoleEnum Role { get; set; }
    }
}
