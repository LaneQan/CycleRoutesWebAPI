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
        public async Task<IActionResult> Upload(IFormFile photo)
        {
            if (photo == null ||
                photo.Length == 0 || photo.Length > (1024 * 1024 * 100) ||
                !(new string[] { ".png", ".jpg", ".jpeg" }).Contains(Path.GetExtension(photo.FileName)))
                return BadRequest();
            string fileName;
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                fileName = photo.FileName.GetHashCode().ToString("x2") + Path.GetExtension(photo.FileName);
                using (var memStream = new MemoryStream())
                {
                    await photo.CopyToAsync(memStream);
                    SHA1 sha = new SHA1CryptoServiceProvider();
                    memStream.Position = 0;
                    fileName = BitConverter.ToString(sha.ComputeHash(memStream)).Replace("-", "").ToLower() +
                               Path.GetExtension(photo.FileName);
                    if (System.IO.File.Exists(Path.Combine(path, fileName)))
                        return Ok(fileName);

                    using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.CreateNew))
                    {
                        memStream.WriteTo(fileStream);
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(fileName);
        }

    }
}