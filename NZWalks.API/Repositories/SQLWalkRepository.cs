using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetWalksAsync()
        {
            /* cau use a string in Include to include the region
            // to make it type-safe, can use a lambda expression (x => x.Difficulty)
            // but he uese string here, cuz will use generic repository pattern later
            // which means the repository will be generic and can't use lambda expression
            // and by using generic repository pattern, can use the same repository for all the entities
            */
            return await dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .ToListAsync();
        }
        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if(existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.LengthInKm = walk.LengthInKm;

            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walkToDelete = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walkToDelete == null)
            {
                return null;
            }

            dbContext.Walks.Remove(walkToDelete);
            await dbContext.SaveChangesAsync();

            return walkToDelete;
        }
    }
}
