namespace CinemateAPI.Features.Identity
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using CinemateAPI.Data.Models;
    using CinemateAPI.Features.Identity.Models;

    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;

        public IdentityController(UserManager<User> userManager, IIdentityService identityService)
        {
            this.userManager = userManager;
            this.identityService = identityService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterUserModel input)
        {
            var user = new User
            {
                Email = input.Email,
                UserName = input.UserName,
            };

            var result = await this.userManager.CreateAsync(user, input.Password);

            if (result.Succeeded)
            {
                return Ok("User created successfully");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Login))]
        public async Task<ActionResult<string>> Login(LoginUserModel input)
        {
            var user = await this.userManager.FindByNameAsync(input.UserName);

            if (user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, input.Password);

            if (!passwordValid)
            {
                return Unauthorized();
            }

            string encyrptedToken = this.identityService.GenerateJwtToken(user);

            return encyrptedToken;
        }
    }
}
