using Microsoft.EntityFrameworkCore;
using Npgsql;
using static ShareCalendar.Model.User;
using static ShareCalendar.Model.UserFriends;

namespace ShareCalendar.Model
{
    public class DataContext : DbContext
    {
       public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        } 
       static DataContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<UserStatus>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<FriendsRole>();
        }
        public DbSet<Event>? Events{get; set;}
        public DbSet<User>? Users{get; set;}
         public DbSet<UserFriends>? UsersFriends {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresEnum<UserStatus>();
            modelBuilder.HasPostgresEnum<FriendsRole>();
            
            modelBuilder.Entity<UserFriends>(b =>
             {
                 b.HasKey(x => new { x.FriendsListId });
             });

            

            modelBuilder.Entity<Event>().Property(p => p.Id).HasIdentityOptions(startValue: 4);

                 modelBuilder.Entity<Event>().HasData(
                new Event
                 {
                    Id= 1,
                     Title = "Work",
                     Date = "2022-12-16",
                     StartTime = "08:00",
                     EndTime = "16:30",
                     Description = "Long shift. Lunch - 1 hour",
                      OwnerId = 1
                 },
                 new Event
                 {
                    Id= 2,
                     Title = "Gym",
                    Date = "2022-12-16",
                     StartTime = "17:30",
                     EndTime = "19:00",
                     Description = "Cardio",
                      OwnerId = 1
                 },
                  new Event
                 {
                    Id= 3,
                     Title = "Chill with friends",
                    Date = "2022-12-16",
                     StartTime = "19:30",
                     EndTime = "23:00",
                     Description = "",
                      OwnerId = 1
                 },
                 new Event
                {
                    Id= 4,
                     Title = "Work",
                     Date = "2022-12-16",
                     StartTime = "08:00",
                     EndTime = "16:30",
                     Description = "Long shift. Lunch - 1 hour",
                      OwnerId = 2
                 },
                 new Event
                 {
                    Id= 5,
                     Title = "Gym",
                    Date = "2022-12-16",
                     StartTime = "17:30",
                     EndTime = "19:00",
                     Description = "Cardio",
                      OwnerId = 2
                 },
                  new Event
                 {
                    Id= 6,
                     Title = "Chill with friends",
                    Date = "2022-12-16",
                     StartTime = "19:30",
                     EndTime = "23:00",
                     Description = "",
                      OwnerId = 2
                 },
                new Event
                {
                    Id= 7,
                    Title = "Math Exam",
                    Date = "2022-12-16",
                    StartTime = "08:30",
                    EndTime = "15:30",
                    OwnerId = 3
                },
                 new Event
                 {
                    Id= 8,
                     Title = "Haircut",
                    Date = "2022-12-16",
                     StartTime = "16:45",
                     EndTime = "19:00",
                     Description = "Studio salon",
                      OwnerId = 3
                 },
                new Event
                {
                    Id= 9,
                    Title = "Gym",
                    Date = "2022-12-16",
                    StartTime = "19:30",
                    EndTime = "21:00",
                    OwnerId = 3
                },
                new Event
                {
                    Id= 10,
                    Title = "Math Exam",
                    Date = "2022-12-16",
                    StartTime = "08:30",
                    EndTime = "15:30",
                    OwnerId = 4
                },
                 new Event
                 {
                    Id= 11,
                     Title = "Haircut",
                    Date = "2022-12-16",
                     StartTime = "16:45",
                     EndTime = "19:00",
                     Description = "Studio salon",
                      OwnerId = 4
                 },
                new Event
                {
                    Id= 12,
                    Title = "Gym",
                    Date = "2022-12-16",
                    StartTime = "19:30",
                    EndTime = "21:00",
                    OwnerId = 4
                }
                );

            modelBuilder.Entity<User>().HasData(
               new User {
                    Id=1,
                    Username="victori.a",
                    Firstname = "Victoria",
                    Lastname= "Korbin",
                    Email="victoria.korbin@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 1,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                },
                new User {
                    Id=2,
                    Username="van.kan",
                    Firstname = "Van",
                    Lastname= "Korbin",
                    Email="van.korbin@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 2,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k="
                },
                new User {
                    Id=3,
                    Username="va.lentino",
                    Firstname = "Valentino",
                    Lastname= "Korbin",
                    Email="valentino.korbin@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 3,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                },
                new User {
                    Id=4,
                    Username="violet.ta",
                    Firstname = "Violetta",
                    Lastname= "Korbin",
                    Email="valentino.korbin@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 4,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                },
                new User {
                    Id=5,
                    Username="steevee",
                    Firstname = "Steven",
                    Lastname= "Lee",
                    Email="steven.lee@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 5,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                },
                new User {
                    Id=6,
                    Username="emily.swe",
                    Firstname = "Emily",
                    Lastname= "Sweden",
                    Email="emily.sweden@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 6,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                },
                new User {
                    Id=7,
                    Username="lau.ren.wi",
                    Firstname = "Lauren",
                    Lastname= "Weesley",
                    Email="lauren.weesley@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 7,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                },
                new User {
                    Id=8,
                    Username="boris.smith",
                    Firstname = "Boris",
                    Lastname= "Smith",
                    Email="boris.smith@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 8,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                },
                new User {
                    Id=9,
                    Username="grand.pa",
                    Firstname = "Patrick",
                    Lastname= "Grand",
                    Email="patrick.grand@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 9,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                },
                new User {
                    Id=10,
                    Username="bbbarril",
                    Firstname = "Bruce",
                    Lastname= "Barril",
                    Email="bruce.barril@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 10,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                },
                new User {
                    Id=11,
                    Username="fymarin",
                    Firstname = "Marin",
                    Lastname= "Fynn",
                    Email="marin.fynn@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 11,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                },
                new User {
                    Id=12,
                    Username="zora.indiah",
                    Firstname = "Zora",
                    Lastname= "Indah",
                    Email="zora.indah@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 12,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                },
                new User {
                    Id=13,
                    Username="otto.chios",
                    Firstname = "Ottó",
                    Lastname= "Eutychios",
                    Email="otto.eutychios@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 13,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                },
                new User {
                    Id=14,
                    Username="michael",
                    Firstname = "Michael",
                    Lastname= "Wales",
                    Email="marin.fynn@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 14,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                },
                new User {
                    Id=15,
                    Username="bob.by",
                    Firstname = "Baby",
                    Lastname= "Bob",
                    Email="zora.indah@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 15,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                },
                new User {
                    Id=16,
                    Username="marlenn",
                    Firstname = "Marlen",
                    Lastname= "Monro",
                    Email="otto.eutychios@gmail.com",
                    Birthday="2003-12-30",
                    Status=UserStatus.Private,
                    Photo = 16,
                    Password="kvl9cgc7ua4RxTPgD2yGetC/MHAN0uA8B+tsr1kpC0k=" //Password=12345
                }
                );
 
                modelBuilder.Entity<UserFriends>().HasData(
                new UserFriends
                {
                    FriendsListId = 1,
                    FriendsId = 1,
                    UserId = 2,
                    Roles = UserFriends.FriendsRole.Friends,
                    FriendsFirstname = "Victoria",
                    FriendsLastname ="Korbin",
                    FriendsUsername ="victori.a",
                    FriendsPhoto =1
                },
                new UserFriends
                {
                    FriendsListId = 2,
                    FriendsId = 2,
                    UserId = 1,
                    Roles = UserFriends.FriendsRole.Friends,
                    FriendsFirstname = "Van",
                    FriendsLastname ="Korbin",
                    FriendsUsername ="van.kan",
                    FriendsPhoto = 2
                },
                new UserFriends
                {
                    FriendsListId = 3,
                    FriendsId = 3,
                    UserId = 1,
                    Roles = UserFriends.FriendsRole.Friends,
                    FriendsFirstname = "Valentino",
                    FriendsLastname ="Korbin",
                    FriendsUsername ="va.lentino",
                    FriendsPhoto =3
                },
                new UserFriends
                {
                    FriendsListId = 4,
                    FriendsId = 4,
                    UserId = 1,
                    Roles = UserFriends.FriendsRole.Friends,
                    FriendsFirstname = "Violetta",
                    FriendsLastname ="Korbin",
                    FriendsUsername ="violet.ta",
                    FriendsPhoto = 4
                },
                new UserFriends
                {
                    FriendsListId = 5,
                    FriendsId = 5,
                    UserId = 1,
                    Roles = UserFriends.FriendsRole.Friends,
                    FriendsFirstname = "Steven",
                    FriendsLastname ="Lee",
                    FriendsUsername ="steevee",
                    FriendsPhoto = 5
                },
                new UserFriends
                {
                    FriendsListId = 6,
                    FriendsId = 6,
                    UserId = 1,
                    Roles = UserFriends.FriendsRole.Friends,
                    FriendsFirstname = "Emily",
                    FriendsLastname ="Sweden",
                    FriendsUsername ="emily.swe",
                    FriendsPhoto = 6
                },
                new UserFriends
                {
                    FriendsListId = 7,
                    FriendsId = 7,
                    UserId = 1,
                    Roles = UserFriends.FriendsRole.Friends,
                    FriendsFirstname = "Lauren",
                    FriendsLastname ="Weesley",
                    FriendsUsername ="lau.ren.wi",
                    FriendsPhoto = 7
                },
                new UserFriends
                {
                    FriendsListId = 8,
                    FriendsId = 8,
                    UserId = 1,
                    Roles = UserFriends.FriendsRole.Friends,
                    FriendsFirstname = "Boris",
                    FriendsLastname ="Smith",
                    FriendsUsername ="boris.smith",
                    FriendsPhoto = 8
                },
                new UserFriends
                {
                    FriendsListId = 9,
                    FriendsId = 9,
                    UserId = 1,
                    Roles = UserFriends.FriendsRole.Friends,
                    FriendsFirstname = "Patrick",
                    FriendsLastname ="Grand",
                    FriendsUsername ="grand.pa",
                    FriendsPhoto = 9
                },
                new UserFriends
                {
                    FriendsListId = 10,
                    FriendsId = 10,
                    UserId = 1,
                    Roles = UserFriends.FriendsRole.Friends,
                    FriendsFirstname = "Bruce",
                    FriendsLastname ="Barril",
                    FriendsUsername ="bbbarril",
                    FriendsPhoto = 10
                },
                new UserFriends
                {
                    FriendsListId = 11,
                    FriendsId = 11,
                    UserId = 1,
                    Roles = UserFriends.FriendsRole.Friends,
                    FriendsFirstname = "Marin",
                    FriendsLastname ="Fynn",
                    FriendsUsername ="fymarin",
                    FriendsPhoto = 11
                },
                new UserFriends
                {
                    FriendsListId = 12,
                    FriendsId = 12,
                    UserId = 1,
                    Roles = UserFriends.FriendsRole.Friends,
                    FriendsFirstname = "Zora",
                    FriendsLastname ="Indah",
                    FriendsUsername ="zora.indiah",
                    FriendsPhoto = 12
                },
                new UserFriends
                {
                    FriendsListId = 13,
                    FriendsId = 13,
                    UserId = 1,
                    Roles = UserFriends.FriendsRole.Friends,
                    FriendsFirstname = "Ottó",
                    FriendsLastname ="Eutychios",
                    FriendsUsername ="otto.chios",
                    FriendsPhoto = 13
                });           
         }
  }
}