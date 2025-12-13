using AutoMapper;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Configuration
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            //CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>().ReverseMap();
            //CreateMap<Employee, EmployeeDTO>().ReverseMap();
        }
    }
}
