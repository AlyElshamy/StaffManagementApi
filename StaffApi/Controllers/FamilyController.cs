using AdminStaff.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffApi.Buisness;
using StaffApi.Buisness.UserCheck;
using StaffApi.Data.Interfaces;
using StaffApi.Entities;
using StaffApi.Interfaces;
using StaffApi.ViewModels;

namespace StaffApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController : Controller
    {
        private readonly IFamilyMemberRepository familyMembers;
        private readonly IMapper mapper;
        private readonly IUserRoleCheck userRoleCheck;
        private readonly IEnumsCheck enumsCheck;

        public FamilyController(IFamilyMemberRepository familyMembers, IMapper mapper, IUserRoleCheck userRoleCheck,IEnumsCheck enumsCheck)
        {
            this.familyMembers = familyMembers;
            this.mapper = mapper;
            this.userRoleCheck = userRoleCheck;
            this.enumsCheck = enumsCheck;
        }
        // GET: FamilyController
        [HttpGet]
        public IEnumerable<object> Get()
        {
            var result = familyMembers.GetAll();

            return (result.Select(a => new { a.ID, a.firstName, a.lasttName, a.dateOfBirth, RelationShip = Enum.GetName(typeof(RelatioshipEnum), a.relatioship), nationality = Enum.GetName(typeof(NationalityEnum), a.nationality) }));
        }
        // GET: FamilyController
        [HttpGet]
        [Route("FamilyByStudentId")]

        public IActionResult GetStudentFamily(int studentid)
        {
            if (!familyMembers.CheckStudent(studentid))
                return BadRequest("No Student Find");
            var result = familyMembers.FindFamiliesByStudent(studentid);

            return Ok(result.Select(a => new { a.ID, a.firstName, a.lasttName, a.dateOfBirth, RelationShip = Enum.GetName(typeof(RelatioshipEnum), a.relatioship), nationality = Enum.GetName(typeof(NationalityEnum), a.nationality),student=a.student.firstName }));
        }

        // GET: FamilyController/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0) return BadRequest("Please Enter Valid Id");
            var family = await familyMembers.FindAsync(id);
            if (family == null) return BadRequest("No Family Members Find With this ID");
            else
                return Ok(new
                {
                    family.ID,
                    family.firstName,
                    family.lasttName,
                    family.dateOfBirth,
                    family.studentId,
                    nationality = Enum.GetName(typeof(NationalityEnum), family.nationality),
                    RelationShip = Enum.GetName(typeof(RelatioshipEnum), family.relatioship)
                });

        }

        // GET: FamilyController/Create
        [HttpPost]
        public async Task<IActionResult> Post(FamilyMemberVM familyMember)
        {
            
            if (!familyMembers.CheckStudent(familyMember.studentId)) 
                return BadRequest("No Student Find");
            var familymember = new FamilyMember();
            var std = mapper.Map(familyMember, familymember);
             await familyMembers.AddAsync(std);
            familyMembers.Complete();
            return Ok(new
            {
                familyMember.firstName,
                familyMember.lasttName,
                familyMember.dateOfBirth,
                familyMember.studentId,
                nationality = Enum.GetName(typeof(NationalityEnum), familyMember.Nationality),
                RelationShip = Enum.GetName(typeof(RelatioshipEnum), familyMember.Relatioship)
            });
        }

        // POST: FamilyController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: FamilyController/Edit/5
        [HttpPut]
        public async Task<object> Put([FromBody] EditFamilyMemberVM familyMember)
        {
            if (!enumsCheck.NationalitiesCheckByid(familyMember.Nationality)) return BadRequest("Choose correct Nationality");
            if (!enumsCheck.RelationsCheckByid(familyMember.Relatioship)) return BadRequest("Choose correct Relation");
            if (!await userRoleCheck.checkUser(familyMember.userId)) return Unauthorized("user not Found");
            if (!await userRoleCheck.checkrole(familyMember.userId)) Forbid("You have no permissions to edit student information");
            if (!familyMembers.CheckStudent(familyMember.studentId))
                return BadRequest("No Student Find");
            int? id = familyMember.id;
            var familumember = await familyMembers.FindAsync(id);

            if (familumember == null) return BadRequest("No Family Member Find With this ID");
            familumember.nationality = (NationalityEnum)Enum.Parse(typeof(NationalityEnum), familyMember.Nationality.ToString());
            familumember.relatioship = (RelatioshipEnum)Enum.Parse(typeof(RelatioshipEnum), familyMember.Relatioship.ToString());
            mapper.Map(familyMember, familumember);

            familyMembers.Update(familumember);
            familyMembers.Complete();
            return Ok(new
            {
                familumember.ID,
                familumember.firstName,
                familumember.lasttName,
                familumember.dateOfBirth,
                familumember.studentId,
                nationality = Enum.GetName(typeof(NationalityEnum), familumember.nationality),
                RelationShip = Enum.GetName(typeof(RelatioshipEnum), familumember.relatioship)
            });
        }

        // POST: FamilyController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: FamilyController/Delete/5
        [HttpDelete]
        public async Task<object> Delete(DeleteStudentVM familymember)
        {
            if (!await userRoleCheck.checkUser(familymember.userId)) return Unauthorized("user not Found");
            if (!await userRoleCheck.checkrole(familymember.userId)) Forbid("You have no permissions to edit student information");
            int? id = familymember.id;
            var familyMember = await familyMembers.FindAsync(id);
            if (familyMember == null) return BadRequest("No Family Member Find With this ID");
            familyMembers.Delete(familyMember);
            familyMembers.Complete();
            return Ok("Successfully Deleted");
        }

        // POST: FamilyController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
