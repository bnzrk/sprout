using Sprout.Web.Contracts;

namespace Sprout.Web.Services
{
    public interface ISrsService
    {
        Task<SrsDataDto> GetSrsDataByIdAsync(int srsDataId);
        Task UpdateSrsProgressAsync(int srsId, bool isCorrect);
    }
}