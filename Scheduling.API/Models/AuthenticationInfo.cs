using System;
using System.ComponentModel.DataAnnotations;

namespace Scheduling.API.Models
{
	public class AuthenticationInfo
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

