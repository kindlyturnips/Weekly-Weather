using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Weekly_Weather.Models
{
    public class Forecast
    {
        [Key]
        public int? ForecastId { get; set; }

        [ForeignKey("Location")]
        public int? LocationId { get; set; }

        //Forecast Parameters
        //https://stackoverflow.com/questions/15220921/how-to-store-double-array-to-database-with-entity-framework-code-first-approac
        [JsonIgnore]
        public string? date { get; set; }
            [NotMapped]
            public string[] date_array
            {
                get
                {
                    return date.Split(';');
                }
                set
                {
                    string[] _data = value;
                    date = String.Join(";", _data);
                }
            }
        [JsonIgnore]
        public string? temperature_2m_max { get; set; }
            [NotMapped]
            public float[] temperature_2m_max_array 
            {
                get
                {
                    return Array.ConvertAll(temperature_2m_max.Split(';'), float.Parse);
                }
                set
                {
                    float[] _data = value;
                    temperature_2m_max = String.Join(";", _data.Select(p => p.ToString()).ToArray());
                }
            }
        [JsonIgnore]
        public string? temperature_2m_min { get; set; }
            [NotMapped]
            public float[] temperature_2m_min_array
            {
                get
                {

                    return Array.ConvertAll(temperature_2m_min.Split(';'), float.Parse);
                }
                set
                {
                    float[] _data = value;
                    temperature_2m_min = String.Join(";", _data.Select(p => p.ToString()).ToArray());
                }
        }
        [JsonIgnore]
        public string? sunrise { get; set; }
            [NotMapped]
            public string[] sunrise_array
            {
                get
                {
                    return sunrise.Split(';');
                }
                set
                {
                    string[] _data = value;
                    sunrise = String.Join(";", _data);
                }
        }
        [JsonIgnore]
        public string? sunset { get; set; }
            [NotMapped]
            public string[] sunset_array
            {
                get
                {
                    return sunset.Split(';');
                }
                set
                {
                    string[] _data = value;
                    sunset = String.Join(";", _data);
                }
        }
        [JsonIgnore]
        public string? precipitation_sum { get; set; }
            [NotMapped]
            public float[] precipitation_sum_array
            {
                get
                {
                    return Array.ConvertAll(precipitation_sum.Split(';'), float.Parse);
                }
                set
                {
                    float[] _data = value;
                    precipitation_sum = String.Join(";", _data.Select(p => p.ToString()).ToArray());
                }
        }
        [JsonIgnore]
        public string? precipitation_probability_max { get; set;}
            [NotMapped]
            public float[] precipitation_probability_max_array
            {
                get
                {
                    return Array.ConvertAll(precipitation_probability_max.Split(';'), float.Parse);
                }
                set
                {
                    float[] _data = value;
                    precipitation_probability_max = String.Join(";", _data.Select(p => p.ToString()).ToArray());
                }
            }
        [JsonIgnore]
        public string? weather_code { get; set; }
            [NotMapped]
            public int[] weather_code_array
            {
                get
                {              
                return Array.ConvertAll(weather_code.Split(';'), int.Parse);
                }
                set
                {
                    int[] _data = value;
                    weather_code = String.Join(";", _data.Select(p => p.ToString()).ToArray());
                }
            }


        public string precipitation_sum_units { get; set; }
        public string temperature_2m_units { get; set; }
      
        public string creation_date { get; set; }

    }
}
