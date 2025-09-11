using Microsoft.EntityFrameworkCore;
using ContratacaoService.Application.Commands;
using ContratacaoService.Application.Interfaces;
using ContratacaoService.Application.Queries;
using ContratacaoService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContratacaoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<IPropostaServiceClient, PropostaServiceClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PropostaService:BaseUrl"] ?? "http://localhost:7001");
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddScoped<IContratacaoRepository, ContratacaoRepository>();

builder.Services.AddScoped<CriarContratacaoCommandHandler>();
builder.Services.AddScoped<ObterContratacaoQueryHandler>();
builder.Services.AddScoped<ListarContratacoesQueryHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ContratacaoDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
