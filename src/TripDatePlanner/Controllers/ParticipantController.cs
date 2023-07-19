using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using TripDatePlanner.Entities;
using TripDatePlanner.Mappers.Interfaces;
using TripDatePlanner.Models.Dto;
using TripDatePlanner.Services.Interfaces;

namespace TripDatePlanner.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ParticipantController : CrudController<int, Participant, ParticipantPostDto>
{
    private readonly ILogger<ParticipantController> _logger;
    private readonly IParticipantService _participantService;
    private readonly IRangeService _rangeService;
    private readonly IMapper<ParticipantPostDto, Participant> _participantMapper;
    private readonly IMapper<ParticipantWithRangesPostDto, Participant> _participantWithRangesMapper;

    public ParticipantController(
        ILogger<ParticipantController> logger,
        IParticipantService participantService,
        IRangeService rangeService,
        IMapper<ParticipantPostDto, Participant> participantMapper,
        IMapper<ParticipantWithRangesPostDto, Participant> participantWithRangesMapper)
        : base(logger, participantService, participantMapper)
    {
        _logger = logger;
        _participantService = participantService;
        _rangeService = rangeService;
        _participantMapper = participantMapper;
        _participantWithRangesMapper = participantWithRangesMapper;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateWithRanges([FromBody] ParticipantWithRangesPostDto participantWithRangesPostDto, CancellationToken token = default)
    {
        Participant participant = _participantWithRangesMapper.Map(participantWithRangesPostDto);

        await using IDbContextTransaction transaction = await BeginTransactionAsync(token);

        try
        {
            await _participantService.Create(participant, token: token);
            await _rangeService.CreateRange(participant.Ranges, token: token);

            await transaction.CommitAsync(token);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(token);
            
            _logger.LogError(
                e,
                $$"""Exception occured when creating {{nameof(Participant)}} with {{nameof(Participant.Ranges)}}! Transaction Id: {TransactionId}""",
                transaction.TransactionId
            );
            throw;
        }

        await SaveChangesAsync(token);
        return Ok(participant);
    }
}