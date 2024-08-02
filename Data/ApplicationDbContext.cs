using Microsoft.EntityFrameworkCore;
using ToDO.Models;

namespace ToDO.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var Builder = new ConfigurationBuilder().
                AddJsonFile("appsettings.json", true, true).
                Build().
                GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(Builder);
        }
    }
}
