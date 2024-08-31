using AutoMapper;
using EmployeeManagementAPI.DTO;
using EmployeeManagementAPI.IServices;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly appContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(appContext db, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        /* Get All Employees */
        public async Task<List<EmployeeDTO>> GetAllEmployees()
        {
            try
            {
                var employees = await _db.Employees.ToListAsync();
                return _mapper.Map<List<EmployeeDTO>>(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all employees.");
                throw;
            }
        }

        /* Get Employee By ID */
        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _db.Employees.FirstOrDefaultAsync(x => x.EmployeeID == id);
                if (employee == null)
                {
                    return null;
                }
                return _mapper.Map<EmployeeDTO>(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching employee with ID {id}.");
                throw;
            }
        }

        /* Add Employee */
        public async Task<EmployeeDTO> AddEmployee(EmployeeDTO employeeDTO)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employeeDTO);
                _db.Employees.Add(employee);
                await _db.SaveChangesAsync();

                return _mapper.Map<EmployeeDTO>(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new employee.");
                throw;
            }
        }

        /* Update Employee */
        public async Task<EmployeeDTO> UpdateEmployee(int id, EmployeeDTO employeeDTO)
        {
            try
            {
                var employee = await _db.Employees.FirstOrDefaultAsync(x => x.EmployeeID == id);
                if (employee == null)
                {
                    _logger.LogInformation($"Employee with ID {id} not found.");
                    return null;
                }

                _logger.LogInformation($"Updating employee with ID {id}.");
                _mapper.Map(employeeDTO, employee);
                await _db.SaveChangesAsync();

                return _mapper.Map<EmployeeDTO>(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating employee with ID {id}.");
                throw;
            }
        }


        /* Delete Employee */
        public async Task<bool> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _db.Employees.FirstOrDefaultAsync(x => x.EmployeeID == id);
                if (employee == null)
                {
                    return false;
                }

                _db.Employees.Remove(employee);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting employee with ID {id}.");
                throw;
            }
        }
    }
}
