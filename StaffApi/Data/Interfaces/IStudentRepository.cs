using StaffApi.Entities;
using StaffApi.Interfaces;

namespace StaffApi.Data.Interfaces
{
    public interface IStudentRepository:IBaseRepository <Student>
    {
        public Student SubmitStudent(Student entity);

    }
}
