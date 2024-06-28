using Newtonsoft.Json;

namespace NZWalks.API.Model
{
    public class StationData
    {
        //public string Type { get; set; }
        //public List<Feature> Features { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }

        public class Feature
        {
            [JsonProperty("geometry")]
            public Geometry Geometry { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("properties")]
            public Properties Properties { get; set; }
        }

        public class Geometry
        {
            [JsonProperty("coordinates")]
            public List<double> Coordinates { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public class Properties
        {
            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("owner")]
            public string Owner { get; set; }

            [JsonProperty("parameterId")]
            public List<string> ParameterId { get; set; }

            [JsonProperty("stationId")]
            public string StationId { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("timeCreated")]
            public string TimeCreated { get; set; }

            [JsonProperty("timeOperationFrom")]
            public string TimeOperationFrom { get; set; }

            [JsonProperty("timeOperationTo")]
            public string TimeOperationTo { get; set; }

            [JsonProperty("timeUpdated")]
            public string TimeUpdated { get; set; }

            [JsonProperty("timeValidFrom")]
            public string TimeValidFrom { get; set; }

            [JsonProperty("timeValidTo")]
            public string TimeValidTo { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }
    }
}
