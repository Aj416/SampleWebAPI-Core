using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Data;
using EmployeeAPI.GenericRepository;
using EmployeeAPI.Logging;
using EmployeeAPI.Repository;
using EmployeeAPI.UnitOfWorks;
using AutoMapper;
using EmployeeAPI.Model;

namespace EmployeeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private IUnitOfWork unitOfWork;
        private IMapper _mapper;

        private readonly ILoggerManager _logger;

        public EmployeeController(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._logger = logger;
            this._mapper = mapper;
        }

        // GET: api/Employee
        [HttpGet]
        // [Route("[action]")]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                // var employeeList = await repository.GetAll();
                var employeeList = await unitOfWork.EmployeeRepository.GetAll();
                var dto = _mapper.Map<IEnumerable<EmployeeDto>>(employeeList);
                _logger.LogInfo($"Returned all employees from database.");
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Employee
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployeeWithDetails()
        {
            try
            {
                // var employeeList = await repository.GetAll();
                var employeeList = await unitOfWork.EmployeeRepository.GetAllWithDetails();
                var dto = _mapper.Map<IEnumerable<CompleteEmployeeDto>>(employeeList);
                _logger.LogInfo($"Returned all employees with details from database.");
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployeeWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: Employee/5
        [HttpGet("{id}", Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                var employee = await unitOfWork.EmployeeRepository.GetById(id);
                var dto = _mapper.Map<EmployeeDto>(employee);
                if (employee == null)
                {
                    _logger.LogError($"Employee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned Employee with id: {id}");
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetEmployeeWithDetails(int id)
        {
            try
            {
                var employee = await unitOfWork.EmployeeRepository.GetByIdWithDetails(id);
                var dto = _mapper.Map<CompleteEmployeeDto>(employee);
                if (employee == null)
                {
                    _logger.LogError($"Employee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _logger.LogInfo($"Returned Employee details with id: {id}");
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetEmployeeWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeEditDto employee)
        {
            try
            {
                if (employee == null)
                {
                    _logger.LogError("Employee object sent from client is null.");
                    return BadRequest("Employee object is null");
                }

                var entity = await unitOfWork.EmployeeRepository.GetById(id);
                if (entity == null)
                {
                    _logger.LogError($"Employee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(employee, entity);
                unitOfWork.EmployeeRepository.Update(entity);
                await unitOfWork.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PutEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }


        }

        // POST: api/Employee
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeEditDto dto)
        {
            try
            {
                if (dto == null)
                {
                    _logger.LogError("Employee object sent from client is null.");
                    return BadRequest("Employee object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Employee object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var entity = _mapper.Map<Employee>(dto);

                unitOfWork.EmployeeRepository.Insert(entity);
                await unitOfWork.SaveAsync();

                var createdEmployee = _mapper.Map<EmployeeDto>(entity);

                return CreatedAtRoute("EmployeeById", new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside PostEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var entity = await unitOfWork.EmployeeRepository.GetById(id);
                if (entity == null)
                {
                    _logger.LogError($"Employee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                // if (_repository.Account.AccountsByOwner(id).Any()) 
                // {
                //     _logger.LogError($"Cannot delete employee with id: {id}. It has related details. Delete those details first"); 
                //     return BadRequest("Cannot delete employee. It has related details. Delete those details first"); 
                // }

                unitOfWork.EmployeeRepository.Delete(id);
                await unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteEmployee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
