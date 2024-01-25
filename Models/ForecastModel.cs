using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Weekly_Weather.Models
{
    public class Forecast
    {
        [Key]
        [ForeignKey("Location")]
        public int? ForecastId { get; set; }

        [NotMapped]
        public virtual Location? virtual_location { get; set; }

        //Forecast Parameters
        [JsonIgnore]
        public string? date { get; set; }  //DB data
        [NotMapped]
        public string[] date_array     //Json data
        {
            get
            {
                return date.Split(';');  //Returns DB Data
            }
            set
            {
                string[] _data = value;  //Recieves Array from JSON
                date = String.Join(";", _data);  //Joins into string for DB
            }
        }
        [JsonIgnore]
        public string? temperature_2m_max { get; set; }  //DB data
        [NotMapped]
        public float[] temperature_2m_max_array      //Json data
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
        public string? temperature_2m_min { get; set; }  //DB data
        [NotMapped]
        public float[] temperature_2m_min_array     //Json data
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
        public string? sunrise { get; set; }  //DB data
        [NotMapped]
        public string[] sunrise_array     //Json data
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
        public string? sunset { get; set; }  //DB data
        [NotMapped]
        public string[] sunset_array     //Json data
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
        public string? precipitation_sum { get; set; }  //DB data
        [NotMapped]
        public float[] precipitation_sum_array     //Json data
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
        public string? precipitation_probability_max { get; set; }  //DB data
        [NotMapped]
        public float[] precipitation_probability_max_array     //Json data
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
        public string? weather_code { get; set; }  //DB data
        [NotMapped]
        public int[] weather_code_array     //Json data
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

        public string precipitation_sum_units { get; set; }  //DB data
        public string temperature_2m_units { get; set; }  //DB data
        public string creation_date { get; set; }  //DB data


    }
}
