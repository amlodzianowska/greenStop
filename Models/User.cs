using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace greenStop.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}
        [Required]
        public string FName {get;set;}
        [Required]
        public string LName {get;set;}
        [Required]
        [EmailAddress]
        public string Email {get;set;}
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password {get;set;} 

        //connection between users
        
        [InverseProperty("UserFollowed")]
        public List<Connection> Followers { get; set; }

        [InverseProperty("Follower")]
        public List<Connection> UsersFollowed { get; set; }
        public List<Plant> PlantsPosted {get;set;}

        public List<Comment> PlantsCommented { get; set; }
        public List<Like> PlantsLiked {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
    }
}