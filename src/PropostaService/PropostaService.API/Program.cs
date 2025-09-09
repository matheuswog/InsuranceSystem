using Microsoft.EntityFrameworkCore;
using PropostaService.Application.Commands;
using PropostaService.Application.Interfaces;
using PropostaService.Application.Queries;
using PropostaService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<PropostaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IPropostaRepository, PropostaRepository>();

// Command and Query Handlers
builder.Services.AddScoped<CriarPropostaCommandHandler>();
builder.Services.AddScoped<AlterarStatusPropostaCommandHandler>();
builder.Services.AddScoped<ObterPropostaQueryHandler>();
builder.Services.AddScoped<ListarPropostasQueryHandler>();
builder.Services.AddScoped<VerificarPropostaExisteQueryHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PropostaDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
