namespace NZWalks.API.Model
{
    public class Station
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Created { get; set; }
        public string OperationFrom { get; set; }
        public string OperationTo { get; set; }
        public string Owner { get; set; }
        public List<string> ParameterId { get; set; }
        public string RegionId { get; set; }
        public string StationHeight { get; set; }
        public string StationId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Updated { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string WmoCountryCode { get; set; }
        public string WmoStationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

