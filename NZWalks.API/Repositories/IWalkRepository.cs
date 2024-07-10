using NZWalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetWalksAsync();
        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk?> UpdateAsync(Guid id, Walk walk);

        Task<Walk?> DeleteAsync(Guid id);
    }
}
