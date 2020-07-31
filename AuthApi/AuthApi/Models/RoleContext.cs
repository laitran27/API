using Microsoft.EntityFrameworkCore;

namespace AuthApi.Models
{
    public class RoleContext : DbContext
    {
        public RoleContext(DbContextOptions<RoleContext> options)
            : base(options)
        {
        }

        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Roles> Roles { get; set; }
    }
}