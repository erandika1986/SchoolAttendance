using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SchoolAttendance.Application.Common.Interfaces;
using SchoolAttendance.Application.Pipelines.User.Queries.GetUserByUsername;
using SchoolAttendance.Application.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IConfiguration config;
        private readonly ICoreDataService coreDataService;
        private readonly IMediator _mediator;

        public AuthController(ILogger<AuthController> logger, IConfiguration config, ICoreDataService coreDataService, IMediator mediator)
        {
            this.logger = logger;
            this.config = config;
            this.coreDataService = coreDataService;
            this._mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (model == null)
            {
                return Unauthorized(new { ErrorMessage = "Login failed.Please enter your password and username." });
            }

            try
            {
                var user = await _mediator.Send(new GetUserByUsernameQuery(model.Username.Trim()));

                if (user == null)
                {
                    return Unauthorized(new { ErrorMessage = "Login failed.Invalid username has entered." });
                }
                else
                {
                    bool verified = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
                    if (verified)
                    {
                        var test = config["Tokens:Key"];
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
                        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                        var roles = user.UserRoles.Select(x => x.Role.Name).ToList();
                        var userRoles = string.Join(",", roles);
                        var arrayLength = roles.Count + 4;

                        var now = DateTime.UtcNow;
                        DateTime nowDate = DateTime.UtcNow;
                        var claims = new Claim[arrayLength];
                        claims[0] = new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString());
                        claims[1] = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
                        claims[2] = new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64);
                        claims[3] = new Claim(JwtRegisteredClaimNames.Aud, "webapp");

                        for (int i = 0; i < roles.Count; i++)
                        {
                            claims[i + 4] = new Claim(ClaimTypes.Role, roles[i]);
                        }

                        var tokenOptions = new JwtSecurityToken(
                            issuer: config["Tokens:Issuer"],
                            claims: claims,
                            notBefore: nowDate,
                            expires: nowDate.AddDays(100),
                            signingCredentials: signingCredentials

                        );

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                        return Ok(new
                        {
                            Token = tokenString,
                            FullName = user.FullName,
                            Username = user.Username,
                            ProfilePic = "",
                            Roles = roles,
                            Gender = user.Gender
                        });
                    }
                    else
                    {
                        return Unauthorized(new { ErrorMessage = "Login failed.Invalid password has entered." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(new { ErrorMessage = "Login failed.Invalid password has entered." });
            }

        }
    }
}
