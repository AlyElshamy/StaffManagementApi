using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StaffApi.AutoMapping;
using StaffApi.Buisness;
using StaffApi.Buisness.UserCheck;
using StaffApi.Data.Interfaces;
using StaffApi.DTOs;
using StaffApi.ViewModels;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StaffApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapping mapper;
        private readonly IEnumsCheck enumsCheck;
        private readonly IUserRoleCheck userRoleCheck;

        public StudentController(IStudentRepository StudentRepository, IUserRoleCheck userRoleCheck, IMapping mapper,IEnumsCheck enumsCheck)
        {
            studentRepository = StudentRepository;
            this.mapper = mapper;
            this.enumsCheck = enumsCheck;
            this.userRoleCheck = userRoleCheck;
        }
        // GET: api/<StudentController>
        [HttpGet]
        public IEnumerable<object> Get()
        {
            var result = studentRepository.GetAll();
            return (result.Select(a => new { a.ID, a.firstName, a.lasttName, a.dateOfBirth,nationality=Enum.GetName(typeof(NationalityEnum), a.nationality) }));
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0) return BadRequest("Please Enter Valid Id");
            var student = await studentRepository.FindAsync(id);
            if (student == null) return BadRequest("No Student Find With this ID");
            else
                return Ok(new { student.ID, student.firstName, student.lasttName, student.dateOfBirth, nationality=Enum.GetName(typeof(NationalityEnum), student.nationality) });
        }

        // POST api/<StudentController>
        [HttpPost]
        public IActionResult Post(StudentDTO Student)
        {

            if (!enumsCheck.NationalitiesCheck(Student.nationality)) return BadRequest("Choose correct Nationality");
            var student = mapper.MappingStudent(Student);

            //var student = new Student
            //{
            //    firstName = Student.firstName,
            //    lasttName = Student.lasttName,
            //    dateOfBirth = Student.dateOfBirth,
            //    nationality = (NationalityEnum)Enum.Parse(typeof(NationalityEnum), Student.nationality)
            //};
            studentRepository.AddAsync(student);
            studentRepository.Complete();
            return Ok(new { student.ID, student.firstName, student.lasttName, student.dateOfBirth, nationality = Enum.GetName(typeof(NationalityEnum), student.nationality) });
        }

        // PUT api/<StudentController>/5
        [HttpPut]
        public async Task<object> Put([FromBody] EditStudentVM Student)
        {
            if (!enumsCheck.NationalitiesCheck(Student.nationality)) return BadRequest("Choose correct Nationality");
            if (!await userRoleCheck.checkUser(Student.userID)) return Unauthorized("user not Found");
            if (!await userRoleCheck.checkrole(Student.userID)) Forbid("You have no permissions to edit student information");
            int? id = Student.ID;
            var student = await studentRepository.FindAsync(id);
            if (student == null) return BadRequest("No Student Find With this ID");
            var Model = mapper.MappingStudent(Student);
            studentRepository.Update(Model);
            studentRepository.Complete();
            return Ok(Model);
        }

        [HttpPut]
        [Route("ApproveInfo")]
        public async Task<object> ApproveInfo([FromBody] DeleteStudentVM Student)
        {

            if (!await userRoleCheck.checkUser(Student.userId)) return Unauthorized("user not Found");
            if (!await userRoleCheck.checkrole(Student.userId)) Forbid("You have no permissions to edit student information");
            int? id = Student.id;
            var student = await studentRepository.FindAsync(id);
            if (student == null) return "No Student Find With this ID";
            studentRepository.SubmitStudent(student);
            studentRepository.Complete();
            return (student);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete]
        public async Task<object> Delete( DeleteStudentVM student)
        {
            if (!await userRoleCheck.checkUser(student.userId)) return Unauthorized("user not Found");
            if (!await userRoleCheck.checkrole(student.userId)) Forbid("You have no permissions to edit student information");
            int? id = student.id;
            var FM = await studentRepository.FindAsync(id);
            if (FM == null) return BadRequest("No Family Member Find With this ID");
            studentRepository.Delete(FM);
            studentRepository.Complete();
            return Ok("Deleted Successfully");
        }

    }
    
}
