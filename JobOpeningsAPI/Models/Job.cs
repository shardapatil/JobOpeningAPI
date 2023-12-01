using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobOpeningsAPI.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string? JobCode { get; set; }
        public required string JobTitle { get; set; }
        public required string JobDescription { get; set; }
        public Locations? Location { get; set; }
        public Department? Department { get; set; }
        public DateTime JobPostedDate { get; set; }
        public DateTime JobClosingDate { get; set; }
    }
}
