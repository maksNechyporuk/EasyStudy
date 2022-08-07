using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using BLL.Models.TeacherModels;
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
        [Route("searchByName/{name}")]
        public async Task<List<StudentVM>> GetStudentsByName([FromRoute] string name)
        {
            var list = _studentService.GetStudentsByName(name);
            return await list;
        }

        [HttpGet]
        [Route("searchByAge/{a}/{b}")]
        public async Task<List<StudentVM>> GetStudentsByAge([FromRoute] int a, int b)
        {
            var list = _studentService.GetStudentsByAge(a, b);
            return await list;
        }

        [HttpGet]
        [Route("searchByTeacher/")]
        public async Task<List<StudentVM>> GetStudentsByTeacher(long id)
        {
            var list = _studentService.GetStudentsByTeacher(id);
            return await list;
        }

        [HttpGet]
        [Route("searchById")]
        public async Task<StudentVM> GetStudentById(long Id)
        {
            return _studentService.GetStudentById(Id);
        }

        [HttpGet]
        [Route("searchByGroup")]
        public async Task<List<StudentVM>> GetStudentsByGroup(long Id)
        {
            var list = _studentService.GetStudentsByGroup(Id);
            return await list;
        }

        [HttpPut]
        [Route("Register")]
        public async Task<IActionResult> RegisterStudent([FromBody] StudentRegisterVM model)
        {
            var rezult = await _studentService.Create(model);
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

        [HttpGet]
        [Route("StudentsWithoutGroup")]
        public async Task<IActionResult> GetStudentsWithoutGroup()
        {
            var list = await _studentService.GetStudentsWithoutGroup();
            if (list.Count > 0)
            {
                return Ok(list);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("DeleteStudents")]
        public async Task<IActionResult> DeleteSelected([FromBody] int[] ids)
        {
            try
            {
                await _studentService.DeleteStudents(ids);
                return Ok();

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] TeacherCreateVM model)
        {
            try
            {
                if (await _studentService.CreateStudent(model))
                {
                    return Ok();
                }
                else
                {
                    var invalid = new Dictionary<string, string>
                    {
                        { "email", "Користувач з даною електронною поштою уже створений" }
                    };
                    return Ok(invalid);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] TeacherCreateVM model)
        {
            try
            {
                if (await _studentService.UpdateStudent(model))
                {
                    return Ok();
                }
                else
                {
                    var invalid = new Dictionary<string, string>
                    {
                        { "email", "Користувач з даною електронною поштою уже створений" }
                    };
                    return Ok(invalid);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("getStudentsBySchool")]
        public async Task<List<StudentVM>> GetStudentsBySchool(long Id)
        {
            return await _studentService.GetStudentsBySchool(Id);
        }

        [HttpGet]
        [Route("TestQgis")]
        public async Task<List<StudentVM>> TestQgis(long Id)
        {
            return await _studentService.GetStudentsBySchool(Id);
        }
    }
}
