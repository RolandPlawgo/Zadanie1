using Microsoft.EntityFrameworkCore;
using Zadanie1_API.Models;

namespace Zadanie1_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
