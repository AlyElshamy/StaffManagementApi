using StaffApi.Entities;
using StaffApi.ViewModels;
using AutoMapper;
using AdminStaff.ViewModels;
using StaffApi.DTOs;

namespace StaffApi.AutoMapping
{
    public class Mapping : IMapping
    {
        private readonly IMapper mapper;

        public Mapping(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public Student MappingStudent(StudentDTO student)
        {
            return new Student
            {
                firstName = student.firstName,
                lasttName = student.lasttName,
                dateOfBirth = student.dateOfBirth,
                nationality = (NationalityEnum)Enum.Parse(typeof(NationalityEnum), student.nationality)
            };
           
        }
        public Student MappingStudent(EditStudentVM student)
        {
            
            return new Student
            {
                ID = student.ID,
                firstName = student.firstName,
                lasttName = student.lasttName,
                dateOfBirth = student.dateOfBirth,
                nationality = (NationalityEnum)Enum.Parse(typeof(NationalityEnum), student.nationality)
            };
        }
        
    }
}
