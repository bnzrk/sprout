namespace Sprout.Web.Data.Entities.Kanji
{
    public interface IKanjiRepository
    {
        Task SaveKanjiAsync(Kanji kanji);
        Task SaveKanjiListAsync(List<Kanji> kanji);
        Task UpdateKanjiAsync(Kanji kanji);
        Task DeleteKanjiAsync(Kanji kanji);
        Task<IEnumerable<Kanji>> GetAllKanjiAsync();
        Task<Kanji> GetKanjiByIDAsync(int id);
        Task<Kanji> GetKanjiByLiteralAsync(string literal);
        Task<bool> SaveAllAsync();
    }
}
