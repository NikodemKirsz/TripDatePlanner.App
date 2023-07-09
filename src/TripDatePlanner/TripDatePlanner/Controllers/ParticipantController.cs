using Microsoft.AspNetCore.Mvc;
using TripDatePlanner.Controllers;
using TripDatePlanner.Entities;
using TripDatePlanner.Mappers.Interfaces;
using TripDatePlanner.Models.Dto;
using TripDatePlanner.Services.Interfaces;

namespace ParticipantDatePlanner.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ParticipantController : CrudController<int, Participant, ParticipantPostDto>
{
    private readonly ILogger<ParticipantController> _logger;
    private readonly IParticipantService _participantService;
    private readonly IMapper<ParticipantPostDto, Participant> _participantMapper;

    public ParticipantController(
        ILogger<ParticipantController> logger,
        IParticipantService participantService,
        IMapper<ParticipantPostDto, Participant> participantMapper)
        : base(logger, participantService, participantMapper)
    {
        _logger = logger;
        _participantService = participantService;
        _participantMapper = participantMapper;
    }
}