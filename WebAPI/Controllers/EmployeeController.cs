using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return new JsonResult( await _dataContext.Employees.ToListAsync());
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var employee = await _dataContext.Employees.SingleOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<ActionResult> Post(EmployeeRequest employeeRequest)
        {
            var employee = new Employee()
            {
                FirstName = employeeRequest.FirstName,
                LastName = employeeRequest.LastName,
                DepartmentName = employeeRequest.DepartmentName,
                DateOfJoining = employeeRequest.DateOfJoining,
                PhotoFileName = employeeRequest.PhotoFileName
            };
            await _dataContext.AddAsync(employee);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, EmployeeRequest employeeRequest)
        {
            var employee = await _dataContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                employee.FirstName = employeeRequest.FirstName;
                employee.LastName = employeeRequest.LastName;
                employee.DepartmentName = employeeRequest.DepartmentName;
                employee.DateOfJoining = employeeRequest.DateOfJoining;
                employee.PhotoFileName = employeeRequest.PhotoFileName;

                await _dataContext.SaveChangesAsync();
                return Ok();
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var employee = await _dataContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                _dataContext.Employees.Remove(employee);
                await _dataContext.SaveChangesAsync();
                return Ok();
            }
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(fileName);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }
    }
}
