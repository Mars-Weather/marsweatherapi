namespace MarsWeatherApi.Models
{
    public class Sol
    {
        public int Id { get; set; }
        public Wind Wind { get; set; }
        public Temperature Temperature { get; set; }
        public Pressure Pressure { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Season { get; set; }
        public int SolNumber { get; set; }

    }
}