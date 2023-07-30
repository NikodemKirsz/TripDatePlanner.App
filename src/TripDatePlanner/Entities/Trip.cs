using System.ComponentModel.DataAnnotations;
using TripDatePlanner.Entities.Interfaces;
using TripDatePlanner.Models;

namespace TripDatePlanner.Entities;

public record Trip : IEntity<string>
{
    [StringLength(Consts.MaxTripIdLength)]
    public string Id { get; set; } = null!;

    [StringLength(Consts.MaxNameLength)]
    public string Name { get; set; } = null!;

    [StringLength(Consts.MaxPasscodeLength)]
    public string? Passcode { get; set; }

    [StringLength(Consts.MaxDateRangeStringLength)]
    public DateRange AllowedRange { get; set; }

    public int MinDays { get; set; }

    public int MaxDays { get; set; }


    public virtual IList<Participant> Participants { get; set; } = new List<Participant>();
}