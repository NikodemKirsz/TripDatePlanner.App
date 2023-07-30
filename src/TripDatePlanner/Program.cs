using TripDatePlanner.Data;
using TripDatePlanner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

services.AddDbContext<DataContext>();

services.AddControllers(options =>
    {
        options.AddFilters();
    })
    .AddJsonOptions(options =>
    {
        options.AddReferenceHandler();
        options.AddConverters();
    });

services.AddServices();
services.AddMappers();

services.AddCors();
services.AddDateOnlyTimeOnlyStringConverters();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options
        .UseDateRangeStringConverter()
        .UseDateOnlyTimeOnlyStringConverters();
});
services.AddHealthChecks();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(options =>
{
    options
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .AllowAnyHeader();
});

app.UseHsts();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello Pal!");

app.Run();