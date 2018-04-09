﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CycleRoutesCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}