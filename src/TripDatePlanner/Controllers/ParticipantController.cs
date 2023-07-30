using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using TripDatePlanner.Entities;
using TripDatePlanner.Mappers.Interfaces;
using TripDatePlanner.Models.Dto;
using TripDatePlanner.Services.Interfaces;
using TripDatePlanner.Utilities.Extensions;
using Range = TripDatePlanner.Entities.Range;

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
    private readonly IMapper<Participant, ParticipantWithStatsDto> _participantWithStatsMapper;
    private readonly IMapper<Participant[], ParticipantWithStatsDto[]> _participantsWithStatsMapper;

    public ParticipantController(
        ILogger<ParticipantController> logger,
        IParticipantService participantService,
        IRangeService rangeService,
        IMapper<ParticipantPostDto, Participant> participantMapper,
        IMapper<ParticipantWithRangesPostDto, Participant> participantWithRangesMapper,
        IMapper<Participant, ParticipantWithStatsDto> participantWithStatsMapper,
        IMapper<Participant[], ParticipantWithStatsDto[]> participantsWithStatsMapper)
        : base(logger, participantService, participantMapper)
    {
        _logger = logger;
        _participantService = participantService;
        _rangeService = rangeService;
        _participantMapper = participantMapper;
        _participantWithRangesMapper = participantWithRangesMapper;
        _participantWithStatsMapper = participantWithStatsMapper;
        _participantsWithStatsMapper = participantsWithStatsMapper;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<ParticipantWithStatsDto>> GetWMultipleWithRanges([FromQuery] int[] ids, CancellationToken token = default)
    {
        Participant[] participants = await _participantService.GetMultipleWithRanges(ids, token: token);

        ParticipantWithStatsDto[] participantsWithStatsDto = _participantsWithStatsMapper.Map(participants);
        return Ok(participantsWithStatsDto);
    }

    [HttpGet("[action]/{tripId}")]
    public async Task<ActionResult<ParticipantWithStatsDto>> GetWMultipleWithRangesByTripId([FromRoute] string tripId, CancellationToken token = default)
    {
        Participant[] participants = await _participantService.GetMultipleWithRangesByTripId(tripId, token: token);

        ParticipantWithStatsDto[] participantsWithStatsDto = _participantsWithStatsMapper.Map(participants);
        return Ok(participantsWithStatsDto);
    }

    [HttpGet("{id:int}/[action]")]
    public async Task<ActionResult<ParticipantWithStatsDto>> GetWithRanges(int id, CancellationToken token = default)
    {
        Participant participant = await _participantService.Get(id, true, token);

        ParticipantWithStatsDto participantWithStatsDto = _participantWithStatsMapper.Map(participant);
        return Ok(participantWithStatsDto);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<Participant>> CreateWithRanges([FromBody] ParticipantWithRangesPostDto participantWithRangesPostDto, CancellationToken token = default)
    {
        Participant createdParticipant;
        Range[] createdRanges;
        
        Participant participant = _participantWithRangesMapper.Map(participantWithRangesPostDto);
        
        await using IDbContextTransaction transaction = await BeginTransactionAsync(token);

        try
        {
            Participant onlyParticipant = participant with { Ranges = new List<Range>(), Trip = null };
            createdParticipant = await _participantService.Create(onlyParticipant, save: true, token: token);

            participant.Ranges.ForEach(r => r.ParticipantId = createdParticipant.Id);
            createdRanges = await _rangeService.CreateRangeNormalized(participant.Ranges, save: true, token: token);

            await transaction.CommitAsync(token);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(token);
            
            _logger.LogError(
                e,
                $"Exception occured when creating {nameof(Participant)} with {nameof(Participant.Ranges)}! Transaction Id: {{TransactionId}}",
                transaction.TransactionId
            );
            throw;
        }

        int entriesWritten = await SaveChangesAsync(token);
        _logger.LogInformation(
            $"Successfully saved changes for '{nameof(CreateWithRanges)}' operation. Entries written: {{EntriesWritten}}",
            entriesWritten
        );

        createdParticipant.Ranges = createdRanges;
        return CreatedAtAction(
            nameof(Get),
            nameof(Participant),
            new { createdParticipant.Id },
            createdParticipant
        );
    }

    [HttpPut("{id:int}/[action]")]
    public async Task<ActionResult<Participant>> UpdateWithRanges(int id, [FromBody] ParticipantWithRangesPostDto participantWithRangesPostDto, CancellationToken token = default)
    {
        Participant updatedParticipant;
        Range[] createdRanges;
        
        Participant participant = _participantWithRangesMapper.Map(participantWithRangesPostDto);
        
        await using IDbContextTransaction transaction = await BeginTransactionAsync(token);

        try
        {
            Participant onlyParticipant = participant with { Ranges = new List<Range>(), Trip = null };
            updatedParticipant = await _participantService.Update(id, onlyParticipant, save: true, token: token);
            
            await _rangeService.DeleteMultipleByParticipantId(id, token: token);
            
            participant.Ranges.ForEach(r => r.ParticipantId = updatedParticipant.Id);
            createdRanges = await _rangeService.CreateRangeNormalized(participant.Ranges, save: true, token: token);

            await transaction.CommitAsync(token);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(token);
            
            _logger.LogError(
                e,
                $"Exception occured when updating {nameof(Participant)} with {nameof(Participant.Ranges)}! Transaction Id: {{TransactionId}}",
                transaction.TransactionId
            );
            throw;
        }

        int entriesWritten = await SaveChangesAsync(token);
        _logger.LogInformation(
            $"Successfully saved changes for '{nameof(UpdateWithRanges)}' operation. Entries written: {{EntriesWritten}}",
            entriesWritten
        );

        updatedParticipant.Ranges = createdRanges;
        return CreatedAtAction(
            nameof(Get),
            nameof(Participant),
            new { updatedParticipant.Id },
            updatedParticipant
        );
    }
}