using JobOpeningsAPI.Data;
using JobOpeningsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace JobOpeningsAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly JobContext _context;

        public JobsController(JobContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get All Jobs from database
        /// </summary>
        // GET: api/v1/Jobs
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            //return await _context.Job.ToListAsync();

            var jobs = await _context.Job
                .Include(j => j.Location)
                .Include(j => j.Department)
                .ToListAsync();

            var response = jobs.Select(j => new
            {
                id = j.JobId,
                code = j.JobCode,
                title = j.JobTitle,
                description = j.JobDescription,

                Location = new
                {
                    id = j.Location?.LocationId ?? 0,
                    title = j.Location?.LocationTitle,
                    city = j.Location?.LocationCity,
                    state = j.Location?.LocationState,
                    country = j.Location?.LocationCountry,
                    ZipCode = j.Location?.LocationZipCode ?? 0
                },
                Department = new
                {
                    id = j.Department?.DeptId ?? 0,
                    title = j.Department?.DeptTitle
                },
                postedDate = j.JobPostedDate,
                ClosingDate = j.JobClosingDate
            });

            return Ok(response);
        }


        /// <summary>
        /// search for job based on job id passed
        /// </summary>
        /// <param name="id">Job id to be searched</param>
        /// <returns></returns>
        // GET: api/v1/Jobs/5
       // [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJobOpening(int id)
        {
            var jobs = await _context.Job
                  .Include(j => j.Location)
                  .Include(j => j.Department)
                  .FirstOrDefaultAsync(j => j.JobId == id);

            if (jobs == null)
            {
                return NotFound();
            }

            var response = new 
            {
                id = jobs.JobId,
                code = jobs.JobCode,
                title = jobs.JobTitle,
                description = jobs.JobDescription,
                location = new 
                {
                    id = jobs.Location?.LocationId ?? 0,
                    title = jobs.Location?.LocationTitle,
                    city = jobs.Location?.LocationCity,
                    state = jobs.Location?.LocationState,
                    country = jobs.Location?.LocationCountry,
                    zip = jobs.Location?.LocationZipCode ?? 0
                },
                department = new 
                {
                    id = jobs.Department?.DeptId ?? 0,
                    title = jobs.Department?.DeptTitle
                },
                postedDate = jobs.JobPostedDate,
                closingDate = jobs.JobClosingDate
            };

            return Ok(response);
        }

        /// <summary>
        /// this will create a job opening and returns the job id
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        // POST : api/v1/jobs
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<Job>> PostJobs(Job req)
        {
            req.Department = await _context.Department.FindAsync(req.Department.DeptId);
            req.Location = await _context.Location.FindAsync(req.Location.LocationId);

            _context.Job.Add(req);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobExists(req.JobId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJobOpening", new { id = req.JobId });
        }

        /// <summary>
        /// this will edit a job opening based on job id 
        /// </summary>
        /// <param name="id"> job id</param>
        /// <param name="job"></param>
        /// <returns></returns>
        //PUT: api/v1/jobs/5
        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobOpenings(int id, Job job)
        {
            if (id != job.JobId)
            {
                return BadRequest();
            }

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }


        /// <summary>
        /// this api will searh based on query string, department id, location id
        /// </summary>
        /// <param name="request">
        /// Q: Search String
        /// Locationid: put location id to be searched
        /// DepartmentId: put Department id to be searched</param>
        /// <returns></returns>
        //[Authorize]
        [HttpPost("list")]
        public IActionResult ListJobs([FromBody] JobList request)
        {
            var query = _context.Job
                .Include(j => j.Location)
                  .Include(j => j.Department).AsQueryable();

            if (request.LocationId > 0)
            {
                query = query.Where(j => j.Location.LocationId == request.LocationId);
            }

            if (request.DepartmentId > 0)
            {
                query = query.Where(j => j.Department.DeptId == request.DepartmentId);
            }

            if (!string.IsNullOrEmpty(request.Q))
            {
                query = query.Where(j => j.JobTitle.Contains(request.Q) || j.Location.LocationCity.Contains(request.Q) || j.Location.LocationState.Contains(request.Q)
                || j.JobDescription.Contains(request.Q) || j.Location.LocationCountry.Contains(request.Q));
            }

            var total = query.Count();

            var data = query.Select(j => new
            {
                id = j.JobId,
                code = j.JobCode,
                title = j.JobTitle,
                location = j.Location == null ? "" : j.Location.LocationTitle,
                department = j.Department == null ? "" : j.Department.DeptTitle,
                postedDate = j.JobPostedDate,
                ClosingDate = j.JobClosingDate
            });


            return Ok(new { Total = total, Data = data });
        }

        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.JobId == id);
        }

    }
}
