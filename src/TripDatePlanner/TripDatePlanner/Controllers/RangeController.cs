using Microsoft.AspNetCore.Mvc;
using TripDatePlanner.Mappers.Interfaces;
using TripDatePlanner.Models.Dto;
using TripDatePlanner.Services.Interfaces;
using Range = TripDatePlanner.Entities.Range;

namespace TripDatePlanner.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class RangeController : CrudController<int, Range, RangePostDto>
{
    private readonly ILogger<RangeController> _logger;
    private readonly IRangeService _rangeService;
    private readonly IMapper<RangePostDto, Range> _rangeMapper;

    public RangeController(
        ILogger<RangeController> logger,
        IRangeService rangeService,
        IMapper<RangePostDto, Range> rangeMapper)
        : base(logger, rangeService, rangeMapper)
    {
        _logger = logger;
        _rangeService = rangeService;
        _rangeMapper = rangeMapper;
    }
}