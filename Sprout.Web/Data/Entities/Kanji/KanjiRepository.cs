using Microsoft.EntityFrameworkCore;

namespace Sprout.Web.Data.Entities.Kanji
{
    public class KanjiRepository : IKanjiRepository
    {
        public KanjiRepository(KanjiContext context) 
        {
            _context = context;
        }

        private readonly KanjiContext _context;

        public async Task DeleteKanjiAsync(Kanji kanji)
        {
            _context.Remove(kanji);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Kanji>> GetAllKanjiAsync()
        {
            var kanji = from k in _context.Kanji select k;
            return kanji.ToList();
        }

        public async Task<Kanji> GetKanjiByIDAsync(int id)
        {
            var kanji = await (from k in _context.Kanji where k.Id == id select k).FirstOrDefaultAsync();
            return kanji;
        }

        public async Task<Kanji> GetKanjiByLiteralAsync(string literal)
        {
            var kanji = await (from k in _context.Kanji where k.Literal == literal select k).FirstOrDefaultAsync();
            return kanji;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task SaveKanjiAsync(Kanji kanji)
        {
            _context.Add(kanji);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateKanjiAsync(Kanji kanji)
        {
            _context.Update(kanji);
            await _context.SaveChangesAsync();
        }

        public async Task SaveKanjiListAsync(List<Kanji> kanji)
        {
            await _context.BulkInsertAsync(kanji);
            await _context.SaveChangesAsync();
        }
    }
}
