using Riok.Mapperly.Abstractions;
using TripDatePlanner.Entities;
using TripDatePlanner.Mappers.Interfaces;
using TripDatePlanner.Models;
using TripDatePlanner.Models.Dto;
using Range = TripDatePlanner.Entities.Range;

namespace TripDatePlanner.Mappers;

[Mapper]
public sealed partial class MultiMapper :
    IMapper<TripPostDto, Trip>,
    IMapper<ParticipantPostDto, Participant>,
    IMapper<RangePostDto, Range>,
    IMapper<ParticipantWithRangesPostDto, Participant>
{
    public partial Trip Map(TripPostDto postDto);

    public partial Participant Map(ParticipantPostDto postDto);

    public partial Range Map(RangePostDto postDto);

    public Participant Map(ParticipantWithRangesPostDto postDto)
    {
        Participant participant = Map((ParticipantPostDto)postDto);
        participant.Ranges = postDto.RangesPostDto.Select(Map).ToArray();

        return participant;
    }
}