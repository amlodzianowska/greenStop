using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace greenStop.Models
{
    public class Plant
    {
        [Key]
        public int PlantId {get;set;}
        [Required]
        public string PlantName {get;set;}
        [Required]
        public int Size {get;set;}
        [Required]
        public string SizeUnit {get;set;}
        public string Kind {get;set;}
        [Required]
        public string Action {get;set;}
        public int UserId {get;set;}
        public User Owner {get;set;}
        [Required]
        public string Description {get;set;}
        public List<Comment> Comments { get; set;}
        public List<Like> Likes {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}