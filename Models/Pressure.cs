using Newtonsoft.Json;

namespace MarsWeatherApi.Models
{
    public class Pressure
    {
        public int Id { get; set; }
        public float Average { get; set; }
        public float Minimum { get; set; }
        public float Maximum { get; set; }        
        [JsonIgnore]
        public Sol? Sol { get; set; }
        public int SolId { get; set; }

    }
}