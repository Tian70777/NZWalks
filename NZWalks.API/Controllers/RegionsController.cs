using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
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
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,IMapper mapper)
        {
            // assign the private field here, so can use dbcontext in the methods
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET ALL REGIONS
        // GET: https//localhost:1234/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // data from database
            // warp result in async ta
            var regionsDomain = await regionRepository.GetAllAsync();

            /* // map Domain Model back to DTO
            //var regionDtoList = new List<RegionDto>();

            //foreach(var region in regionsDomainModel)
            //{
            //    // map Domain Model back to DTO
            //    regionDtoList.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}

            // use automapper to map the domain model to the dto
            // take list regionDmainModel as source, and convert it to type List<RegionDto>
            */
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            // return DTOs back to the client,never Domain Model
            // Decoupling the Domain Model from the view layer
            return Ok(regionsDto);
        }

        // GET region by Id
        // GET: https//localhost:1234/api/regions/{id}
        [HttpGet]
        // Add a route attribute 
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);

            if( region == null)
            {
                return NotFound();
            }
            /* map/convert region domain model to region Dto
            //var regionDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};

            // use automapper to map the domain model to the dto
            */
            var regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }

        [HttpPost]
        //check model state before running the method
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            /* create a dto with code,name and url, only the three properties
            * get info from the clientm and map it to the domain model
            * pass the addRegionRequestDto in as a parameter

            * 1. map or convert the dto to the Domain Model
            *var regionDomainModel = new Region
            *{
            *    Code = addRegionRequestDto.Code,
            *    Name = addRegionRequestDto.Name,
            *    RegionImageUrl = addRegionRequestDto.RegionImageUrl
            *};
            */
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            /* 2. Use Domain Model to create Region then DbContext to save it to the database
            //await dbContext.Regions.AddAsync(regionDomainModel);

            // 3. need to run, so region can created in db
            */await dbContext.SaveChangesAsync();

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            /* map Domain Model back to DTO
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            */
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            // only return the regionDto, not the domain model
            // dispose to the client
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //Update Region
        // PUT: https//localhost:1234/api/regions/{id}
        [HttpPut]
        // Add a route attribute 
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            /* map the dto to the domain model
            //var regionDomainModel = new Region
            //{

            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            //};
            */
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            /* //update the domain model with new values
            //regionDomainModel.Code = updateRegionRequestDto.Code;
            //regionDomainModel.Name = updateRegionRequestDto.Name;
            //regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            //await dbContext.SaveChangesAsync();

            // convert the domain model back to the dto

            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            */
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            /*var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            */
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                

            return Ok(regionDto);
        }
    }
}
