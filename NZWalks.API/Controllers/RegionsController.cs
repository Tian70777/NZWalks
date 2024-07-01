using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using static System.Net.WebRequestMethods;

namespace NZWalks.API.Controllers
{
    // use DbContext to talk to the dband get the result back
    // have inject dbcontext in program.cs
    // now can use a constructor to inject the dbcontext
    
    // https://localhost:1234/api/regions
    // pointing to the regions controller
    [Route("api/[controller]")]
    [ApiController]

    // CRUD operations on this region's resource
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        // use DbContext to talk to the dband get the result back
        // have inject dbcontext in program.cs
        // now can use a constructor to inject the dbcontext
        public RegionsController(NZWalksDbContext dbContext)
        {
            // assign the provate foeld here, so can use dbcontext in the methods
            this.dbContext = dbContext;
        }

        // GET ALL REGIONS
        // GET: https//localhost:1234/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();

            return Ok(regions);
        }

        // GET region by Id
        // GET: https//localhost:1234/api/regions/{id}
        [HttpGet]
        //add a route attribute 
        [Route("{id:Guid}")]
        public  IActionResult GetById([FromRoute] Guid id)
        {
            // Find method only takes the Primary Key
            // var region = dbContext.Regions.Find(id);

            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if( region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }

    }
}
