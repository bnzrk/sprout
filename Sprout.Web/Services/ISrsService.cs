using Sprout.Web.Data.Entities.Srs;

namespace Sprout.Web.Services
{
    public interface ISrsService
    {
        Task<SrsData> GetSrsDataByIdAsync(int id);
        Task UpdateSrsProgressAsync(int srsId, bool isCorrect);
    }
}
