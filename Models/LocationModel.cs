using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weekly_Weather.Models
{
    public class Location
    {
        [Key]
        public int? LocationId { get; set; }

        //Microsoft Identity
        [ForeignKey("ApplicationUser")]
        public string? UserId { get; set; }
        //public User User { get; set; }


        //Location Parameters
        public string lat { get; set; }
        public string lon { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string state { get; set; }  
        public string country { get; set; }
        public string country_code { get; set; }

        //Forecast Parameters
        public IList<Forecast>? forecast { get; set; }
    }
    }
