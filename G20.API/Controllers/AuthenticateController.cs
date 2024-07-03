using G20.API.Auth;
using G20.API.Factories.Users;
using G20.API.Models;
using G20.Core.Domain;
using G20.Core.Enums;
using G20.Core.IndentityModels;
using G20.Service.Account;
using G20.Service.UserRoles;
using G20.Service.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QRCoder.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace G20.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IUserFactoryModel _userFactoryModel;
        
        private readonly IConfiguration _configuration;

        public AuthenticateController(
            IAuthenticateService authenticateService,
            IUserService userService,
            IUserRoleService userRoleService,
            IUserFactoryModel userFactoryModel,
            IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _userService = userService;
            _userRoleService = userRoleService;
            _userFactoryModel = userFactoryModel;
            _configuration = configuration;
        }

        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login([FromBody] LoginModel model)
        //{
        //    var user = await _authenticateService.FindByNameAsync(model.Email);
        //    if (user != null && await _authenticateService.CheckPasswordAsync(user, model.Password))
        //    {
        //        var userRoles = await _authenticateService.GetRolesAsync(user);

        //        var authClaims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, user.UserName),
        //            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        };

        //        foreach (var userRole in userRoles)
        //        {
        //            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        //        }

        //        var token = GetToken(authClaims);

        //        return Ok(new
        //        {
        //            token = new JwtSecurityTokenHandler().WriteToken(token),
        //            expiration = token.ValidTo
        //        });
        //    }
        //    return Unauthorized();
        //}

        #region Privatte  Method(s)

        private async Task<IActionResult> CheckAuthenticationByRoleType(LoginModel model,RoleEnum? roleEnum=null)
        {
            var user = await _userService.GetByEmailAndPasswordAsync(model.Email, model.Password);
            if (user != null)
            {
                var userRoles = await _userRoleService.GetRoleByUserIdAsync(user.Id);
                if ( roleEnum.HasValue && !userRoles.Any(c=> c.Name.ToLower().Trim()== roleEnum.ToString().ToLower().Trim()))
                {
                    return Unauthorized();
                }
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole.Name));
                }
                var token = GetToken(authClaims);
                var userModel = await _userFactoryModel.PrepareUserModelAsync(user);
                if (userRoles.Any())
                {
                    userModel.RoleNames= userRoles.Select(x=>x.Name).ToList();
                }
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    user= userModel
                });
            }
            return Unauthorized();
        }
        #endregion

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            return await CheckAuthenticationByRoleType(model);            
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _authenticateService.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
               
            };
            var result = await _authenticateService.CreateAsync(user, model.Password);
            if (!result)
                //return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." + result.Errors.First().Description });
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again."  });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _authenticateService.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _authenticateService.CreateAsync(user, model.Password);
            if (!result)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _authenticateService.RoleExistsAsync(UserRoles.Admin))
                await _authenticateService.CreateAsync(UserRoles.Admin);
            if (!await _authenticateService.RoleExistsAsync(UserRoles.User))
                await _authenticateService.CreateAsync(UserRoles.User);

            if (await _authenticateService.RoleExistsAsync(UserRoles.Admin))
            {
                await _authenticateService.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _authenticateService.RoleExistsAsync(UserRoles.Admin))
            {
                await _authenticateService.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SymmetricSecurityKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddYears(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
                
        [HttpPost]
        [Route("enduser-login")]
        public async Task<IActionResult> EndUserLogin([FromBody] LoginModel model)
        {
            return await CheckAuthenticationByRoleType(model, RoleEnum.User);
        }

        [HttpPost]
        [Route("security-login")]
        public async Task<IActionResult> SecurityLogin([FromBody] LoginModel model)
        {
            return await CheckAuthenticationByRoleType(model, RoleEnum.Security);
        }

    }
}   
