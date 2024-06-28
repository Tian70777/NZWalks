using Newtonsoft.Json;
using NZWalks.API.Model;

namespace NZWalks.API.Services
{
    /// <summary>
    /// this class is responsible for getting weather data from the external API
    /// ses HttpClient to send a GET request to the weather API. 
    /// The X-Gravitee-Api-Key header is set to the provided API key.
    /// Header is required to access the weather data.
    /// </summary>
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetWeatherAsyn()
        {
            _httpClient.DefaultRequestHeaders.Add("X-Gravitee-Api-Key", "726f7f25-f557-40ab-89f0-447ae22c037d");
            var response = await _httpClient.GetAsync("https://dmigw.govcloud.dk/v1/forecastdata");

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
            {
                throw new Exception($"Failed to get weather data. Status code: {response.StatusCode}");
            }
        }

        public async Task<IEnumerable<Station>> GetStationAsync()
        {
            var response = await _httpClient.GetAsync("https://dmigw.govcloud.dk/v2/metObs/collections/station/items?api-key=7df6a393-bf6d-4995-8bae-0f8092a79c86");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Log the content for debugging
                // Console.WriteLine(content);


                var stationData = JsonConvert.DeserializeObject<StationData>(content);
                var stations = stationData.Features.Select(f => new Station
                {
                    Name = f.Properties.Name,
                    Country = f.Properties.Country,
                    Created = f.Properties.TimeCreated,
                    OperationFrom = f.Properties.TimeOperationFrom,
                    OperationTo = f.Properties.TimeOperationTo,
                    Owner = f.Properties.Owner,
                    ParameterId = f.Properties.ParameterId,
                    // Assuming you adjust your Station class accordingly
                    // Remove or adjust properties that are not present in the adjusted model
                    StationId = f.Properties.StationId,
                    Status = f.Properties.Status,
                    Type = f.Properties.Type,
                    Updated = f.Properties.TimeUpdated,
                    ValidFrom = f.Properties.TimeValidFrom,
                    ValidTo = f.Properties.TimeValidTo,
                    // Assuming Latitude and Longitude are still relevant and your Station class has these properties
                    Latitude = f.Geometry.Coordinates[1],
                    Longitude = f.Geometry.Coordinates[0]
                }).ToList();
                Console.WriteLine(stations.Count);
                return stations;
            }
            else
            {
                throw new Exception($"Failed to get stations. Status code: {response.StatusCode}");
            }
        }

    }
}
