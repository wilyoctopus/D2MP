using D2MP.API.Mapper;
using D2MP.Data;
using D2MP.Data.Interfaces;
using D2MP.Infrastructure;
using D2MP.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddInfrastructureServices();
builder.Services.AddDataServices();
builder.Services.AddMetaPartyServices();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

MigrateDatabase(builder.Services.BuildServiceProvider());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();

void MigrateDatabase(IServiceProvider serviceProvider)
{
    var migrator = serviceProvider.GetService<IMigrationService>();
    migrator.MigrateUp();
}
