using Microsoft.EntityFrameworkCore;
using simpleProject.Models.Entities;

namespace simpleProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) {
            
        }

        public DbSet<Student> Students { get; set; }    
            
    }
}
