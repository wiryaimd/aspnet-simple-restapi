using aspnet_simple_restapi.Dtos;
using aspnet_simple_restapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace aspnet_simple_restapi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        private string CreateToken(User user) {
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor() {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString()) // role jadikan toString() agar tidak enum type
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings:SecretKey").Value)), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddMinutes(5) // inget clockSkew jadiin zero agar tidak di + 5mins
            };
            Console.WriteLine("role to str: " + user.Role.ToString());

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Auth(AuthDto authDto) {
            User? user = _context.Users.Where(delegate (User u)
            {
                Console.WriteLine("u: " + u.Email + " w " + authDto.Email + "n pass " + u.Password + " w : " + authDto.Password);
                return u.Email.Equals(authDto.Email) && u.Password.Equals(authDto.Password);
            }).FirstOrDefault();

            if (user == null) {
                return NotFound();
            }

            return Ok(CreateToken(user));
        }

        [HttpPost("/register")]
        public async Task<ActionResult<User>> RegisterUser(UserDto userDto) {
            bool isAvailable = _context.Users.Any(u => u.Email.Equals(userDto.Email));
            if (isAvailable) {
                //return Conflict();
                return Problem("Email already exists", statusCode: 409);
            }

            string pass = userDto.Password;
            if (!Regex.Match(pass, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").Success) {
                return Problem("Password must contains Upper case, lower case, number, symbol and length more than 8", statusCode: 400);
            }

            User user = new User()
            {
                Id = Guid.NewGuid(),
                Email = userDto.Email,
                Password = userDto.Password,
                Gender = userDto.Gender,
                RegisterDate = DateTime.Now,
                Role = userDto.Role,
                Address = userDto.Address
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(RegisterUser), new { Id = user.Id }, user);

        }
    }
}
