using System.Text.Json.Serialization;

namespace MarsWeatherApi.Models
{
public class Pressure
    {
        public int Id { get; set; }
        public float? Average { get; set; } // Pascal (Pa)
        public float? Minimum { get; set; } // Pascal (Pa)
        public float? Maximum { get; set; } // Pascal (Pa)        
        [JsonIgnore]        
        public Sol? Sol { get; set; }
        public int SolId { get; set; }        
    }
}