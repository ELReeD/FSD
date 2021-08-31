using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FSD.Context.DTO
{
    public class UserInfoDTO
    {
        [Required]
        public DateTime DateRegistration { get; set; }

        [Required]
        public DateTime DateLastActivity { get; set; }
    }
}
