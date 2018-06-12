using System;
using CycleRoutesCore.Domain.Interfaces;
using CycleRoutesCore.Domain.Models;
using CycleRoutesCore.WebAPI.Auth;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using CycleRoutesCore.WebAPI.Helpers;

namespace CycleRoutesCore.WebAPI.Controllers
{
    [EnableCors("AllowAny")]
    [Authorize(Policy = "JWTAuthorize")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;

        public UsersController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        [AllowAnonymous]
        [Route("{userId:int}")]
        [HttpGet]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null) return BadRequest();
            else return Ok(user);
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormCollection form)
        {
            int userId = Convert.ToInt32(form["UserId"][0]);

            if (form.Files.Count > 0)
            {
                var uploader = new ImgurUploader(); ;
                foreach (IFormFile file in form.Files)
                {
                    if (file.Length > 0)
                    {
                        var imgUrl = "";
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            imgUrl = uploader.UploadImage(s);
                        }

                        await _userRepository.UploadImage(imgUrl, userId);
                    }
                }
            }
            return this.Content("OK");
        }

        [Route("delete/{userId:int}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteImage(int userId)
        {
            await _userRepository.DeleteImage(userId);
            return Ok();
        }

    }
}