using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Data;

namespace EmployeeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeDetailController : ControllerBase
    {
        private readonly ProductsContext _context;

        public EmployeeDetailController(ProductsContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDetails>>> GetEmployeeDetails()
        {
            return await _context.EmployeeDetails.ToListAsync();
        }

        // GET: api/EmployeeDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDetails>> GetEmployeeDetails(int id)
        {
            var employeeDetails = await _context.EmployeeDetails.FindAsync(id);

            if (employeeDetails == null)
            {
                return NotFound();
            }

            return employeeDetails;
        }

        // PUT: api/EmployeeDetail/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeDetails(int id, EmployeeDetails employeeDetails)
        {
            if (id != employeeDetails.DetailId)
            {
                return BadRequest();
            }

            _context.Entry(employeeDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeDetailsExists(id))
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

        // POST: api/EmployeeDetail
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EmployeeDetails>> PostEmployeeDetails(EmployeeDetails employeeDetails)
        {
            _context.EmployeeDetails.Add(employeeDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeDetails", new { id = employeeDetails.DetailId }, employeeDetails);
        }

        // DELETE: api/EmployeeDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeDetails>> DeleteEmployeeDetails(int id)
        {
            var employeeDetails = await _context.EmployeeDetails.FindAsync(id);
            if (employeeDetails == null)
            {
                return NotFound();
            }

            _context.EmployeeDetails.Remove(employeeDetails);
            await _context.SaveChangesAsync();

            return employeeDetails;
        }

        private bool EmployeeDetailsExists(int id)
        {
            return _context.EmployeeDetails.Any(e => e.DetailId == id);
        }
    }
}
