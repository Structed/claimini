using System.Threading.Tasks;
using Claimini.Api.Services;
using Claimini.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Claimini.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenService tokenService;
        private readonly UserManager<IdentityUser> userManager;

        public TokenController(IJwtTokenService tokenService, UserManager<IdentityUser> userManager)
        {
            this.tokenService = tokenService;
            this.userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TokenViewModel vm)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var user = await this.userManager.FindByEmailAsync(vm.Email);
            var correctUser = await this.userManager.CheckPasswordAsync(user, vm.Password);

            if (correctUser == false)
            {
                return BadRequest("Username or password is incorrect");
            }

            string token = this.tokenService.BuildToken(vm.Email);
            var tokenResponse = new TokenResponse
            {
                BearerToken = token
            };
            return new OkObjectResult(tokenResponse);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] TokenViewModel vm)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var result = await this.userManager.CreateAsync(new IdentityUser()
            {
                UserName = vm.Email,
                Email = vm.Email,
            }, vm.Password);

            if (result.Succeeded == false)
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
