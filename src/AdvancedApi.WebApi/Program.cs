using AdvancedApi.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddJwtConfiguration();
builder.Services.AddDependencyInjectionConfiguration();

var app = builder.Build();

app.UseSwaggerSetup();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();