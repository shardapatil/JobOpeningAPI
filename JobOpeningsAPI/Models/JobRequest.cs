namespace JobOpeningsAPI.Models
{
    public class JobRequest
    {
        public required string JobTitle { get; set; }
        public required string JobDescription { get; set; }
        public int LocationId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime JobClosingDate { get; set; }
    }
}
