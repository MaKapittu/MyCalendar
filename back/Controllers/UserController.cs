using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareCalendar.Model;

namespace ShareCalendar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private IConfiguration _config;

        public UserController(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUser()
        {
            return Ok(_context.Users);
        }

        [HttpGet("loggedUser")]
        public ActionResult GetLoggedUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            try{
            var id = int.Parse(identity!.FindFirst("Id")!.Value);
            return Ok(_context.Users!.FirstOrDefault(x => x.Id == id));
            }
            catch{
                
                return Unauthorized();
            }
        }

        [HttpGet("{username}")]
        public ActionResult<User> GetUser(string username) 
        {
            var usernameList = _context.Users!.Include(x => x.UsersEvents).AsQueryable();

            if (username != null)
            {
                usernameList = usernameList.Where(x => x.Username!.ToUpper().Contains(username.ToUpper())); 
            }

            return Ok(usernameList);
        }
        // GET: api/Users/5/posts
        [HttpGet("{id}/event")]
        public async Task<ActionResult<IEnumerable<Event>>> GetUserEvents(int id)
        {
            return await _context.Events!.Where(x => x.OwnerId == id).ToListAsync();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User request)
        {
            try
            {
                var dbUser = _context.Users!.AsNoTrackingWithIdentityResolution().FirstOrDefault(x => x.Id == id);         
                dbUser!.Username = request.Username;
                dbUser.Email = request.Email;
                dbUser.Firstname = request.Firstname;
                dbUser.Lastname = request.Lastname;
                dbUser.Birthday = request.Birthday;
                dbUser.Status = request.Status;

                if ( dbUser == null)
                    return BadRequest("No matching users Id");

                _context.Update(request);
                _context.SaveChanges();
                
                return Ok(request);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
            }
        }

        [HttpPost]
        public ActionResult<User> PostUsers(User user)
        {
            var dbUser = _context.Users!.Find(user.Id);
            var usernames = _context.Users!.Where(name => name.Username!.ToUpper() != user.Username!.ToUpper());
            var emails = _context.Users!.Where(mail => mail.Email!.ToUpper() != user.Email!.ToUpper());

            if (dbUser == null && usernames != null && emails != null)
            {
                var lastId = _context.Users.OrderBy(x => x.Id).Last().Id;
                _context.Add(user);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetUser), new { Id = user.Id }, user);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{username}")]
        public IActionResult DeleteUser(string username)
        {
           var user = _context.Users!.AsNoTrackingWithIdentityResolution()
           .FirstOrDefault(x => x.Username!.ToUpper() == username.ToUpper());

            if (user == null)
            {
             return NotFound();
            }

            _context.Remove(user);
            _context.SaveChanges();

            return Ok(username);
         }
    }
}