using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using TripDatePlanner.Entities;
using TripDatePlanner.Models.DateRange;
using Range = TripDatePlanner.Entities.Range;

namespace TripDatePlanner.Data;

public sealed class DataContext : DbContext
{
    private readonly ILogger<DataContext> _logger;
    private readonly IConfiguration _configuration;

    public DataContext(
        ILogger<DataContext> logger,
        DbContextOptions<DataContext> options,
        IConfiguration configuration)
        : base(options)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public DbSet<Trip> Trips { get; set; } = null!;
    public DbSet<Participant> Participants { get; set; } = null!;
    public DbSet<Range> Ranges { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        string mainDbConnectionString = _configuration.GetConnectionString(Consts.MainDbConnectionStringName) 
            ?? throw new ArgumentException($"Missing ConnectionString for {Consts.MainDbConnectionStringName}");

        optionsBuilder.UseMySQL(mainDbConnectionString, options => 
        {
            options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            options.MigrationsAssembly(typeof(DataContext).Assembly.FullName);
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        MySQLModelBuilderExtensions.UseCollation(modelBuilder,"utf16_general_ci");
            
        modelBuilder.Entity<Trip>(builder =>
        {
            builder
                .Property<DateRange>(trip => trip.AllowedRange)
                .HasConversion<DateRangeStringConverter>();
        });

        modelBuilder.Entity<Participant>(builder =>
        {
            builder
                .HasOne<Trip>(participant => participant.Trip)
                .WithMany(trip => trip.Participants);
        });

        modelBuilder.Entity<Range>(builder =>
        {
            builder
                .HasOne<Participant>(range => range.Participant)
                .WithMany(range => range.Ranges);
            builder
                .Property<DateRange>(range => range.DateRange)
                .HasConversion<DateRangeStringConverter>();
        });

        base.OnModelCreating(modelBuilder);
    }
}