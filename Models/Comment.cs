using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace greenStop.Models
{
    public class Comment 
    {
        [Key]
        public int CommentId {get;set;}
        [Required]
        [MinLength(10)]
        public string Text {get;set;}
        public int UserId {get;set;}
        public int PlantId {get;set;}
        public User Commenter {get;set;}
        public Plant PlantCommented {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}