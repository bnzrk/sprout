using Microsoft.EntityFrameworkCore;
using Sprout.Web.Data.Entities.Kanji;
using Sprout.Web.Data.Entities.Srs;
using Sprout.Web.Data.Entities.Review;

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
        public DbSet<Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:DefaultConnection"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Kanji>()
                .HasIndex(k => k.Literal)
                .IsUnique();

            modelBuilder.Entity<Card>()
                .HasMany(c => c.Decks)
                .WithMany(d => d.Cards)
                .UsingEntity(
                    "CardDeck",
                    l => l.HasOne(typeof(Deck)).WithMany().HasForeignKey("DecksId").HasPrincipalKey(nameof(Deck.Id)),
                    r => r.HasOne(typeof(Card)).WithMany().HasForeignKey("CardsId").HasPrincipalKey(nameof(Card.Id)),
                    j => j.HasKey("CardsId", "TagsId"))
                .HasIndex(c => c.Kanji)
                .IsUnique();
        }
    }
}
