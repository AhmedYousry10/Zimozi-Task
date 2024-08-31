using AutoMapper;
using EmployeeManagementAPI.DTO;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
        }
    }
}
