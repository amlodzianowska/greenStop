using Microsoft.EntityFrameworkCore;

namespace greenStop.Models
{ 
    // the MyContext class representing a session with our MySQL 
    // database allowing us to query for or save data
    public class MyContext : DbContext 
    { 
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}