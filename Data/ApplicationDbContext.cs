using NguyenVietPhuongBTH2.Models;
using Microsoft.EntityFrameworkCore;

namespace NguyenVietPhuongBTH2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employee { get; set; }
      
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Person> Person { get; set; }
    }   
}
