using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShareCalendar.Model;
using back.Model.Request;

namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthManagementController : ControllerBase
    {
       private readonly DataContext _context;
        private IConfiguration _config;

        public AuthManagementController(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUser()
        {
            return Ok(_context.Users);
        }

        [HttpPost("registration")]
        public IActionResult Registration([FromBody] UserRegistrationRequest registration)
        {
            var usernames = _context.Users!.FirstOrDefault(user => user.Username!.ToUpper() == registration.Username!.ToUpper());
            var emails = _context.Users!.FirstOrDefault(user => user.Email!.ToUpper() == registration.Email!.ToUpper());

            if (usernames == null && emails == null)
            {
                var lastId = _context.Users!.OrderBy(x => x.Id).Last().Id;
                 try
                 {
                     var newUser = new User
                    {
                        Id = lastId + 1,
                        Username = registration.Username,
                        Email = registration.Email,
                        Firstname = registration.Firstname,
                        Lastname = registration.Lastname,
                        Birthday = registration.Birthday,
                        Password = HashPassword(registration.Password!)
                    };
                    _context.Add(newUser);
                    _context.SaveChanges();

                    return CreatedAtAction(nameof(GetUser), registration);
                 }
                 catch
                 {
                     return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data");
                 }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest login)
        {
            var dbUser = _context.Users!.FirstOrDefault(user => user.Username == login.Username);

            if (dbUser == null) return NotFound("User is not found");

            if (dbUser.Password! != HashPassword(login.Password!)) return Unauthorized();

            var token = GenerateJSONWebToken(dbUser);

            return Ok(new {token = token});
        }

        private string HashPassword(string password)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: new byte[0],
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],

              _config["Jwt:Issuer"],
              new List<Claim> { new Claim("Id", user.Id.ToString()!) },
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}