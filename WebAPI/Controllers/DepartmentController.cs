using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DataContext _dbContext;
        public DepartmentController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<IEnumerable<Department>> Get()
        {
            return await _dbContext.Departments.ToListAsync();
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if (department == null)
            {
                NotFound();
            }
            return Ok(department);
        }

        // POST api/<DepartmentController>
        [HttpPost]
        public async Task<ActionResult> Post(DepartmentRequest departmentRequest)
        {
            var department = new Department()
            {
                Name = departmentRequest.Name
            };
            await _dbContext.Departments.AddAsync(department);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<DepartmentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,DepartmentRequest departmentRequest)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if(department == null)
            {
                 return NotFound();
            }
            else
            {
                department.Name = departmentRequest.Name;
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Departments.Remove(department);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
