using AutoMapper;
using Company.DAL.Models;
using Company.G01.PL.Dtos;

namespace Company.Mapping
{
    public class EmployeeProfile : Profile
    {
        // CLR
        public EmployeeProfile() 
        {
            CreateMap<CreateEmployeeDto, Employee>();
                //.ForMember(d => d.Name , o => o.MapFrom(s => s.Name));//  We need this when the property names are different between the two classes in the mapping
            CreateMap<Employee, CreateEmployeeDto>();
                //.ForMember(d => d.DepartmentName , o => o.MapFrom(s => s.Department.Name));
        }
    }
}
