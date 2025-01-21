using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;

namespace Sprout.Web.Data.Entities.Srs
{
    public class SrsDataRepository : ISrsDataRepository
    {
        private readonly ApplicationContext _context;

        public SrsDataRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<SrsData> GetSrsDatabyIdAsync(int id)
        {
            var srsData = await (from s in _context.SrsData where s.Id == id select s).FirstOrDefaultAsync();
            return srsData;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateSrsDataAsync(SrsData srsData)
        {
            _context.Update(srsData);
            await _context.SaveChangesAsync();
        }
    }
}
