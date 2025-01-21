namespace Sprout.Web.Data.Entities.Srs
{
    public interface ISrsDataRepository
    {
        Task UpdateSrsDataAsync(SrsData srsData);
        Task<SrsData> GetSrsDatabyIdAsync(int id);
        Task<bool> SaveAllAsync();
    }
}
