using DocumentAccessApprovalSystemAPI.DbContexts;
using DocumentAccessApprovalSystemAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(DbContextOptions => DbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:myDBConnectionString"]));
builder.Services.AddScoped<IApprovalSystemRepository, ApprovalSystemRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Register AutoMapper with all assemblies in the current domain
builder.Services.AddTransient<IMailService, LocalMailService>();
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

app.Run();
