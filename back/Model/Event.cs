using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShareCalendar.Model
{
    public class Event
    {
       [Column("id")] public int Id {get; set;}
       [Column("title")][Required] public string? Title {get; init;}
       [Column("date")][Required] public string? Date {get; init;}
       [Column("startTime")][Required] public string? StartTime {get; init;}
       [Column("endTime")][Required] public string? EndTime {get; init;}
       [Column("description")] public string? Description {get; init;}
       [Column("ownerId")] public int OwnerId { get; set; }

       //public virtual User? Owner { get; set; }
    }
}