namespace MarsWeatherApi.Models
{
    public class Pressure
    {
        public int Id { get; set; }
        public float Average { get; set; }
        public float Minimum { get; set; }
        public float Maximum { get; set; }        
        public int Sol_Id { get; set; }

    }
}