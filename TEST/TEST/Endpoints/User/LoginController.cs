using Test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TEST.Models;
using Azure;
using Swashbuckle.AspNetCore.Annotations;

namespace Test.Controllers
{
	[Route("user")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly IConfiguration configuration;

		public LoginController(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		[AllowAnonymous]
		[HttpPost]
		[SwaggerOperation(
		Summary = "Login",
		Description = "Login",
		OperationId = "Login",
		Tags = new[] { "User Endpoint" })]
		public IActionResult Login([FromBody] UserLogin userLogin)
		{
			var user = Authenticate(userLogin);

			if (user != null)
			{
				var token = Generate(user);
				return Ok(token);
			}

			return NotFound("User not found");
		}

		private string Generate(UserModel user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var roles = new[] { "Admin", "Seller", "Manager", "Driver" };

			var claims = new List<Claim>
			{
				new Claim("user_id", user.users_id.ToString()),
				new Claim(ClaimTypes.Email, user.email),
				new Claim(ClaimTypes.Name, user.u_firstname),
				new Claim(ClaimTypes.Surname, user.u_lastname),
				new Claim("role_id", user.role_id.ToString()),
				new Claim(ClaimTypes.Role, user.rolename)
			};

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private static UserModel? Authenticate(UserLogin userLogin)
		{
			var currentUser = UserConstants.GetUsersFromDatabase().FirstOrDefault(x => x.email.ToLower() == userLogin.email.ToLower() &&
			x.user_password == userLogin.user_password);

			if (currentUser != null)
			{
				return currentUser;
			}

			return null;
		}
	}
}