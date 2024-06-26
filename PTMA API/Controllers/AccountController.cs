﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Data;
using PTMA.DB;
using PTMA_API.Model.user;

namespace library.Controllers
{
    [Route("api/v{version}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly PtmaDBContext _db;


        public AccountController(
            UserManager<UserModel> userManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager,
            PtmaDBContext db)
        {
            _userManager = userManager;
            _configuration = configuration;
            this.roleManager = roleManager;
            _db = db;
        }
        /// <summary>
        /// this function used for add new user 
        /// </summary>
        /// <param name="newUserDTO">model for new user [name ,email, password]</param>
        /// <returns></returns>

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterUser([FromBody] NewUserDTO newUserDTO)
        {
            if (ModelState.IsValid)
            {
                var userModel = new UserModel
                {
                    UserName = newUserDTO.Name,
                    Email = newUserDTO.Email,

                };
                var result = await _userManager.CreateAsync(userModel, newUserDTO.Password);

                if (result.Succeeded)
                {
                    return Ok("User registered successfully");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// this function used for login 
        /// </summary>
        /// <param name="user">model [email,Password]</param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO user)
        {
            if (ModelState.IsValid)
            {
                var userModel = await _userManager.FindByEmailAsync(user.Email);
                if (userModel != null)
                {
                    if (await _userManager.CheckPasswordAsync(userModel, user.Password))
                    {
                        var userRoles = await _userManager.GetRolesAsync(userModel);

                        // SigningCredentials
                        var key = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        // Claims
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userModel.Email),
                    new Claim(ClaimTypes.Name, userModel.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };



                        var token = new JwtSecurityToken(
                            claims: claims,
                            issuer: _configuration["JWT:Issuer"],
                            audience: _configuration["JWT:Audience"],
                            expires: DateTime.Now.AddMonths(1),
                            signingCredentials: signingCredentials
                        );
                        //create token for authorization 
                        var _token = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expires = token.ValidTo
                        };

                        return Ok(_token);
                    }
                    return Unauthorized("Invalid password");
                }
                return Unauthorized("Email not found");
            }
            return BadRequest(ModelState);
        }
    }
}
