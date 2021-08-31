using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FSD.Context.Model
{
    public class UserInformation
    {
        public UserInformation()
        {

        }

        [Key]
        public string Id { get;  }

        [Required]
        public DateTime DateRegistration { get; set; }

        [Required]        
        public DateTime DateLastActivity { get; set; }

        public UserInformation(DateTime dateReg, DateTime dateLast)
        {
            DateRegistration = dateReg;
            DateLastActivity = dateLast;
        }


    }
}
