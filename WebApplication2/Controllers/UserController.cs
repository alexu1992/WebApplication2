using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Request;
using Model.Response;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public UserResponse Get(string username)
        {
            try
            {
                return _userService.Get(new UserRequest() { Username = username });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
