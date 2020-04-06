using EmployeeRepository;
using EmployeeRepository.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeContext _context;
        private readonly ILogger<EmployeesController> _logger;
        public EmployeesController(EmployeeContext context, ILogger<EmployeesController> logger)
        {
            _context = context;
            _logger = logger;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                _logger.LogInformation($"Employee id {id} not found.");
                return NotFound();
            }

            return employee;
        }

        // GET: api/Employees/karen
        [HttpGet("search/{name}/{startYear}/{endYear}/")]
        public async Task<ActionResult<List<Employee>>> GetEmployees(string name, int startYear, int endYear)
        {
            name = name.ToUpperInvariant();
            DateTime start;
            DateTime end;
            if (startYear > 0)
                start = new DateTime(startYear, 1, 1);
            else
                start = DateTime.MinValue;

            if (endYear > 0)
                end = new DateTime(endYear, 1, 1);
            else
                end = DateTime.MaxValue;

            var employees = await _context.Employees
                 .Where(e => e.HireDate >= start 
                            && Convert.ToDateTime(e.TerminationDate) <= end
                            && (name == "_" || e.FirstName.ToUpperInvariant().Contains(name) || e.LastName.ToUpperInvariant().Contains(name)))
                 .ToListAsync();

            if (employees == null)
            {
                _logger.LogInformation($"Employee name containing {name} not found.");
                return NotFound();
            }

            return employees;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(long id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        private bool EmployeeExists(long id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
