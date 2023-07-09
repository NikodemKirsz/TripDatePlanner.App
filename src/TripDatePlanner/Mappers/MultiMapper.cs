using Riok.Mapperly.Abstractions;
using TripDatePlanner.Entities;
using TripDatePlanner.Mappers.Interfaces;
using TripDatePlanner.Models.DateRange;
using TripDatePlanner.Models.Dto;
using Range = TripDatePlanner.Entities.Range;

namespace TripDatePlanner.Mappers;

[Mapper]
public sealed partial class MultiMapper :
    IMapper<TripPostDto, Trip>,
    IMapper<ParticipantPostDto, Participant>,
    IMapper<RangePostDto, Range>
{
    public Trip Map(TripPostDto postDto)
    {
        return new Trip()
        {
            Name = postDto.Name,
            Password = postDto.Password,
            AllowedRange = new(postDto.AllowedRangeStart, postDto.AllowedRangeEnd),
            MinDays = postDto.MinDays,
            MaxDays = postDto.MaxDays,
        };
    }

    public partial Participant Map(ParticipantPostDto postDto);
    
    public Range Map(RangePostDto postDto)
    {
        return new Range()
        {
            DateRange = new(postDto.DateRangeStart, postDto.DateRangeEnd),
            RangeType = postDto.RangeType,
            ParticipantId = postDto.ParticipantId,
        };
    }
}