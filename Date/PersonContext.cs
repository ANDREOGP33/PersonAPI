using Microsoft.EntityFrameworkCore;
using Person.Models;

namespace Person.Date {
    public class PersonContext : DbContext {
        public DbSet<Models.PersonModel> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=Person.sqlite");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
