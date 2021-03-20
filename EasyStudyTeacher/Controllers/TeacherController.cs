using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models.AccountModels;
using BLL.Models.ErrorsModel;
using BLL.Models.TeacherModels;
using DAL.Entities;
using EasyStudy.Core.Controller;
using EasyStudy.Helpers;
using JWT.Token.Service;
using JWT.Token.Service.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public TeacherController(
               IWebHostEnvironment env,
            IConfiguration configuration,
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
            this._configuration = configuration;
            this._env = env;
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
        [Route("GetTeacherByGroup")]
        public async Task<TeacherVM> GetTeacherByGroup(long Id)
        {
            return await _teacherService.GetTeacherByGroup(Id);
        }

        [HttpGet]
        [Route("GetTeacherById")]
        public async Task<TeacherVM> GetTeacherById(long Id)
        {
            return await _teacherService.GetTeacherById(Id);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterTeacher([FromBody] TeacherRegisterVM model)
        {

            return await HandleRequestAsync(async () =>
            {
                string imageName = Path.GetRandomFileName() + ".jpg";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploaded\Users");

                string pathSaveImages = InitStaticFiles
                         .CreateImageByFileName(_env, _configuration,
                              new string[] { Directory.GetCurrentDirectory(), @"wwwroot", "Uploaded", "Users" },
                              imageName,
                              model.Photo);

                //+38 (098) 665 34 18
                model.Photo = imageName;

                var rezult = await _teacherService.Create(model);
                if (rezult)
                {
                    var user = _userManager.FindByEmailAsync(model.Email).Result;
                    var teacher = await _teacherService.GetTeacherById(user.Id);
                    JwtInfo jwtInfo;
                    if (teacher != null)
                    {
                        // Return token
                        jwtInfo = new JwtInfo()
                        {
                            Token = _jwtTokenService.CreateToken(user),
                            RefreshToken = _jwtTokenService.CreateRefreshToken(user),
                            SchoolId = teacher.SchoolId.ToString()
                        };
                    }
                    else
                    {
                        // Return token
                        jwtInfo = new JwtInfo()
                        {
                            Token = _jwtTokenService.CreateToken(user),
                            RefreshToken = _jwtTokenService.CreateRefreshToken(user),
                        };
                    }

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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO loginModel)
        {
            // Auto return errors from viewModel and other global errors
            return await HandleRequestAsync(async () =>
            {
                int countOfAttempts = this.HttpContext.Session.GetInt32("LoginAttemts") ?? 0;
                countOfAttempts++;
                this.HttpContext.Session.SetInt32("LoginAttemts", countOfAttempts);

                this._logger.LogDebug("Start method LoginUser...");
                var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false);

                if (!result.Succeeded)
                {
                    var invalid = new Dictionary<string, string>
                     {
                        { "email", "Не правильно введені дані" },
                        {"showCaptcha", (countOfAttempts > 4 ? true : false).ToString() }
                     };
                    return BadRequest(invalid);
                    //return BadRequest(new InvalidData
                    //{
                    //    Invalid = "Не правильно введені дані",
                    //    ShowCaptcha = countOfAttempts > 4 ? true : false
                    //});
                }

                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                await _signInManager.SignInAsync(user, isPersistent: false);

                //if (countOfAttempts > 4)
                //{
                //    // TODO: Captcha validation
                //    this._recaptchaService.IsValid(loginModel.RecaptchaToken);
                //}

                var teacher = await _teacherService.GetTeacherById(user.Id);
                // Return token
                JwtInfo jwtInfo;
                if (teacher != null)
                {
                    // Return token
                    jwtInfo = new JwtInfo()
                    {
                        Token = _jwtTokenService.CreateToken(user),
                        RefreshToken = _jwtTokenService.CreateRefreshToken(user),
                        SchoolId = teacher.SchoolId.ToString()
                    };
                }
                else
                {
                    // Return token
                    jwtInfo = new JwtInfo()
                    {
                        Token = _jwtTokenService.CreateToken(user),
                        RefreshToken = _jwtTokenService.CreateRefreshToken(user),
                    };
                }

                this.HttpContext.Session.SetInt32("LoginAttemts", 0);
                this._logger.LogDebug("End method LoginUser...");

                return Ok(jwtInfo);
            });
        }

        [HttpGet]
        [Route("TeachersWithoutGroup")]
        public async Task<IActionResult> GetTeachersWithoutGroup()
        {
            var list = await _teacherService.GetTeachersWithoutGroup();
            if (list.Count > 0)
            {
                return Ok(list);
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("DeleteTeachers")]
        public async Task<IActionResult> DeleteSelected([FromBody] int[] ids)
        {
            try
            {
                await _teacherService.DeleteTeacher(ids);
                return Ok();

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("CreateTeacher")]
        public async Task<IActionResult> CreateTeacher([FromBody] TeacherCreateVM model)
        {
            try
            {
                if (await _teacherService.CreateTeacher(model))
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
        [Route("UpdateTeacher")]
        public async Task<IActionResult> UpdateTeacher([FromBody] TeacherCreateVM model)
        {
            try
            {
                if (await _teacherService.UpdateTeacher(model))
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
        [Route("getTeachersBySchool")]
        public async Task<List<TeacherVM>> GetTeachersBySchool(long Id)
        {
            return await _teacherService.GetTeachersBySchool(Id);
        }
    }
}