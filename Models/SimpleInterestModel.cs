using System.ComponentModel.DataAnnotations;

namespace webapp.Models
{
    public class SimpleInterestModel
    {
        [Required(ErrorMessage = "Price is required")]
        public string Price {get; set; }
        [Required(ErrorMessage = "Time is required")]
        public string Time {get; set; }

        public string Interest {get; set; }
    }
    

}