using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models.AccountModels;
using BLL.Models.ErrorsModel;
using BLL.Models.GroupsModels;
using BLL.Models.TeacherModels;
using DAL.Entities;
using EasyStudy.Core.Controller;
using JWT.Token.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasyStudy.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GroupController : WebControllerBase
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly ILogger<GroupController> _logger;
        private readonly IGroupService _groupService;

        public GroupController(
            UserManager<DbUser> userManager,
            SignInManager<DbUser> signInManager,
            ILogger<GroupController> logger,
            IGroupService groupService
        ) : base(logger)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
            this._groupService = groupService;
        }

        [HttpGet]
        [Route("getGroups")]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await _groupService.GetGroups());
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] UpdateAndCreateGroupVM model)
        {
            if (await _groupService.CreateGroup(model))
            {
                return Ok();
            }
            var invalid = new Dictionary<string, string>
            {
                        { "group", "Дана група вже створена" }
            };
            return Ok(invalid);
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] UpdateAndCreateGroupVM model)
        {
            if (await _groupService.EditGroup(model))
            {
                return Ok();
            }
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _groupService.DeleteGroup(id))
            {
                return Ok();
            }
            var invalid = new Dictionary<string, string>
            {
              { "group", "Групу не знайдено" }
            };
            return Ok(invalid);
        }

        [HttpPost]
        [Route("DeleteSelected")]
        public async Task<IActionResult> DeleteSelected([FromBody] int[] GroupsId)
        {
            foreach (var item in GroupsId)
            {
                await _groupService.DeleteGroup(item);
            }
            return Ok();
        }

        [HttpGet]
        [Route("getGroupsBySchool")]
        public async Task<List<GroupVM>> GetGroupsBySchool(long Id)
        {
            return await _groupService.GetGroupsBySchool(Id);
        }
    }
}
