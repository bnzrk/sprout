using Sprout.Web.Mappings;
using Sprout.Web.Data.Entities.Srs;
using Sprout.Web.Contracts;

namespace Sprout.Web.Services
{
    public class SrsService : ISrsService
    {
        private readonly ISrsDataRepository _repository;
        private readonly IMapper _mapper;

        public const int Bins = 12; // NOTE: Temporary value for calculating repitition spacing
        public const int IncrementBase = 2;

        public SrsService(ISrsDataRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SrsDataDto> GetSrsDataByIdAsync(int srsDataId)
        {
            var srsData = await _repository.GetSrsDatabyIdAsync(srsDataId);
            return _mapper.MapSrsDataToDto(srsData);
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
            srsData.LastReview = DateTime.Now;
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
