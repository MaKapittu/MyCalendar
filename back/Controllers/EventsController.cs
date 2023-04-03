using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareCalendar.Model;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ShareCalendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly DataContext _context;

        public EventsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public  IActionResult GetEvents() 
        {
            var events = _context.Events!.OrderBy(x => x.Date).AsQueryable().Where(x => x.OwnerId == GetOwnerId());
            return Ok(events);
        }

        [HttpGet("allEvents")]
        public  IActionResult GetAllEvents() 
        {
            var events = _context.Events!;
            return Ok(events);
        }
        
        [HttpGet("{title}")]
        public ActionResult<Event> GetEvent(string title)
        {
            var ev = _context.Events!.AsQueryable().Where(x =>x.OwnerId == GetOwnerId());

            if (ev != null)
            {
                ev = ev.Where(x => x.Title!.ToUpper().Contains(title.ToUpper())); 
            }

            return Ok(ev);
        }
        [HttpPost]
        public ActionResult<Event> CreateEvents(Event ev)
        {
            if (_context.Events!.Contains(ev))
            {
                return BadRequest();
            }
            if( DateTime.Parse(ev.StartTime!) > DateTime.Parse(ev.EndTime!))
            {
                return BadRequest();
            }
            ev.OwnerId = GetOwnerId();
            ev.Id = _context.Events!.OrderBy(x => x.Id).Last().Id+1;
            _context.Events!.Add(ev);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEvents), ev);
        }

       [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, Event ev)
        {
            var dbEvents = _context.Events!.AsNoTracking().FirstOrDefault(x => x.Id == ev.Id);

            if (!EventExists(id))
            {
                return NotFound();
            }

            ev.OwnerId = GetOwnerId();
            if (ev.OwnerId != dbEvents!.OwnerId)
            {
                return Unauthorized();
            }

            ev.Id = id;
            _context.Update(ev);
            _context.SaveChanges();

            return NoContent();
        }

       [HttpDelete("{id}")]
        public ActionResult<Event> DeleteEvent(int id)
        {
            var ev = _context.Events!.FirstOrDefault(x => x.Id == id);
            if (ev == null)
            {
                return NotFound();
            }

            if (ev.OwnerId != ev!.OwnerId)
            {
                return Unauthorized();
            }
            
            _context.Events!.Remove(ev);
            _context.SaveChanges();

            return NoContent();
        }
        private bool EventExists(int id)
        {
            return _context.Events!.Any(c => c.Id == id);
        }
        private int GetOwnerId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return int.Parse(identity!.FindFirst("Id")!.Value);
        }
    }
}
