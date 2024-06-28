using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOpitons) : base(dbContextOpitons)
        {
            
        }

        /* DBSet for each of the domain models, which will be used to create the tables in the database
        * type of DbSet is the domain model class, and the name of the DbSet is the name of the table in the database
        * DbSet is a collection of entities that can be queried, added, updated, and removed from the database, 
        * and it is a representation of a table in the database
        */
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

    }
}
