using System.Text.Json.Serialization;

namespace ShareCalendar.Model
{
    public class UserFriends
    {
        public int? FriendsListId {get; set;}
        public int? FriendsId {get; set;}
        public int? UserId {get; set;}
        public string? FriendsUsername {get; set;}
        public string? FriendsFirstname {get; set;}
        public string? FriendsLastname {get; set;}
        public FriendsRole Roles { get; set; }
        public int? FriendsPhoto {get; set;}

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum FriendsRole
        {
            Family = 0,
            Friends = 1,
            Work = 2,
            Other = 3
        }
    }
}
