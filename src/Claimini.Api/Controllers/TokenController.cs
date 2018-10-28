using Claimini.Api.Model;
using Claimini.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Claimini.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenService _tokenService;
        public TokenController(IJwtTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult GenerateToken([FromBody] TokenViewModel vm)
        {
            var token = _tokenService.BuildToken(vm.Email);

            return Ok(new {token});
        }
    }
}
