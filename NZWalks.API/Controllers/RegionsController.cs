using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
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
            // data from database
            var regionsDomainModel = dbContext.Regions.ToList();

            // map Domain Model back to DTO
            var regionDtoList = new List<RegionDto>();
            foreach(var region in regionsDomainModel)
            {
                // map Domain Model back to DTO
                regionDtoList.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            // return DTOs back to the client,never Domain Model
            // Decoupling the Domain Model from the view layer
            return Ok(regionDtoList);
        }

        // GET region by Id
        // GET: https//localhost:1234/api/regions/{id}
        [HttpGet]
        // Add a route attribute 
        [Route("{id:Guid}")]
        public  IActionResult GetById([FromRoute] Guid id)
        {
            // Find method only takes the Primary Key
            // var region = dbContext.Regions.Find(id);
            // 
            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if( region == null)
            {
                return NotFound();
            }
            // map/convert region domain model to region Dto
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };
            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // create a dto with code,name and url, only the three properties
            // get info from the clientm and map it to the domain model
            // pass the addRegionRequestDto in as a parameter

            // 1. map or convert the dto to the Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // 2. Use Domain Model to create Region then DbContext to save it to the database
            dbContext.Regions.Add(regionDomainModel);

            // 3. need to run, so region can created in db
            dbContext.SaveChanges();

            // map Domain Model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            // only return the regionDto, not the domain model
            // dispose to the client
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //Update Region
        // PUT: https//localhost:1234/api/regions/{id}
        [HttpPut]
        // Add a route attribute 
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if(regionDomainModel ==null)
            {
                return NotFound();
            }

            // update the domain model with new values
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

            // convert the domain model back to the dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
