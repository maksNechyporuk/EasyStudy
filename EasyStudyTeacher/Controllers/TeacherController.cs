using System.Collections.Generic;
using System.Threading.Tasks;
using EasyStudy.BLL.Interfaces;
using EasyStudy.BLL.Models;
using EasyStudy.BLL.Models.TeacherModels;
using EasyStudy.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasyStudy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TeacherController : ControllerBase
    {
       
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly ILogger<TeacherController> _logger;
        private readonly ITeacherService _teacherService;
        public TeacherController(
            UserManager<DbUser> userManager,
            SignInManager<DbUser> signInManager,
            ILogger<TeacherController> logger,
            ITeacherService teacherService
        ) 
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._teacherService = teacherService;
        }
        
        [HttpGet]
        [Route("teacher")]
        public async Task<List<TeacherVM>>  GetStudents()
        {
            return await _teacherService.GetTeachers();
        }
        
        [HttpGet]
        [Route("getTeachersByStudent/{Id}")]
        public async Task<TeacherVM> GetTeachersByStudent([FromRoute]long Id)
        {
            return await _teacherService.GetTeachersByStudent(Id);
        }
        [HttpGet]
        [Route("getTeachersByName/{name}")]
        public async Task<List<TeacherVM>>  GetTeachersByName([FromRoute] string name)
        {
            return await _teacherService.GetTeachersByName(name);
        }
        
        [HttpGet]
        [Route("GetTeachersByAge/{a}/{b}")]
        public async Task<List<TeacherVM>>  GetTeachersByAge([FromRoute] int a,[FromRoute] int b)
        {
            return await _teacherService.GetTeachersByAge(a,b);
        }
        [HttpGet]
        [Route("GetTeacherByGroup/{Id}")]
        public async Task<TeacherVM> GetTeacherByGroup([FromRoute]long Id)
        {
            return await _teacherService.GetTeacherByGroup(Id);
        }
        [HttpGet]
        [Route("GetTeacherById/{Id}")]
        public async Task<TeacherVM> GetTeacherById([FromRoute]long Id)
        {
            return await _teacherService.GetTeacherById(Id);
        }
    }
}