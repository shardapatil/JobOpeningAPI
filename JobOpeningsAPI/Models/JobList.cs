namespace JobOpeningsAPI.Models
{
    public class JobList
    {
        /// <summary>
        /// Search string
        /// </summary>
        public string Q { get; set; }

        /// <summary>
        /// Page number
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Optional location id
        /// </summary>
        public int LocationId { get; set; } = 0;

        /// <summary>
        /// Optional department id
        /// </summary>
        public int DepartmentId { get; set; } = 0;
    }
}
