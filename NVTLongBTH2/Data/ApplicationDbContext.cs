using NVTLongBTH2.Models;
using Microsoft.EntityFrameworkCore;
namespace NVTLongBTH2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students {get; set;}
        public DbSet<NVTLongBTH2.Models.Employee> Employee{get; set;}
    }
}