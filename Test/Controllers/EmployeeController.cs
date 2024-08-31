using AutoMapper;
using EmployeeManagementAPI.DTO;
using EmployeeManagementAPI.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IMapper mapper)
        {
            _employeeService = employeeService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all employees", Description = "Get all Employees.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(List<EmployeeDTO>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<ActionResult<List<EmployeeDTO>>> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all employees.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get employee by ID")]
        [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(EmployeeDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Employee not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound($"Employee with ID {id} not found.");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching employee with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add a new employee")]
        [SwaggerResponse(StatusCodes.Status201Created, "Employee created", typeof(EmployeeDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<ActionResult<EmployeeDTO>> AddEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdEmployee = await _employeeService.AddEmployee(employeeDTO);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.EmployeeID }, createdEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new employee.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing employee")]
        [SwaggerResponse(StatusCodes.Status200OK, "Employee updated", typeof(EmployeeDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "ID mismatch or invalid input")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Employee not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployee(int id, [FromBody] EmployeeDTO employeeDTO)
        {
            if (id != employeeDTO.EmployeeID)
            {
                _logger.LogWarning($"ID mismatch: URL ID {id} does not match body ID {employeeDTO.EmployeeID}.");
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedEmployee = await _employeeService.UpdateEmployee(id, employeeDTO);
                if (updatedEmployee == null)
                {
                    _logger.LogWarning($"Employee with ID {id} not found.");
                    return NotFound($"Employee with ID {id} not found.");
                }
                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating employee with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an employee")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Employee deleted")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Employee not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                var success = await _employeeService.DeleteEmployee(id);
                if (!success)
                {
                    _logger.LogWarning($"Employee with ID {id} not found.");
                    return NotFound($"Employee with ID {id} not found.");
                }
                return Ok($"Employee with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting employee with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
