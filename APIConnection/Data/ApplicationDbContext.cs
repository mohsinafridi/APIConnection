using APIConnection.Model;
using Microsoft.EntityFrameworkCore;

namespace APIConnection.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Department> Departments { get; set; }
    }
}
