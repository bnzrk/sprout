using Sprout.Web.Data.Entities.Srs;

namespace Sprout.Web.Services
{
    public class SrsService : ISrsService
    {
        private readonly ISrsDataRepository _repository;
        public const int Bins = 12; // NOTE: Temporary value for calculating repitition spacing
        public const int IncrementBase = 2;

        public SrsService(ISrsDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<SrsData> GetSrsDataByIdAsync(int id)
        {
            return await _repository.GetSrsDatabyIdAsync(id);
        }

        public async Task UpdateSrsProgressAsync(int srsId, bool isCorrect)
        {
            var srsData = await _repository.GetSrsDatabyIdAsync(srsId);
            if (srsData == null)
            {
                throw new Exception("Srs data not found.");
            }

            // Update progress level
            if (isCorrect)
            {
                srsData.ProgressLevel++;
            }
            else
            {
                srsData.ProgressLevel = 1;
            }

            // Update review times
            if (srsData.FirstReview == null)
            {
                srsData.FirstReview = DateTime.Now;
            }
            srsData.LastReviewed = DateTime.Now;
            srsData.NextReview = GetNextReviewDate(srsData);

            await _repository.UpdateSrsDataAsync(srsData);
        }

        public DateTime GetNextReviewDate(SrsData srsData)
        {
            int maxBins = GetMaxBins();
            int bin = (srsData.ProgressLevel > maxBins) ? maxBins : srsData.ProgressLevel;
            int daysToAdd = (int)Math.Pow(GetIncrementBase(), bin - 1);
            return DateTime.Now.AddDays(daysToAdd);
        }

        private int GetMaxBins()
        {
            return Bins;
        }

        private int GetIncrementBase()
        {
            return IncrementBase;
        }
    }
}
