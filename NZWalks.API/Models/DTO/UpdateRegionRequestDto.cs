namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        public string Code { get; set; }
        public string Name { get; set; }

        // RegionImageUrl is nullable 
        public string? RegionImageUrl { get; set; }
    }
}
