using Microsoft.AspNetCore.Mvc;
using TripDatePlanner.Entities;
using TripDatePlanner.Mappers.Interfaces;
using TripDatePlanner.Models.DateRange;
using TripDatePlanner.Models.Dto;
using TripDatePlanner.Services.Interfaces;

namespace TripDatePlanner.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class TripController : CrudController<string, Trip, TripPostDto>
{
    private readonly ILogger<TripController> _logger;
    private readonly ITripService _tripService;
    private readonly IMapper<TripPostDto, Trip> _tripMapper;

    public TripController(
        ILogger<TripController> logger,
        ITripService tripService,
        IMapper<TripPostDto, Trip> tripMapper)
        : base(logger, tripService, tripMapper)
    {
        _logger = logger;
        _tripService = tripService;
        _tripMapper = tripMapper;
    }

    [HttpGet("[action]/{tripId}")]
    public async Task<IActionResult> Evaluate(string tripId, CancellationToken token = default)
    {
        DateRange[] sortedRanges = 
        {
            new(DateOnly.MinValue, DateOnly.MaxValue),
        };

        return await Task.FromResult<IActionResult>(Ok(sortedRanges));
    }
}