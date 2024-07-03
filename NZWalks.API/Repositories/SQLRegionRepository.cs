using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        // use DbContext to talk to the dband get the result back
        // have inject dbcontext in program.cs
        // now can use a constructor to inject the dbcontext
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetByIdAsync(Guid id)
        {
            // Find method only takes the Primary Key
            // var region = dbContext.Regions.Find(id);
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
