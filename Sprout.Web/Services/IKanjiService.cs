using Sprout.Web.Data.Entities.Kanji;

namespace Sprout.Web.Services
{
    public interface IKanjiService
    {
        Task<IEnumerable<Kanji>> GetAllKanjiAsync();
        Task<Kanji> GetKanjiByLiteralAsync(string literal);
        Task<Kanji> GetKanjiByIdAsync(int id);
        Task CreateKanjiAsync(Kanji kanji);
        Task UpdateKanjiAsync(Kanji kanji);
        Task DeleteKanjiAsync(Kanji kanji);
        Task DeleteKanjiByLiteralAsync(string literal);
        Task PopulateDbAsync();
    }
}
