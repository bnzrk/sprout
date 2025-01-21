using Microsoft.EntityFrameworkCore;
using Sprout.Web.Data.Entities.Kanji;
using Sprout.Web.Data.Entities.Srs;

namespace Sprout.Web.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(IConfiguration config)
        {
            _config = config;
        }

        private readonly IConfiguration _config;

        public DbSet<Kanji> Kanji { get; set; }
        public DbSet<SrsData> SrsData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:DefaultConnection"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
