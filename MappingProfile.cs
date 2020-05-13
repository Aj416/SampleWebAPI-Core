using AutoMapper;
using EmployeeAPI.Data;
using EmployeeAPI.Model;

namespace EmployeeAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmpId))
            .ReverseMap();
            CreateMap<Employee, CompleteEmployeeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmpId))
            .ReverseMap();

            CreateMap<EmployeeDetails, EmployeeDetailDto>().ReverseMap();

            CreateMap<Employee, EmployeeEditDto>().ReverseMap();
        }
    }
}