using JwtImplementation.Models.User;
using Microsoft.EntityFrameworkCore;

namespace JwtImplementation.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
