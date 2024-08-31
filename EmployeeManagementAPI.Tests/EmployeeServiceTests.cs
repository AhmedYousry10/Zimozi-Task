using AutoMapper;
using EmployeeManagementAPI.DTO;
using EmployeeManagementAPI.IServices;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Tests
{
    public class EmployeeServiceTests
    {
        private readonly EmployeeService _service;
        private readonly Mock<DbSet<Employee>> _mockSet;
        private readonly Mock<appContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<EmployeeService>> _mockLogger;

        public EmployeeServiceTests()
        {
            _mockSet = new Mock<DbSet<Employee>>();
            _mockContext = new Mock<appContext>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<EmployeeService>>();

            _mockContext.Setup(m => m.Employees).Returns(_mockSet.Object);
            _service = new EmployeeService(_mockContext.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllEmployees_ReturnsListOfEmployeeDTO()
        {
            // Arrange
            var employees = new List<Employee>
    {
        new Employee { EmployeeID = 1, Name = "Ahmed Helal" }
    }.AsQueryable();

            _mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employees.Provider);
            _mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            _mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            _mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            var employeeDTOs = new List<EmployeeDTO>
    {
        new EmployeeDTO { EmployeeID = 1, Name = "Ahmed Helal" }
    };

            _mockMapper.Setup(m => m.Map<List<EmployeeDTO>>(It.IsAny<List<Employee>>()))
                .Returns(employeeDTOs);

            // Act
            var result = await _service.GetAllEmployees();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Ahmed Helal", result.First().Name);
        }





    }

}
