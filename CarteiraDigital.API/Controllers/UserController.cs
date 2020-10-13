using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using CarteiraDigital.Domain.Service;
using CarteiraDigital.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraDigital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost, Route("Register")]
        public async Task<IResult<UserModel>> Register(UserModel userModel)
        {
            return await _userService.Register(userModel);
        }
    }
}
