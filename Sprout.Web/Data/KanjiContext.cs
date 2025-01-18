using Microsoft.EntityFrameworkCore;
using Sprout.Web.Data.Entities.Kanji;

namespace Sprout.Web.Data
{
    public class KanjiContext : DbContext
    {
        public KanjiContext(IConfiguration config)
        {
            _config = config;
        }

        private readonly IConfiguration _config;

        public DbSet<Kanji> Kanji { get; set; }

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
