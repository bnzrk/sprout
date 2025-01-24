
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Sprout.Web.Data.Entities.Review
{
    public class DeckRepository : IDeckRepository
    {
        private readonly ApplicationContext _context;

        public DeckRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task DeleteDeckAsync(Deck deck)
        {
            _context.Add(deck);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Card>> GetAllDeckCards(int deckId)
        {
            var deck = await _context.Decks
                .Include(d => d.Cards)
                .FirstOrDefaultAsync(d => d.Id == deckId);
            return deck.Cards;
        }

        public async Task<Deck> GetDeckByIdAsync(int deckId)
        {
            var deck = await _context.Decks
                .FirstOrDefaultAsync(d => d.Id == deckId);
            return deck;
        }

        public async Task<IEnumerable<Card>> GetDeckCardsDueOnAsync(int deckId, DateTime dueDateTime)
        {
            var cards = await _context.Cards
                .Where(c => c.Decks.Any(d => d.Id == deckId))
                .Include(c => c.SrsData)
                .Where(c => c.SrsData.NextReview <= dueDateTime)
                .ToListAsync();
            return cards;
        }

        public async Task<IEnumerable<Card>> GetDeckCardsWithoutReviewAsync(int deckId)
        {
            var cards = await _context.Cards
                .Where(c => c.Decks.Any(d => d.Id == deckId))
                .Include(c => c.SrsData)
                .Where(c => c.SrsData.FirstReview == null)
                .ToListAsync();
            return cards;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task SaveDeckAsync(Deck deck)
        {
            _context.Add(deck);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeckAsync(Deck deck)
        {
            _context.Update(deck);
            await _context.SaveChangesAsync();
        }
    }
}
