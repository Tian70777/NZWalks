using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using static System.Net.WebRequestMethods;

namespace NZWalks.API.Controllers
{
    // https://localhost:1234/api/regions
    // poing to the regions controller
    [Route("api/[controller]")]
    [ApiController]

    // CRUD operations on this region's resource
    public class RegionsController : ControllerBase
    {
        // action method, [attribute]
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region>
            {
                
                
                
                
                new Region{
                    Id = Guid.NewGuid(),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://i0.wp.com/www.yolosolotravel.com/wp-content/uploads/2021/11/auckland-map.jpg?resize=800%2C774&ssl=1"
                },
                new Region{
                    Id = Guid.NewGuid(),
                    Name = "Wellington Region",
                    Code = "WLG",
                    RegionImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT2fy0QYm9K1G__E_NtqaQcUc65Mma9iBZ5aQ&s"
                }
            };
            return Ok(regions);
        }
    }
}
