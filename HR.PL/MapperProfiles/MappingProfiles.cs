using AutoMapper;
using HR.DAL.Models;
using HR.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HR.PL.MapperProfiles
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
			//Department
			CreateMap<DepartmentViewModel, Department>().ReverseMap();
			//Employee
			CreateMap<EmployeeViewModel, Employee>().ReverseMap();
			//Role
			CreateMap<RoleViewModel, IdentityRole>()
		   .ForMember(D => D.Name, o => o.MapFrom(s => s.RoleName)).ReverseMap();
			//User
			CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
			//Work_For
			CreateMap<WorksViewModel, Work_For>().ReverseMap();
		}

    }
}
