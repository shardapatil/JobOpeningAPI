using Microsoft.EntityFrameworkCore;

namespace JobOpeningsAPI.Models
{
    public class Locations
    {

        public int LocationId { get; set; }
        public string? LocationTitle { get; set; }
        public string? LocationCity { get; set; }
        public string? LocationState { get; set; }
        public string? LocationCountry { get; set; }
        public int LocationZipCode { get; set; }
    }
}
