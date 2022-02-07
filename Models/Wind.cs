namespace MarsWeatherApi.Models
{
    public class Wind
    {
        public int Id { get; set; }
        public float average { get; set; }
        public float minimum { get; set; }
        public float maximum { get; set; }
        public string? mostCommonDirection { get; set; }

        //int sol_id { get; set; }
    }
}