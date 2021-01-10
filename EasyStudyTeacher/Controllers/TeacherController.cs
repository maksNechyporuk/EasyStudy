using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models.TeacherModels;
using DAL.Entities;
using EasyStudy.Core.Controller;
using JWT.Token.Service;
using JWT.Token.Service.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasyStudy.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TeacherController : WebControllerBase
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly ILogger<TeacherController> _logger;
        private readonly ITeacherService _teacherService;
        private readonly IJWTTokenService _jwtTokenService;

        public TeacherController(
            UserManager<DbUser> userManager,
            SignInManager<DbUser> signInManager,
            ILogger<TeacherController> logger,
            ITeacherService teacherService,
            IJWTTokenService jwtTokenService
        ) : base(logger)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._teacherService = teacherService;
            this._jwtTokenService = jwtTokenService;
        }

        [HttpGet]
        [Route("teacher")]
        public async Task<List<TeacherVM>> GetStudents()
        {
            return await _teacherService.GetTeachers();
        }
        [HttpGet]
        [Route("getTeachersByStudent/{Id}")]
        public async Task<TeacherVM> GetTeachersByStudent([FromRoute] long Id)
        {
            return await _teacherService.GetTeachersByStudent(Id);
        }

        [HttpGet]
        [Route("getTeachersByName/{name}")]
        public async Task<List<TeacherVM>> GetTeachersByName([FromRoute] string name)
        {
            return await _teacherService.GetTeachersByName(name);
        }

        [HttpGet]
        [Route("GetTeachersByAge/{a}/{b}")]
        public async Task<List<TeacherVM>> GetTeachersByAge([FromRoute] DateTime a, [FromRoute] DateTime b)
        {
            return await _teacherService.GetTeachersByAge(a, b);
        }

        [HttpGet]
        [Route("GetTeacherByGroup/{Id}")]
        public async Task<TeacherVM> GetTeacherByGroup([FromRoute] long Id)
        {
            return await _teacherService.GetTeacherByGroup(Id);
        }

        [HttpGet]
        [Route("GetTeacherById/{Id}")]
        public async Task<TeacherVM> GetTeacherById([FromRoute] long Id)
        {
            return await _teacherService.GetTeacherById(Id);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterTeacher([FromBody] TeacherRegisterVM model)
        {

            return await HandleRequestAsync(async () =>
            {
                //+38 (098) 665 34 18
                var rezult = await _teacherService.Create(model);
                if (rezult)
                {
                    var user = _userManager.FindByEmailAsync(model.Email).Result;
                    // Return token
                    JwtInfo jwtInfo = new JwtInfo()
                    {
                        Token = _jwtTokenService.CreateToken(user),
                        RefreshToken = _jwtTokenService.CreateRefreshToken(user)
                    };
                    this._logger.LogDebug("End method LoginUser...");

                    return Ok(jwtInfo);
                }
                else
                {
                    var invalid = new Dictionary<string, string>
                     {
                        { "email", "Користувач з даною електронною поштою уже зареєстрований" }
                     };
                    return BadRequest(invalid);
                }

            });
        }
    }
}