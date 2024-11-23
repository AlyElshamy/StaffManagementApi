using StaffApi.Entities;
using StaffApi.ViewModels;
using AutoMapper;
using AdminStaff.ViewModels;
using StaffApi.DTOs;

namespace StaffApi.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<FamilyMemberVM, FamilyMember>();
            CreateMap<StudentDTO, Student>();
            CreateMap<EditStudentVM, Student>();
            CreateMap<EditFamilyMemberVM, FamilyMember>();
        }
    }
}
