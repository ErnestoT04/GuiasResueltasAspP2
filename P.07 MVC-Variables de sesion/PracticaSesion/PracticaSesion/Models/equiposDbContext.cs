
using Microsoft.EntityFrameworkCore;
namespace PracticaSesion.Models
{
    public class equiposDbContext : DbContext
    {
        public equiposDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<marcas> marcas { get; set; }
        public DbSet<usuarios> usuarios { get; set; }
    }
}
