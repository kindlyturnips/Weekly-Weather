using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weekly_Weather.Models
{
    public class Location
    {
        //Cascade Delete Edit
        [Key]
        //[ForeignKey("Location")]
        public int? LocationId { get; set; }

        //Microsoft Identity
        [ForeignKey("ApplicationUser")]
        public string? UserId { get; set; }


        //Parent Child Relationship
        //Cascade Delete Edit
        [NotMapped]
        public virtual Forecast? virtual_forecast { get; set; }


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
