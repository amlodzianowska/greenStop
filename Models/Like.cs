using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace greenStop.Models
{ 

    public class Like 
    {
        [Key]
        public int LikeId {get;set;}
        public int UserId {get;set;}
        public int PlantId {get;set;}
        public User User {get;set;}
        public Plant Plant {get;set;}
    }
}