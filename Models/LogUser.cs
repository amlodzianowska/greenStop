using System;
using System.ComponentModel.DataAnnotations;

namespace greenStop.Models
{
    public class LogUser
    {
        [Required]
        [EmailAddress]
        public string LogEmail {get;set;}

        [Required]
        [DataType(DataType.Password)]
        public string LogPassword {get;set;}

    }
}