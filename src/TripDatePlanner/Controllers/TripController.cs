using Microsoft.AspNetCore.Mvc;
using TripDatePlanner.Entities;
using TripDatePlanner.Mappers.Interfaces;
using TripDatePlanner.Models;
using TripDatePlanner.Models.Dto;
using TripDatePlanner.Services.Interfaces;

namespace TripDatePlanner.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TripController : CrudController<string, Trip, TripPostDto>
{
    private readonly ILogger<TripController> _logger;
    private readonly ITripService _tripService;
    private readonly IParticipantService _participantService;
    private readonly IMapper<TripPostDto, Trip> _tripMapper;
    private readonly IMapper<Trip?, TripWithStatsDto> _tripWithStatsMapper;

    public TripController(
        ILogger<TripController> logger,
        ITripService tripService,
        IParticipantService participantService,
        IMapper<TripPostDto, Trip> tripMapper,
        IMapper<Trip?, TripWithStatsDto> tripWithStatsMapper)
        : base(logger, tripService, tripMapper)
    {
        _logger = logger;
        _tripService = tripService;
        _participantService = participantService;
        _tripMapper = tripMapper;
        _tripWithStatsMapper = tripWithStatsMapper;
    }

    [HttpGet("{tripId}/[action]")]
    public async Task<ActionResult<TripWithStatsDto>> GetWithStats(string tripId, CancellationToken token = default)
    {
        Trip trip = await _tripService.Get(tripId, token: token);

        Participant[] participants = await _participantService.GetMultipleWithRangesByTripId(tripId, token: token);
        trip.Participants = participants;

        TripWithStatsDto tripWithStatsDto = _tripWithStatsMapper.Map(trip);
        return Ok(tripWithStatsDto);
    }

    [HttpGet("{tripId}/[action]")]
    public async Task<ActionResult<DateRange[]>> Evaluate(string tripId, CancellationToken token = default)
    {
        DateRange[] sortedRanges = {
            new(DateOnly.MinValue, DateOnly.MaxValue),
            new(DateOnly.MinValue, DateOnly.MaxValue),
            new(DateOnly.MinValue, DateOnly.MaxValue),
            new(DateOnly.MinValue, DateOnly.MaxValue),
        };

        await Task.Delay(0, token);

        return Ok(sortedRanges);
    }
}