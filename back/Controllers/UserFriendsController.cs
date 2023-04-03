using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareCalendar.Model;
using Microsoft.AspNetCore.Authorization;

namespace back.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserFriendsController : ControllerBase
    {
        private readonly DataContext _context;
  
        public UserFriendsController( DataContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public ActionResult<ICollection<UserFriends>> GetFriends(int id)
        {
            var friendList = _context.UsersFriends!.Where(x => x.UserId == id);
            return Ok(friendList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddFriend(int friendsId, int id)
        {
            var user = _context.Users!.Find(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var friend = _context.Users!.Find(friendsId);
            if (friend == null)
            {
                return NotFound("Friend not found");
            }

            // Check if the specified user is already in the user's friends list
            var existingFriend = _context.UsersFriends!
                                        .Where(x => x.UserId == id && x.FriendsId == friendsId)
                                        .ToList();

            if (existingFriend.Count() > 1)
            {
                return BadRequest("The specified user is already in your friends list.");
            }

            var userFriendsList = new List<UserFriends>();

            // Add the friend to the user's friends list
            userFriendsList.Add(new UserFriends
            {
                FriendsListId = id + friendsId,
                UserId = user.Id,
                FriendsId = friend.Id,
                Roles = UserFriends.FriendsRole.Other,
                FriendsFirstname = friend.Firstname,
                FriendsLastname = friend.Lastname,
                FriendsUsername = friend.Username,
                FriendsPhoto = friend.Photo
            });

            // Add the user to the friend's friends list
            userFriendsList.Add(new UserFriends
            {
                FriendsListId = id + friendsId + 1,
                UserId = friend.Id,
                FriendsId = user.Id,
                Roles = UserFriends.FriendsRole.Other,
                FriendsFirstname = user.Firstname,
                FriendsLastname = user.Lastname,
                FriendsUsername = user.Username,
                FriendsPhoto = user.Photo
            });

            _context.UsersFriends!.AddRange(userFriendsList);
            await _context.SaveChangesAsync();

            return Ok(userFriendsList);
        }

        [HttpPut("{id}/friends/{friendId}/roles")]
        public async Task<IActionResult> ChangeFriendRole(int id, string friendId, UserFriends.FriendsRole newRole)
        {
            var user = await _context.UsersFriends!.FirstOrDefaultAsync(u => u.UserId == id && u.FriendsId == int.Parse(friendId));
            if (user == null)
            {
                return NotFound();
            }

            user.Roles = newRole;
            _context.Update(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
        
       [HttpDelete("{id}")]
        public IActionResult DeleteFriend(int id, int friendId)
        {
            var user = _context.Users!.FirstOrDefault(x => x.Id == id);
            var friend = _context.Users!.FirstOrDefault(x => x.Id == friendId);

            if (user == null || friend == null)
            {
                return NotFound();
            }

            var userFriendship = _context.UsersFriends!.FirstOrDefault(x => x.UserId == id && x.FriendsId == friendId);
            var friendFriendship = _context.UsersFriends!.FirstOrDefault(x => x.UserId == friendId && x.FriendsId == id);

            if (userFriendship == null || friendFriendship == null)
            {
                return NotFound();
            }

            // Remove the friend from the user's friend list and save the changes to the database.
            user.Friends!.Remove(userFriendship);
            _context.SaveChanges();

            // Remove the user from the friend's friend list and save the changes to the database.
            friend.Friends!.Remove(friendFriendship);
            _context.SaveChanges();

            return NoContent();
        }

    }
}