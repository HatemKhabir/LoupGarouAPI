using LoupGarou.Data;
using Microsoft.EntityFrameworkCore;
using LoupGarou.Controllers;
using LoupGarou.Services.Interfaces;
using LoupGarou.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowedOrigins",
        policy =>
        {
            policy.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IVoteService, VoteServie>();

builder.Services.AddDbContext<LoupGarouDbContext>(
    o => o.UseSqlServer( builder.Configuration.GetConnectionString("SqlServer")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyAllowedOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
