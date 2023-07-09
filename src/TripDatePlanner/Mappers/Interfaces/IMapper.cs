namespace TripDatePlanner.Mappers.Interfaces;

public interface IMapper<in TSrc, out TDest>
{
    TDest Map(TSrc postDto);
}