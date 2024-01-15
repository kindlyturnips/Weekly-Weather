using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weekly_Weather.Models
{
    public class Forecast
    {
        [Key]
        public int? ForecastId { get; set; }

        [ForeignKey("Location")]
        public int? LocationId { get; set; }

        //Location Parameters
        public DateTime date { get; set; }
        public string temperature_2m_max { get; set; }
        public string temperature_2m_min { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string precipitation_sum { get; set; }
        public string precipitation_probability_max { get; set;}
    }
}
