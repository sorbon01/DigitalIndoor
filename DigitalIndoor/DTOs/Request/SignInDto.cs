﻿using System.ComponentModel.DataAnnotations;

namespace DigitalIndoor.DTOs.Request
{
    public class SignInDto
    {
        [MinLength(3)]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
