using AutoMapper;
using FridayAssignments.Models.DTOs.FridayAssignments.DTOs;

namespace FridayAssignments.Models.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<DepartmentCreateDTO, Department>();
        }
    }
}
