﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.API.Attributes;
using TicketingSystem.Application.Abstractions.IServices;
using TicketingSystem.Domain.Entities.DTOs;
using TicketingSystem.Domain.Entities.Enums;
using TicketingSystem.Domain.Entities.Models;

namespace TicketingSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class bUserController : ControllerBase
    {
        private readonly IUserService _userService;
        public bUserController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpPost]
        //[IdentityFilter(Permission.CreateUser)]
        //public async Task<ActionResult<string>> CreateUser([FromForm] UserDTO model)
        //{
        //    var result = await _userService.Create(model);

        //    return Ok(result);
        //}

        [HttpGet]
        [IdentityFilter(Permission.GetAllUsers)]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var result = await _userService.GetAll();
            return result;
        }

        [HttpGet]
        [IdentityFilter(Permission.GetByUserId)]
        public async Task<ActionResult<User>> GetUserByID(int id)
        {
            var res = await _userService.GetById(id);
            return res;
        }

        [HttpGet]
        [IdentityFilter(Permission.GetByUserEmail)]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var res = await _userService.GetByEmail(email);
            return res;
        }

        [HttpGet]
        [IdentityFilter(Permission.GetByUserName)]
        public async Task<ActionResult<User>> GetUserByName(string name)
        {
            var res = await _userService.GetByName(name);
            return res;
        }

        [HttpGet]
        [IdentityFilter(Permission.GetUserPDF)]
        public async Task<IActionResult> DownloadFile()
        {
            var filePath = await _userService.GetPdfPath();

            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found");


            var fileBytes = System.IO.File.ReadAllBytes(filePath);


            var contentType = "application/octet-stream";

            var fileExtension = Path.GetExtension(filePath).ToLowerInvariant();

            return File(fileBytes, contentType, Path.GetFileName(filePath));
        }

        [HttpDelete]
        [IdentityFilter(Permission.DeleteUser)]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            var res = await _userService.Delete(id);
            return Ok(res);
        }

        [HttpPut]
        [IdentityFilter(Permission.UpdateUser)]
        public async Task<ActionResult<string>> UpdateUser(int id, [FromForm] UserDTO userDTO)
        {
            return await _userService.Update(id, userDTO);
        }

    }
}
