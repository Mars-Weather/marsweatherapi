using Newtonsoft.Json;

namespace MarsWeatherApi.Models
{
    public class Temperature
    {
        public int Id { get; set; }
        public float Average { get; set; }
        public float Minimum { get; set; }
        public float Maximum { get; set; }
        public Sol? Sol { get; set; }
        public int SolId { get; set; }
    }
}