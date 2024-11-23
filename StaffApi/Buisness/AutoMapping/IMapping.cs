using StaffApi.DTOs;
using StaffApi.Entities;
using StaffApi.ViewModels;

namespace StaffApi.AutoMapping
{
    public interface IMapping
    {
        Student MappingStudent(EditStudentVM student);
        Student MappingStudent(StudentDTO student);
    }
}