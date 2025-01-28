using Microsoft.EntityFrameworkCore;

namespace Sprout.Web.Data.Entities.Review
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationContext _context;

        public CardRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task DeleteCardAsync(Card card)
        {
            _context.Remove(card);
            await _context.SaveChangesAsync();
        }

        public async Task<Card> GetCardByIdAsync(int id)
        {
            var card = await _context.Cards
                .Include(c => c.SrsData)
                .FirstOrDefaultAsync(c => c.Id == id);
            return card;
        }

        public async Task<Card> GetCardByKanjiAsync(string userId,string kanjiLiteral)
        {
            var card = await _context.Cards
                .Include(c => c.SrsData)
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync(c => c.Kanji == kanjiLiteral);
            return card;
        }

        public async Task<IEnumerable<Card>> GetCardsDueOnAsync(string userId, DateTime dueDateTime)
        {
            var cards = await _context.Cards
            .Include(c => c.SrsData)
            .Where(c => c.SrsData.NextReview <= dueDateTime && c.UserId == userId)
            .ToListAsync();

            return cards;
        }

        public async Task<IEnumerable<Card>> GetCardsWithoutReviewAsync(string userId)
        {
            var card = await _context.Cards
                .Include(c => c.SrsData)
                .Where(c => c.SrsData.FirstReview == null && c.UserId == userId)
                .ToListAsync();
            return card;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task SaveCardAsync(Card card)
        {
            _context.Add(card);
            await _context.SaveChangesAsync();
        }

        public async Task SaveCardListAsync(List<Card> cardList)
        {
            await _context.BulkInsertAsync(cardList);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCardAsync(Card card)
        {
            _context.Update(card);
            await _context.SaveChangesAsync();
        }
    }
}
