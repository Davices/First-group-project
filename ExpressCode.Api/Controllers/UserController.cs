using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressCode.IRepository;
using ExpressCode.Services.Admin.User;


namespace ExpressCode.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserRepository Repository { get; set; }


        //构造函数注入
        private readonly IUserService _userservice;
        public UserController( IUserService userService)
        {
            _userservice = userService;
        }

        [HttpGet]
        public IActionResult Show()
        {
            return Ok(_userservice.GetUser()); 
        }
    }
}
