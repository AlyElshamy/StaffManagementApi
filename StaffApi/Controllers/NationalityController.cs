using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffApi.Entities;
using StaffApi.Interfaces;
using StaffApi.Repositories;

namespace StaffApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalityController : Controller
    {
        public NationalityController()
        {
        }
        [HttpGet]
        public IActionResult Get()
        {
            var result = Enum.GetValues(typeof(NationalityEnum));
            var stringlist = new List<string>();
            if (result == null) return BadRequest(result);

            else
            {
                foreach (var item in result)
                {
                    stringlist.Add(Enum.GetName(typeof(NationalityEnum), item));
                }
                return Ok(stringlist);
            }
        }
    }
}
