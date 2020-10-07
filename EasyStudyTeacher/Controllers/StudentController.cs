using System.Collections.Generic;
using System.Threading.Tasks;
using EasyStudy.BLL.Interfaces;
using EasyStudy.BLL.Models;
using EasyStudy.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasyStudy.Controllers
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
        [Route("students/searchByName/{Name}")]
        public async Task<List<StudentVM>> GetStudentsByName([FromRoute] string Name)
        {
            var list = _studentService.GetStudentsByName(Name);
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
        [Route("students/searchByTeacher/{Id}")]
        public async Task<List<StudentVM>> GetStudentsByTeacher([FromRoute] long Id)
        {
            var list = _studentService.GetStudentsByTeacher(Id);
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
    }
}
