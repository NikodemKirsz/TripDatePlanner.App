using Riok.Mapperly.Abstractions;
using TripDatePlanner.Entities;
using TripDatePlanner.Entities.Enums;
using TripDatePlanner.Mappers.Interfaces;
using TripDatePlanner.Models.Dto;
using TripDatePlanner.Utilities.Extensions;
using Range = TripDatePlanner.Entities.Range;

namespace TripDatePlanner.Mappers;

[Mapper]
public sealed partial class MultiMapper :
    IMapper<TripPostDto, Trip>,
    IMapper<ParticipantPostDto, Participant>,
    IMapper<RangePostDto, Range>,
    IMapper<ParticipantWithRangesPostDto, Participant>,
    IMapper<Range, RangeDto>,
    IMapper<Participant, ParticipantWithStatsDto>,
    IMapper<Participant[], ParticipantWithStatsDto[]>,
    IMapper<Trip, TripWithStatsDto>
{
    public partial Trip Map(TripPostDto postDto);

    public partial Participant Map(ParticipantPostDto postDto);

    public partial Range Map(RangePostDto postDto);

    public Participant Map(ParticipantWithRangesPostDto postDto)
    {
        Participant participant = Map((ParticipantPostDto)postDto);

        List<Range> ranges = new(postDto.PreferredRanges.Length + postDto.RejectedRanges.Length);
        ranges.AddRange(postDto.PreferredRanges.Select(pr => new Range()
        {
            DateRange = pr,
            RangeType = RangeType.Preferred,
        }));
        ranges.AddRange(postDto.RejectedRanges.Select(rr => new Range()
        {
            DateRange = rr,
            RangeType = RangeType.Rejected,
        }));

        participant.Ranges = ranges;

        return participant;
    }

    public partial RangeDto Map(Range range);

    public ParticipantWithStatsDto Map(Participant entity)
    {
        var (preferredRanges, rejectedRanges) = entity.SplitRanges();
        
        return new ParticipantWithStatsDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            TripId = entity.TripId,
            PreferredRanges = preferredRanges,
            RejectedRanges = rejectedRanges,
            PreferredDays = preferredRanges.Sum(r => r.Length),
            RejectedDays = rejectedRanges.Sum(r => r.Length),
        };
    }

    public partial ParticipantWithStatsDto[] Map(Participant[] postDto);
    
    public partial TripWithStatsDto Map(Trip postDto);
}