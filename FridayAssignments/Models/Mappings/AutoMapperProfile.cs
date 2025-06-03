using AutoMapper;
using FridayAssignments.Models.DTOs;
using FridayAssignments.Models.DTOs.FridayAssignments.DTOs;

namespace FridayAssignments.Models.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Department
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<DepartmentPostDto, Department>();
            CreateMap<DepartmentPutDto, Department>();

            // Employee
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(dest => dest.Dept_Id, opt => opt.MapFrom(src => src.Dept_Id));
            CreateMap<EmployeePostDto, Employee>();
            CreateMap<EmployeePutDto, Employee>();
        }
    }
}
