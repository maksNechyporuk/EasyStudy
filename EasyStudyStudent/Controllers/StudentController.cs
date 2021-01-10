using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasyStudyStudent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StudentController : ControllerBase
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _studentService;
        public StudentController(
            UserManager<DbUser> userManager,
            SignInManager<DbUser> signInManager,
            ILogger<StudentController> logger,
            IStudentService studentService
        )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._studentService = studentService;
        }
        [HttpGet]
        [Route("students")]
        public async Task<List<StudentVM>> GetStudents()
        {
            var list = _studentService.GetStudents();
            return await list;
        }

        [HttpGet]
        [Route("students/searchByName/{name}")]
        public async Task<List<StudentVM>> GetStudentsByName([FromRoute] string name)
        {
            var list = _studentService.GetStudentsByName(name);
            return await list;
        }

        [HttpGet]
        [Route("students/searchByAge/{a}/{b}")]
        public async Task<List<StudentVM>> GetStudentsByAge([FromRoute] int a, int b)
        {
            var list = _studentService.GetStudentsByAge(a, b);
            return await list;
        }

        [HttpGet]
        [Route("students/searchByTeacher/{id}")]
        public async Task<List<StudentVM>> GetStudentsByTeacher([FromRoute] long id)
        {
            var list = _studentService.GetStudentsByTeacher(id);
            return await list;
        }
        [HttpGet]
        [Route("students/searchById/{Id}")]
        public async Task<StudentVM> GetStudentById([FromRoute] long Id)
        {
            return await _studentService.GetStudentById(Id);
        }
        [HttpGet]
        [Route("students/searchByGroup/{Id}")]
        public async Task<List<StudentVM>> GetStudentsByGroup([FromRoute] long Id)
        {
            var list = _studentService.GetStudentsByGroup(Id);
            return await list;
        }
        [HttpPut]
        [Route("students/Register")]
        public async Task<IActionResult> RegisterStudent([FromBody] StudentRegisterVM model)
        {
            var  rezult = await _studentService.Create(model);
            if (rezult)
            {
                return Ok();
            }
            else
            {
                var invalid = new Dictionary<string, string>();
                invalid.Add("email", "Користувач з даною електронною поштою уже зареєстрований");
                return BadRequest(invalid);
            }
        }
    }
}
