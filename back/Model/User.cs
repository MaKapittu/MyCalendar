using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace ShareCalendar.Model
{
        public class User
    {
        [Column("id")] public int Id {get; set;}
        [Column("username")][Required] public string? Username {get; set;}
        [Column("firtsname")] public string? Firstname {get; set;}
        [Column("lastname")] public string? Lastname {get; set;}
        [Column("email")] public string? Email {get; set;}
        [Column("birthday")] public string? Birthday {get; set;}
        [Column("status")][Required] public UserStatus Status {get; set;}
        [Column("photo")] public int Photo {get; set;} = 1;
        [Column("password")][Required] public string? Password { get; set; }
        [JsonIgnore] public virtual ICollection<Event>? UsersEvents { get; set; }
        [JsonIgnore] public ICollection<UserFriends>? Friends { get; set; }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum UserStatus
        {
            Private = 0,
            Public = 1
        }
    }
}
