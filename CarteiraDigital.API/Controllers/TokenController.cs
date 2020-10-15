using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarteiraDigital.API.Token;
using CarteiraDigital.Infrastructure.CrossCutting.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NLog.Fluent;

namespace CarteiraDigital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpPost("Token")]
        public ActionResult<UserToken> Token([FromBody] UserInfo userInfo)
        {
            try
            {
                var success = API.Token.Token.Validate(userInfo);
                if (success)
                {
                    var token = API.Token.Token.BuildToken(userInfo);
                    return Ok(token);
                }

                ModelState.AddModelError(string.Empty, "Login inválido!");
                return BadRequest(ModelState);
            }
            catch (Exception error)
            {
                LogHelper.Error("Ocorreu um erro ao obter o token",  error);
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao obter o token");
            }
        }
    }
}
