using Microsoft.EntityFrameworkCore;
using WebApi.DAL;

var builder = WebApplication.CreateBuilder(args);

#region AddServicesToContainer

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("name=DefaultConnection",
        sqlServer => sqlServer.UseNetTopologySuite()));

builder.Services.AddAutoMapper(typeof(Program));

#endregion

var app = builder.Build();

#region ConfigurePipeline

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

#endregion

