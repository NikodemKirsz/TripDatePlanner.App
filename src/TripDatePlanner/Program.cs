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
services.AddSwaggerGen(o => o.UseDateOnlyTimeOnlyStringConverters());
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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();