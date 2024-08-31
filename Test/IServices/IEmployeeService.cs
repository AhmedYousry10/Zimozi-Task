using EmployeeManagementAPI.DTO;
using System;

namespace EmployeeManagementAPI.IServices
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllEmployees();
        Task<EmployeeDTO> GetEmployeeById(int id);  
        Task<EmployeeDTO> AddEmployee(EmployeeDTO employee);
        Task<EmployeeDTO> UpdateEmployee(int id, EmployeeDTO employee);
        Task<bool> DeleteEmployee(int id);


    }
}
