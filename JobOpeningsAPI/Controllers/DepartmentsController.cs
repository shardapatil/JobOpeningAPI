using JobOpeningsAPI.Data;
using JobOpeningsAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobOpeningsAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly JobContext _context;

        public DepartmentsController(JobContext context)
        {
            _context = context;
        }

        /// <summary>
        /// this api returns all the departments
        /// </summary>
        /// <returns></returns>
        // GET: api/v1/Departments
       // [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
        {
            return await _context.Department.ToListAsync();
        }

        /// <summary>
        /// this api will create a department 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        // POST : api/v1/Departments
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartments(Department req)
        {
            _context.Department.Add(req);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DepartmentExists(req.DeptId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        /// <summary>
        /// this api modifies department based on department id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        //PUT: api/v1/Departments/5
       // [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartments(int id, Department req)
        {
            if (id != req.DeptId)
            {
                return BadRequest();
            }

            _context.Entry(req).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.DeptId == id);
        }

    }
}
