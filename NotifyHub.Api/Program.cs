using Microsoft.EntityFrameworkCore;
using NotifyHub.Api.Hubs;
using NotifyHub.Api.Source.Application.Services;
using NotifyHub.Api.Source.Domain.Interfaces;
using NotifyHub.Api.Source.Infrastructure.Data;
using NotifyHub.Api.Source.Infrastructure.Repositories;
using NotifyHub.Api.Source.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=DESKTOP-FGUE93R\\SQLEXPRESS;Database=SafetyClientDB;Trusted_Connection=True;TrustServerCertificate=True;";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
// ✅ ADD DEPENDENCY INJECTION
builder.Services.AddScoped<ICallRepository, CallRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IStackRepository, StackRepository>();
builder.Services.AddScoped<CallService>();
builder.Services.AddScoped<StackService>();
builder.Services.AddScoped<UnitService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyMethod()
               .AllowAnyHeader()
               .AllowAnyOrigin());
});

// Add SignalR
builder.Services.AddSignalR();

var app = builder.Build();
app.MapHub<CallHub>("/callHub");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
