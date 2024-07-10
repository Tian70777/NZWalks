using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        /* use DbContext to talk to the dband get the result back
        // have inject dbcontext in program.cs
        // now can use a constructor to inject the dbcontext
        */
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
        public async Task<Region> CreateAsync(Region region)
        {
            
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var regionToDelete = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionToDelete == null)
            {
                return null;
            }

            dbContext.Regions.Remove(regionToDelete);
            await dbContext.SaveChangesAsync();
            return regionToDelete;
        }
    }
}
